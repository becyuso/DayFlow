using DayFlow.BuildingBlocks.Domain.Exceptions;

namespace DayFlow.Modules.Identity.Domain.Users
{
    // Represents identity.user_security_settings
    public class SecuritySetting
    {
        public long UserId { get; private set; }

        public bool TwoFactorEnabled { get; private set; }

        public int FailedLoginCount { get; private set; }

        public DateTime? LockedUntil { get; private set; }

        public DateTime? LastPasswordChangedAt { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        #region Navigation

        public User? User { get; private set; }

        #endregion


        #region Constructors

        // EF Core
        protected SecuritySetting()
        {
        }

        internal SecuritySetting(
            long userId,
            DateTime createdAt)
        {
            //if (securitySettingId <= 0)
            //    throw new DomainException(
            //        "SecuritySettingId is required.");

            if (userId <= 0)
                throw new DomainException(
                    "UserId is required.");

            UserId = userId;

            TwoFactorEnabled = false;

            FailedLoginCount = 0;

            CreatedAt = createdAt;
            UpdatedAt = createdAt;
        }

        #endregion


        #region Domain Behavior

        public void EnableTwoFactor(
            DateTime now)
        {
            TwoFactorEnabled = true;

            UpdatedAt = now;
        }

        public void DisableTwoFactor(
            DateTime now)
        {
            TwoFactorEnabled = false;

            UpdatedAt = now;
        }

        public void RecordLoginFailure(
            int maxFailedAttempts,
            DateTime now)
        {
            FailedLoginCount++;

            if (FailedLoginCount >= maxFailedAttempts)
            {
                LockedUntil = now.AddMinutes(30);
            }

            UpdatedAt = now;
        }

        public void ResetLoginFailure(
            DateTime now)
        {
            FailedLoginCount = 0;

            LockedUntil = null;

            UpdatedAt = now;
        }

        public void ChangePassword(
            DateTime now)
        {
            FailedLoginCount = 0;

            LockedUntil = null;

            LastPasswordChangedAt = now;

            UpdatedAt = now;
        }

        #endregion


        #region Domain Rules

        public bool IsLocked(
            DateTime now)
        {
            if (LockedUntil == null)
                return false;


            return LockedUntil > now;
        }

        #endregion
    }
}