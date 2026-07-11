using DayFlow.Modules.Identity.Application.Security;
using DayFlow.Modules.Identity.Infrastructure.Repositories;
using MediatR;

namespace DayFlow.Modules.Identity.Application.Features.Authentication.Login
{
    public class Handler : IRequestHandler<Command, Result>
    {
        private readonly IUserRepository _repo;
        private readonly IPasswordHasher _passwordHasher;

        public Handler(IUserRepository repo,
                            IPasswordHasher passwordHasher)
        {
            _repo = repo;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _repo.GetByEmailAsync(request.Email);

            if (user == null)
                return Result.Fail("Invalid credentials");

            bool valid = _passwordHasher.Verify(request.Password, user.PasswordHash);

            if (!valid)
                return Result.Fail("Invalid credentials");
            //    throw new UnauthorizedAccessException("Invalid credentials");

            //var token = _tokenService.CreateToken(user);

            return Result.Ok(user.UserId, user.PublicId, user.Email, user.DisplayName);
        }
    }
}
