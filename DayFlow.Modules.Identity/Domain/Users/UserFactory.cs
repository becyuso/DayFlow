using DayFlow.Modules.Identity.Application.Security;

namespace DayFlow.Modules.Identity.Domain.Users
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
            return Domain.Users.User.Create(
                publicId,
                email,
                _hasher.Hash(displayName),
                hashPassword);
        }
    }
}
