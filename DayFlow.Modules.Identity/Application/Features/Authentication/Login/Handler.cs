using DayFlow.Modules.Identity.Application.Security;
using DayFlow.Modules.Identity.Infrastructure.Repositories;
using MediatR;

namespace DayFlow.Modules.Identity.Application.Features.Authentication.Login
{
    public class Handler : IRequestHandler<Login.Command, Login.Result>
    {
        private readonly IUserRepository _repo;
        private readonly IPasswordHasher _passwordHasher;

        public Handler(IUserRepository repo,
                       IPasswordHasher passwordHasher)
        {
            _repo = repo;
            _passwordHasher = passwordHasher;
        }

        public async Task<Login.Result> Handle(Login.Command request, CancellationToken cancellationToken)
        {
            var user = await _repo.GetByEmailAsync(request.Email);

            if (user == null)
                return Login.Result.Fail("Invalid credentials");

            bool valid = _passwordHasher.Verify(request.Password, user.PasswordHash);

            if (!valid)
                return Login.Result.Fail("Invalid credentials");
            //    throw new UnauthorizedAccessException("Invalid credentials");

            //var token = _tokenService.CreateToken(user);

            return Login.Result.Ok(user.UserId, user.PublicId, user.Email, user.DisplayName);
        }
    }
}
