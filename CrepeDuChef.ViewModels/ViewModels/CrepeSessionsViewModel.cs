using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Domain.Exceptions;
using CrepeDuChef.ViewModels.Mappers;
using CrepeDuChef.ViewModels.Models;
using CrepeDuChef.ViewModels.Presenters;
using System.Diagnostics;
using CrepeDuChef.Localization.Resources;

namespace CrepeDuChef.ViewModels.ViewModels
{
    /// <summary>
    /// Draws the next chef and shows the crepe party history, grouped by session.
    /// </summary>
    public partial class CrepeSessionsViewModel : ObservableObject
    {
        private readonly IChefService _chefService;
        private readonly IChefRotationService _rotationService;
        private readonly ICrepePartyService _crepePartyService;
        private readonly IDialogPresenter _dialogPresenter;
        private readonly IChefSelectionPresenter _selectionPresenter;

        private IReadOnlyList<CrepePartySessionGroup> _allSessions = [];

        #region Translations
        public string CrepeDuChef_title => UiResources.CrepeDuChef_title;
        public string WhoIsTheChef => UiResources.WhoIsTheChef;
        public string ChefsAwayMsg => UiResources.ChefsAway;
        public string NoSessionInDb => UiResources.NoSessionInDb;
        public string OnContentOnlyCurrentSession => UiResources.ShowOnlyCurrentSession;
        public string OffOnContentAllSession => UiResources.ShowAllSession;
        public string HistoryMsg => UiResources.History;
        public string TodayMsg => UiResources.Today;
        #endregion

        public CrepeSessionsViewModel(
            IChefService chefService,
            IChefRotationService rotationService,
            ICrepePartyService crepePartyService,
            IDialogPresenter dialogPresenter,
            IChefSelectionPresenter selectionPresenter)
        {
            _chefService = chefService;
            _rotationService = rotationService;
            _crepePartyService = crepePartyService;
            _dialogPresenter = dialogPresenter;
            _selectionPresenter = selectionPresenter;
        }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasSessions))]
        public partial IReadOnlyList<CrepePartySessionGroup> Sessions { get; private set; } = [];

        public bool HasSessions => Sessions.Count > 0;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(TextSwitchMessage))]
        public partial bool ShowOnlyCurrent { get; set; } = true;

        public string TextSwitchMessage =>
            ShowOnlyCurrent
                ? OnContentOnlyCurrentSession
                : OffOnContentAllSession;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsMoreThanOneChef))]
        [NotifyPropertyChangedFor(nameof(PresentChefsMsg))]
        [NotifyCanExecuteChangedFor(nameof(SelectAvailableChefsCommand))]
        public partial IReadOnlyList<ChefDto> AllChefs { get; private set; } = [];

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(PresentChefsMsg))]
        [NotifyCanExecuteChangedFor(nameof(GetNewChefCommand))]
        public partial IReadOnlyList<ChefDto> AvailableChefs { get; private set; } = [];

        public bool IsMoreThanOneChef => AllChefs.Count > 1;

        /// <summary>"3 chefs present out of 4".</summary>
        public string PresentChefsMsg => string.Format(UiResources.PresentChefs, AvailableChefs.Count, AllChefs.Count);

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanUpdate))]
        public partial bool IsUpdating { get; private set; }

        public bool CanUpdate => !IsUpdating;

        /// <summary>
        /// Reloads chefs and history each time the view appears, so chefs added or renamed
        /// elsewhere are taken into account. Chefs marked absent stay absent; new chefs are available.
        /// </summary>
        public async Task OnAppearingAsync()
        {
            IReadOnlyList<ChefDto> previousChefs = AllChefs;
            HashSet<Guid> knownIds = [.. previousChefs.Select(c => c.Id)];
            HashSet<Guid> availableIds = [.. AvailableChefs.Select(c => c.Id)];

            AllChefs = await _chefService.GetAllAsync();
            AvailableChefs = [.. AllChefs.Where(c => !knownIds.Contains(c.Id) || availableIds.Contains(c.Id))];

            await RefreshSessionsAsync();
        }

        private async Task RefreshSessionsAsync()
        {
            IsUpdating = true;
            try
            {
                IReadOnlyList<CrepePartySessionDto> sessions = await _crepePartyService.GetSessionsAsync();
                _allSessions = [.. sessions.Select(s => s.ToGroup())];
                ApplyFilter();
            }
            finally
            {
                IsUpdating = false;
            }
        }

        partial void OnShowOnlyCurrentChanged(bool value) => ApplyFilter();

        private void ApplyFilter()
        {
            Sessions = ShowOnlyCurrent
                ? [.. _allSessions.Take(1)]
                : _allSessions;
        }

        private bool CanGetNewChef() => AvailableChefs.Count > 0;

        [RelayCommand(CanExecute = nameof(CanGetNewChef))]
        public async Task GetNewChef()
        {
            try
            {
                ChefSelectionDto next =
                    await _rotationService.GetNextChefAsync([.. AvailableChefs.Select(c => c.Id)]);

                await _crepePartyService.AddAsync(new AddCrepePartyDto
                {
                    ChefId = next.Chef.Id,
                    SessionNumber = next.SessionNumber,
                });

                await RefreshSessionsAsync();

                await _dialogPresenter.ShowSuccessAsync(
                    title: UiResources.Today_s_Chef,
                    message: next.Chef.FullName);
            }
            catch (NoChefException)
            {
                await _dialogPresenter.ShowWarningAsync(
                    title: UiResources.NoChefInDB,
                    message: UiResources.NeedAtLeastOneChef);
            }
            catch (NoChefSelectionException)
            {
                await _dialogPresenter.ShowWarningAsync(
                    title: UiResources.Error,
                    message: UiResources.NoChefSelectionExceptionMessage);
            }
            catch (Exception ex)
            {
                await _dialogPresenter.ShowWarningAsync(
                    title: UiResources.Error,
                    message: ex.Message);
            }
        }

        private bool CanSelectAvailableChefs() => IsMoreThanOneChef;

        [RelayCommand(CanExecute = nameof(CanSelectAvailableChefs))]
        public async Task SelectAvailableChefs()
        {
            try
            {
                IReadOnlyList<ChefDto>? selected =
                    await _selectionPresenter.SelectChefsAsync(
                        title: UiResources.ChefsPresent,
                        message: UiResources.SelectAvailableChefs,
                        allChefs: AllChefs,
                        selectedChefs: AvailableChefs);

                if (selected is null)
                {
                    return;
                }

                if (selected.Count == 0)
                {
                    await _dialogPresenter.ShowWarningAsync(
                        title: UiResources.NoChefSelected,
                        message: UiResources.SelectAtLeastOneChef);
                    return;
                }

                AvailableChefs = selected;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{UiResources.UnexpectedError}: {ex}");
            }
        }

        [RelayCommand]
        public void ToggleSwitch() => ShowOnlyCurrent = !ShowOnlyCurrent;
    }
}
