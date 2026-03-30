using CrepeDuChef.Application.DTOs;
using CrepeDuChef.Application.Interfaces;
using CrepeDuChef.Application.ValueObjects;
using FluentValidation;

namespace CrepeDuChef.Application.Ochestrators
{

    public class UserApplicationOrchestrator : IUserApplicationOrchestrator
    {
        private readonly IChefManagementService _chefService;

        public UserApplicationOrchestrator(
            IChefManagementService chefService)
        {
            _chefService = chefService;
        }

        public async Task<UserOperationResult> AddUserAsync(UserFormData userFormData)
        {
            try
            {
                UserDtoAdd userDtoAdd = new()
                {
                    FirstName = userFormData.FirstName,
                    LastName = userFormData.Lastname,
                };

                UserDto? newUser =
                    await _chefService.AddUserAsync(userDtoAdd);

                return new UserOperationResult(OperationStatus.Success, "", newUser);
            }
            catch (ValidationException ex)
            {
                return new UserOperationResult(OperationStatus.Failure, string.Join("\n", ex.Errors.Select(e => e.ErrorMessage)), null);
            }
            catch (Exception ex)
            {
                return new UserOperationResult(OperationStatus.Failure, "Unexpected error: " + ex.Message, null);
            }
        }

        public async Task<UserOperationResult> UpdateUserAsync(UserDto existingUser, UserFormData userFormData)
        {
            // no change
            if(existingUser.FirstName == userFormData.FirstName
                && existingUser.LastName == userFormData.Lastname)
            {
                return new UserOperationResult(OperationStatus.Canceled, "", existingUser);
            }

            try
            {
                UserDtoUpdate userDtoUpdate = new()
                {
                    Id = existingUser.Id,
                    FirstName = userFormData.FirstName,
                    LastName = userFormData.Lastname,
                };

                UserDto? updatedUser =
                    await _chefService.UpdateUserAsync(existingUser, userDtoUpdate);

                return new UserOperationResult(OperationStatus.Success, "", updatedUser);
            }
            catch (ValidationException ex)
            {
                string error = string.Join("\n", ex.Errors.Select(e => e.ErrorMessage));
                return new UserOperationResult(OperationStatus.Failure, error, null);
            }
            catch (Exception ex)
            {
                return new UserOperationResult(OperationStatus.Failure, "Unexpected error: " + ex.Message, null);
            }
        }
    }
}
