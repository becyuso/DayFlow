
using DayFlow.Modules.Identity.Application.Features.Authentication.Login;

namespace DayFlow.Modules.Identity.Presentation.Api.Authentication.Login;

public static class Mapping
{

    public static Command ToCommand(
        this LoginRequest request)
    {
        return new Command
        (
            request.Account,
            request.Password
        );
    }



    public static LoginResponse ToResponse(
        this Result result)
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