using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Tests.Helpers;
using CrepeDuChef.ViewModels.Presenters;
using CrepeDuChef.ViewModels.ViewModels;
using FluentAssertions;
using NSubstitute;

namespace CrepeDuChef.Tests.ViewModels
{
    public class CrepeSessionsViewModelTests
    {
        private readonly IChefService _chefService = Substitute.For<IChefService>();
        private readonly IChefRotationService _rotationService = Substitute.For<IChefRotationService>();
        private readonly ICrepePartyService _crepePartyService = Substitute.For<ICrepePartyService>();
        private readonly IDialogPresenter _dialogPresenter = Substitute.For<IDialogPresenter>();
        private readonly IChefSelectionPresenter _selectionPresenter = Substitute.For<IChefSelectionPresenter>();
        private readonly CrepeSessionsViewModel _vm;

        public CrepeSessionsViewModelTests()
        {
            _crepePartyService.GetSessionsAsync().Returns([]);
            _vm = new CrepeSessionsViewModel(_chefService, _rotationService, _crepePartyService, _dialogPresenter, _selectionPresenter);
        }

        private static ChefDto Chef(int id) => new() { Id = TestData.IntToGuid(id), FirstName = $"F{id}", LastName = $"L{id}" };

        [Fact]
        public async Task OnAppearingAsync_Should_Make_All_Chefs_Available_The_First_Time()
        {
            // Arrange
            _chefService.GetAllAsync().Returns([Chef(1), Chef(2)]);

            // Act
            await _vm.OnAppearingAsync();

            // Assert
            _vm.AvailableChefs.Should().Equal(Chef(1), Chef(2));
        }

        [Fact]
        public async Task OnAppearingAsync_Should_Keep_Absent_Chefs_Absent_And_Add_New_Chefs_As_Available()
        {
            // Arrange: chef 2 marked absent
            _chefService.GetAllAsync().Returns([Chef(1), Chef(2)]);
            await _vm.OnAppearingAsync();

            _selectionPresenter
                .SelectChefsAsync(default!, default!, default!, default!)
                .ReturnsForAnyArgs([Chef(1)]);
            await _vm.SelectAvailableChefsCommand.ExecuteAsync(null);

            // Chef 3 added meanwhile
            _chefService.GetAllAsync().Returns([Chef(1), Chef(2), Chef(3)]);

            // Act
            await _vm.OnAppearingAsync();

            // Assert
            _vm.AllChefs.Should().HaveCount(3);
            _vm.AvailableChefs.Should().Equal(Chef(1), Chef(3));
        }

        [Fact]
        public async Task GetNewChef_Should_Draw_Among_Available_Chefs_And_Record_The_Party()
        {
            // Arrange
            _chefService.GetAllAsync().Returns([Chef(1), Chef(2)]);
            await _vm.OnAppearingAsync();

            _rotationService.GetNextChefAsync(default).ReturnsForAnyArgs(new ChefSelectionDto(Chef(2), 5));

            // Act
            await _vm.GetNewChefCommand.ExecuteAsync(null);

            // Assert
            await _rotationService.Received(1).GetNextChefAsync(Arg.Is<IReadOnlyCollection<Guid>>(ids =>
                ids.SequenceEqual(new[] { TestData.IntToGuid(1), TestData.IntToGuid(2) })));
            await _crepePartyService.Received(1).AddAsync(new AddCrepePartyDto { ChefId = TestData.IntToGuid(2), SessionNumber = 5 });
            await _dialogPresenter.Received(1).ShowSuccessAsync(Arg.Any<string>(), "F2 L2");
        }
    }
}
