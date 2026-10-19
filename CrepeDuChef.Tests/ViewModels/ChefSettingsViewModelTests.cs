using CrepeDuChef.Application.Common;
using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.ViewModels.Models;
using CrepeDuChef.ViewModels.Presenters;
using CrepeDuChef.ViewModels.ViewModels;
using FluentAssertions;
using NSubstitute;

namespace CrepeDuChef.Tests.ViewModels
{
    public class ChefSettingsViewModelTests
    {
        private static readonly ChefDto John = new() { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe" };

        private readonly IChefService _chefService = Substitute.For<IChefService>();
        private readonly IChefFormPresenter _formPresenter = Substitute.For<IChefFormPresenter>();
        private readonly IDialogPresenter _dialogPresenter = Substitute.For<IDialogPresenter>();
        private readonly ChefSettingsViewModel _vm;

        public ChefSettingsViewModelTests()
        {
            _chefService.GetAllAsync().Returns([John]);
            _vm = new ChefSettingsViewModel(_chefService, _formPresenter, _dialogPresenter);
        }

        [Fact]
        public async Task AddChef_Should_Do_Nothing_When_Form_Is_Cancelled()
        {
            // Arrange
            _formPresenter.ShowAddChefFormAsync().Returns((ChefFormData?)null);

            // Act
            await _vm.AddChefCommand.ExecuteAsync(null);

            // Assert
            await _chefService.DidNotReceiveWithAnyArgs().AddAsync(default!);
        }

        [Fact]
        public async Task AddChef_Should_Show_Errors_When_Service_Fails()
        {
            // Arrange
            _formPresenter.ShowAddChefFormAsync().Returns(new ChefFormData("", "Doe"));
            _chefService.AddAsync(default!).ReturnsForAnyArgs(Result<ChefDto>.Failure(["FirstNameRequired"]));

            // Act
            await _vm.AddChefCommand.ExecuteAsync(null);

            // Assert
            await _dialogPresenter.Received(1).ShowErrorAsync(Arg.Any<string>(), "FirstNameRequired");
        }

        [Fact]
        public async Task EditChef_Should_Not_Call_Service_When_Names_Are_Unchanged()
        {
            // Arrange
            _vm.SelectedChef = John;
            _formPresenter.ShowUpdateChefFormAsync(John).Returns(new ChefFormData("John", "Doe"));

            // Act
            await _vm.EditChefCommand.ExecuteAsync(null);

            // Assert
            await _chefService.DidNotReceiveWithAnyArgs().UpdateAsync(default!);
            _vm.SelectedChef.Should().BeNull();
        }

        [Fact]
        public async Task EditChef_Should_Update_And_Reload_When_Names_Change()
        {
            // Arrange
            _vm.SelectedChef = John;
            _formPresenter.ShowUpdateChefFormAsync(John).Returns(new ChefFormData("Jane", "Doe"));

            ChefDto jane = John with { FirstName = "Jane" };
            _chefService.UpdateAsync(default!).ReturnsForAnyArgs(Result<ChefDto>.Success(jane));

            // Act
            await _vm.EditChefCommand.ExecuteAsync(null);

            // Assert
            await _chefService.Received(1).UpdateAsync(new UpdateChefDto { Id = John.Id, FirstName = "Jane", LastName = "Doe" });
            await _chefService.Received(1).GetAllAsync();
        }

        [Fact]
        public async Task EditChef_Should_Edit_The_Chef_Given_As_Parameter()
        {
            // Arrange: row button, no list selection
            _formPresenter.ShowUpdateChefFormAsync(John).Returns(new ChefFormData("Jane", "Doe"));
            _chefService.UpdateAsync(default!).ReturnsForAnyArgs(Result<ChefDto>.Success(John with { FirstName = "Jane" }));

            // Act
            await _vm.EditChefCommand.ExecuteAsync(John);

            // Assert
            await _chefService.Received(1).UpdateAsync(new UpdateChefDto { Id = John.Id, FirstName = "Jane", LastName = "Doe" });
        }
    }
}
