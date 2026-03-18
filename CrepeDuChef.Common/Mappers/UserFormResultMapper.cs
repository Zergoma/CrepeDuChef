using CrepeDuChef.Common.DTOs;
using CrepeDuChef.Common.Interfaces;

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
                FormResultStatus.Invalid or FormResultStatus.InvalidDataForm =>
                    new UserDataResult
                    {
                        Status = dataExtractionStatus,
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
    }
}
