using CrepeDuChef.Common;
using CrepeDuChef.Common.DTOs;
using CrepeDuChef.Common.Interfaces;
using CrepeDuChef.Common.Models;
using CrepeDuChef.Maui.Resources.languages;

namespace CrepeDuChef.Maui.Mappers
{
    public class LocalizedUserFormResultMapper : IUserFormResultMapper
    {
        public readonly IUserFormResultMapper _inner;

        public LocalizedUserFormResultMapper(IUserFormResultMapper inner)
        {
            _inner = inner;
        }


        public UserDataResult Map(List<string?>? results)
        {
            UserDataResult result =
                _inner.Map(results);

            switch (result.Status)
            {
                case FormResultStatus.Invalid:
                    result.ErrorMessage = Traduction.YouMustEnterFirstAndLastName;
                    break;
                case FormResultStatus.InvalidDataForm:
                    result.ErrorMessage = Traduction.InvalidUserFormData;
                    break;

                case FormResultStatus.Cancelled:
                case FormResultStatus.Success:
                default:
                    break;
            }

            return result;
        }

        public UserDataResult Map(UserDto? results)
        {
            UserDataResult result =
                _inner.Map(results);

            switch (result.Status)
            {
                case FormResultStatus.Invalid:
                    result.ErrorMessage = Traduction.YouMustEnterFirstAndLastName;
                    break;
                case FormResultStatus.InvalidDataForm:
                    result.ErrorMessage = Traduction.InvalidUserFormData;
                    break;

                case FormResultStatus.Cancelled:
                case FormResultStatus.Success:
                default:
                    break;
            }

            return result;
        }
    }
}
