using DayFlow.Modules.Identity.Application.Features.Authentication.Login;

namespace DayFlow.Modules.Identity.Presentation.Api.Authentication.Login
{
    public static class Mapping
    {

        public static LoginCommand ToCommand(
            this LoginRequest request)
        {
            return new LoginCommand
            (
                request.Account,
                request.Password
            );
        }



        public static LoginResponse ToResponse(
            this LoginResult result)
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