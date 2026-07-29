using Microsoft.EntityFrameworkCore;
using DayFlow.Modules.Identity.Infrastructure.Database;
using DayFlow.Modules.Identity.Domain.Users;

namespace DayFlow.Modules.Identity.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityDbContext _db;

        public UserRepository(IdentityDbContext db) => _db = db;

        public async Task<User?> GetByEmailReadOnlyAsync(
            string email,
            CancellationToken cancellationToken = default)
        {
            return await _db.Users.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
        }

        public async Task<User?> GetByEmailAsync(
            string email,
            CancellationToken cancellationToken)
        {
            return await _db.Users
                .Include(x => x.SecuritySetting)
                .FirstOrDefaultAsync(
                    x => x.Email == email,
                    cancellationToken);
        }
    }
}
