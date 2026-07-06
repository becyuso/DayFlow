using DayFlow.Modules.Identity.Application.Security;
using DayFlow.Modules.Identity.Domain.Entities;

namespace DayFlow.Modules.Identity.Domain.Factory
{
    public class UserFactory
    {
        private readonly IPasswordHasher _hasher;

        public UserFactory(IPasswordHasher hasher)
        {
            _hasher = hasher;
        }

        public User Create(Guid publicId,
                           string email,
                           string hashPassword,
                           string displayName)
        {
            return User.Create(
                publicId,
                email,
                _hasher.Hash(displayName),
                hashPassword);
        }
    }
}
