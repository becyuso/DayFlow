using DayFlow.Modules.Identity.Domain.Users;

namespace DayFlow.Test.DayFlow.Modules.Identity.Test.TestData.Builders
{
    public class UserBuilder
    {
        private Guid _publicId = Guid.NewGuid();
        private string _email = "default@test.com";
        private string _passwordHash = "hashed";
        private string _displayName = "TestUser";
        private short _status = 0;
        private DateTime _lastLoginAt = DateTime.UtcNow;

        public UserBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }

        public UserBuilder WithDisplayName(string name)
        {
            _displayName = name;
            return this;
        }

        public UserBuilder WithStatus(short status)
        {
            _status = status;
            return this;
        }

        public UserBuilder WithLastLogin(DateTime time)
        {
            _lastLoginAt = time;
            return this;
        }

        public User Build()
        {
            var user = User.Create(
                _publicId,
                _email,
                _passwordHash,
                _displayName
            );

            user.UpdateLastLogin();

            // 如果你還沒 domain method，可以先 reflection（不建議長期）
            if (_status != 0)
            {
                typeof(User)
                    .GetProperty("Status")!
                    .SetValue(user, _status);
            }

            return user;
        }
    }
}
