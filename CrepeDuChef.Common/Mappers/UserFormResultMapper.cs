using CrepeDuChef.Common.DTOs;
using CrepeDuChef.Common.Interfaces;
using CrepeDuChef.Common.Models;

namespace CrepeDuChef.Common.Mappers
{
    public class UserFormResultMapper : IUserFormResultMapper
    {
        public UserDataResult Map(List<string?>? results)
        {
            if (results is null)
            {
                return new UserDataResult
                {
                    Status = FormResultStatus.Cancelled
                };
            }

            FormResultStatus dataExtractionStatus =
                TryExtractUser(results, out var firstName, out var lastName);

            return dataExtractionStatus switch
            {
                FormResultStatus.Invalid =>
                    new UserDataResult
                    {
                        Status = FormResultStatus.Invalid,
                    },
                FormResultStatus.InvalidDataForm =>
                    new UserDataResult
                    {
                        Status = FormResultStatus.InvalidDataForm,
                    },
                _ =>
                    new UserDataResult
                    {
                        Status = FormResultStatus.Success,
                        FirstNameUpdate = firstName,
                        LastNameUpdate = lastName
                    },
            };
        }

        public UserDataResult Map(UserDto? user)
        {
            if (user is null)
            {
                return new UserDataResult
                {
                    Status = FormResultStatus.Cancelled
                };
            }

            FormResultStatus status = IsValid(user);

            return status switch
            {
                FormResultStatus.Invalid =>
                    new UserDataResult
                    {
                        Status = FormResultStatus.Invalid,
                    },
                FormResultStatus.InvalidDataForm =>
                    new UserDataResult
                    {
                        Status = FormResultStatus.InvalidDataForm,
                    },
                _ =>
                    new UserDataResult
                    {
                        Status = FormResultStatus.Success,
                        FirstNameUpdate = user.FirstName,
                        LastNameUpdate = user.LastName,
                    },
            };
        }

        private static FormResultStatus TryExtractUser(
            List<string?> results,
            out string firstName,
            out string lastName)
        {
            firstName = string.Empty;
            lastName = string.Empty;

            if (results.Count < 2)
                return FormResultStatus.InvalidDataForm;

            if (string.IsNullOrWhiteSpace(results[0]) ||
                string.IsNullOrWhiteSpace(results[1]))
                return FormResultStatus.Invalid;

            firstName = results[0]!;
            lastName = results[1]!;

            return FormResultStatus.Success;
        }

        private static FormResultStatus IsValid(UserDto user)
        {
            if (string.IsNullOrWhiteSpace(user.FirstName) ||
                string.IsNullOrWhiteSpace(user.LastName))
                return FormResultStatus.Invalid;
            return FormResultStatus.Success;
        }

    }
}
