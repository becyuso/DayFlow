
using FeatureLogin = DayFlow.Modules.Identity.Application.Features.Authentication.Login.Login;

namespace DayFlow.Modules.Identity.Presentation.Api.Authentication.Login
{
    public static class Mapping
    {

        public static FeatureLogin.Command ToCommand(
            this LoginRequest request)
        {
            return new FeatureLogin.Command
            (
                request.Account,
                request.Password
            );
        }



        public static LoginResponse ToResponse(
            this FeatureLogin.Result result)
        {
            return new LoginResponse
            (
                result.UserId,
                result.PublicId,
                result.Email,
                result.DisplayName
            );
        }

    }
}