using DayFlow.BuildingBlocks.Application.Results;
using DayFlow.BuildingBlocks.Infrastructure.Time;
using DayFlow.Modules.Identity.Application.Security;
using DayFlow.Modules.Identity.Domain.Users;
using MediatR;

namespace DayFlow.Modules.Identity.Application.Features.Authentication.Login
{
    public class Handler
        : IRequestHandler<LoginCommand, Result<LoginResult>>
    {
        private readonly IUserRepository _repo;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IClock _iClock;

        public Handler(
            IUserRepository repo,
            IPasswordHasher passwordHasher,
            IClock iClock) 
            => (_repo, _passwordHasher, _iClock) = (repo, passwordHasher, iClock);

        public async Task<Result<LoginResult>> Handle(
            LoginCommand request,
            CancellationToken cancellationToken)
        {
            var user =
                await _repo.GetByEmailAsync(
                    request.Email,
                    cancellationToken);

            if (user == null)
            {
                return Result<LoginResult>.Fail(
                    "Invalid credentials");
            }

            var now = _iClock.TaiwanNow;

            // 1. 檢查帳號鎖定
            if (user.IsLocked(now))
            {
                return Result<LoginResult>.Fail(
                    "Account is locked.");
            }

            // 2. 驗證密碼
            var valid =
                _passwordHasher.Verify(
                    request.Password,
                    user.PasswordHash);

            if (!valid)
            {
                // Aggregate Root 控制 SecuritySetting
                user.RecordLoginFailure(now);

                return Result<LoginResult>.Fail(
                    "Invalid credentials");
            }

            // 3. 登入成功
            user.RecordSuccessfulLogin(now);

            return Result<LoginResult>.Ok(
                string.Empty
                , new LoginResult
                {
                    IsSuccess = true,
                    UserId = user.UserId,
                    PublicId = user.PublicId,
                    Email = user.Email,
                    DisplayName = user.DisplayName
                });
        }
    }
}