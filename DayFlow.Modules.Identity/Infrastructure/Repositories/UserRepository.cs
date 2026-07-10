using Microsoft.EntityFrameworkCore;
using DayFlow.Modules.Identity.Domain.Entities;
using DayFlow.Modules.Identity.Infrastructure.Database;

namespace DayFlow.Modules.Identity.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string? email);
    }

    public class UserRepository : IUserRepository
    {
        private readonly IdentityDbContext _db;

        public UserRepository(IdentityDbContext db)
        {
            _db = db;
        }

        public async Task<User?> GetByEmailAsync(string? email)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
