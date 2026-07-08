using DayFlow.Modules.Identity.Application.Security;
using DayFlow.Modules.Identity.Infrastructure.Repositories;
using DayFlow.Modules.Identity.Infrastructure.Security;
using MediatR;

namespace DayFlow.Modules.Identity.Application.Commands
{
    public class LoginHandler : IRequestHandler<LoginCommand, LoginResult>
    {
        private readonly IUserRepository _repo;
        private readonly IPasswordHasher _passwordHasher;

        public LoginHandler(IUserRepository repo,
                            IPasswordHasher passwordHasher)
        {
            _repo = repo;
            _passwordHasher = passwordHasher;
        }

        public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _repo.GetByEmailAsync(request.Email);

            if (user == null)
                return LoginResult.Fail("Invalid credentials");

            bool valid = _passwordHasher.Verify(request.Password, user.PasswordHash);

            if (!valid)
                return LoginResult.Fail("Invalid credentials");
            //    throw new UnauthorizedAccessException("Invalid credentials");

            //var token = _tokenService.CreateToken(user);

            return LoginResult.Ok(user.Email, user.DisplayName);
        }
    }
}
