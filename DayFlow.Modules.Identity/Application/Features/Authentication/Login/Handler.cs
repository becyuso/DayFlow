using DayFlow.Modules.Identity.Application.Security;
using DayFlow.Modules.Identity.Domain.Users;
using MediatR;

namespace DayFlow.Modules.Identity.Application.Features.Authentication.Login
{
    public class Handler : IRequestHandler<LoginCommand, LoginResult>
    {
        private readonly IUserRepository _repo;
        private readonly IPasswordHasher _passwordHasher;

        public Handler(IUserRepository repo, IPasswordHasher passwordHasher) =>
            (_repo, _passwordHasher) = (repo, passwordHasher);

        public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _repo.GetByEmailReadOnlyAsync(request.Email!, cancellationToken);

            if (user == null)
                return LoginResult.Fail("Invalid credentials");

            bool valid = _passwordHasher.Verify(request.Password, user.PasswordHash);

            if (!valid)
                return LoginResult.Fail("Invalid credentials");
            //    throw new UnauthorizedAccessException("Invalid credentials");

            //var token = _tokenService.CreateToken(user);

            return LoginResult.Ok(user.UserId, user.PublicId, user.Email, user.DisplayName);
        }
    }
}
