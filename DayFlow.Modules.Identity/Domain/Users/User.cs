namespace DayFlow.Modules.Identity.Domain.Users
{
    public class User
    {
        // 保持型別與資料表對應
        public long UserId { get; private set; }
        public Guid PublicId { get; private set; }
        public string Email { get; private set; } = null!;
        public string PasswordHash { get; private set; } = null!;
        public string DisplayName { get; private set; } = null!;
        public bool EmailVerified { get; private set; }
        public short Status { get; private set; }
        public DateTime LastLoginAt { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public DateTime? DeletedAt { get; private set; }

        // EF 用的 protected ctor
        protected User() { }

        // Factory 用來建立新 User
        public static User Create(Guid publicId,
                                  string email,
                                  string hashPassword,
                                  string displayName)
        {
            var now = DateTime.UtcNow;
            var u = new User
            {
                PublicId = publicId,
                Email = email.Trim(),
                PasswordHash = hashPassword,
                DisplayName = displayName,
                EmailVerified = false,
                Status = 0,
                CreatedAt = now,
                UpdatedAt = now,
                LastLoginAt = now
            };

            return u;
        }

        public void UpdateLastLogin()
        {
            LastLoginAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkDeleted()
        {
            DeletedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
