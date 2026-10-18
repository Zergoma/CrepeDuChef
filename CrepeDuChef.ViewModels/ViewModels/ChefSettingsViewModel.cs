using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrepeDuChef.Application.Common;
using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.ViewModels.Models;
using CrepeDuChef.ViewModels.Presenters;
using CrepeDuChef.Localization.Resources;

namespace CrepeDuChef.ViewModels.ViewModels
{
    /// <summary>
    /// Lists the chefs and lets the user add or edit them.
    /// </summary>
    public partial class ChefSettingsViewModel : ObservableObject
    {
        private readonly IChefService _chefService;
        private readonly IChefFormPresenter _formPresenter;
        private readonly IDialogPresenter _dialogPresenter;

        [ObservableProperty]
        public partial IReadOnlyList<ChefDto> Chefs { get; private set; } = [];

        [ObservableProperty]
        public partial ChefDto? SelectedChef { get; set; }

        public string UserSettingMsg => UiResources.UserSetting;
        public string AddMsg => UiResources.Add;
        public string NoChefMsg => UiResources.NoChefInDB;

        public ChefSettingsViewModel(
            IChefService chefService,
            IChefFormPresenter formPresenter,
            IDialogPresenter dialogPresenter)
        {
            _chefService = chefService;
            _formPresenter = formPresenter;
            _dialogPresenter = dialogPresenter;
        }

        [RelayCommand]
        public async Task LoadChefs()
        {
            Chefs = await _chefService.GetAllAsync();
        }

        [RelayCommand]
        public async Task AddChef()
        {
            ChefFormData? form = await _formPresenter.ShowAddChefFormAsync();
            if (form is null)
            {
                return;
            }

            await RunAsync(
                () => _chefService.AddAsync(new AddChefDto { FirstName = form.FirstName, LastName = form.LastName }),
                showSuccess: false);
        }

        [RelayCommand]
        public async Task EditChef()
        {
            if (SelectedChef is not ChefDto chef)
            {
                return;
            }

            // Clear the selection so the same chef can be picked again afterwards
            SelectedChef = null;

            ChefFormData? form = await _formPresenter.ShowUpdateChefFormAsync(chef);
            if (form is null || (form.FirstName == chef.FirstName && form.LastName == chef.LastName))
            {
                return;
            }

            await RunAsync(
                () => _chefService.UpdateAsync(new UpdateChefDto { Id = chef.Id, FirstName = form.FirstName, LastName = form.LastName }),
                showSuccess: true);
        }

        private async Task RunAsync(Func<Task<Result<ChefDto>>> operation, bool showSuccess)
        {
            try
            {
                Result<ChefDto> result = await operation();

                if (!result.IsSuccess)
                {
                    await _dialogPresenter.ShowErrorAsync(
                        title: UiResources.OperationCanceled,
                        message: string.Join(Environment.NewLine, result.Errors));
                    return;
                }

                if (showSuccess)
                {
                    await _dialogPresenter.ShowSuccessAsync(
                        title: UiResources.Updated,
                        message: UiResources.InformationUpdated);
                }

                await LoadChefs();
            }
            catch (Exception ex)
            {
                await _dialogPresenter.ShowErrorAsync(
                    title: UiResources.UnexpectedError,
                    message: ex.Message);
            }
        }
    }
}
