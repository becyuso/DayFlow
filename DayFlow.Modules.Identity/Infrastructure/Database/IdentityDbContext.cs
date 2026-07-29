using Microsoft.EntityFrameworkCore;
using DayFlow.Modules.Identity.Domain.Users;

namespace DayFlow.Modules.Identity.Infrastructure.Database
{
    public class IdentityDbContext : DbContext
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {
        }

        #region DbSets

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<SecuritySetting> SecuritySettings { get; set; } = null!;

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(b =>
            {
                // 正確指定 schema 與 table
                b.ToTable("users", "identity");

                // primary key maps to user_id (資料表未設定 IDENTITY in script)
                b.HasKey(x => x.UserId).HasName("PK_identity.users_1");
                b.Property(x => x.UserId).HasColumnName("user_id").ValueGeneratedNever();

                b.Property(x => x.PublicId).HasColumnName("public_id").IsRequired();
                b.Property(x => x.Email).HasColumnName("email").HasMaxLength(100).IsRequired();
                b.Property(x => x.PasswordHash).HasColumnName("password_hash").HasMaxLength(300).IsRequired();
                b.Property(x => x.DisplayName).HasColumnName("display_name").HasMaxLength(30).IsRequired();
                b.Property(x => x.EmailVerified).HasColumnName("email_verified");
                b.Property(x => x.Status).HasColumnName("status");
                b.Property(x => x.LastLoginAt).HasColumnName("last_login_at").HasColumnType("datetime2");
                b.Property(x => x.CreatedAt).HasColumnName("created_at").HasColumnType("datetime2");
                b.Property(x => x.UpdatedAt).HasColumnName("updated_at").HasColumnType("datetime2");
                b.Property(x => x.DeletedAt).HasColumnName("deleted_at").HasColumnType("datetime2");
                //b.Navigation(x => x.SecuritySetting).AutoInclude(); 改用Include(x => x.SecuritySetting)

                b.HasOne(x => x.SecuritySetting)
                .WithOne(x => x.User)
                .HasForeignKey<SecuritySetting>(
                    x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<SecuritySetting>(b =>
            {
                b.ToTable("user_security_settings", "identity");
                // 使用 UserId 作為 PK
                // 因為 SecuritySetting 是 User Aggregate 內部 Entity
                b.HasKey(x => x.UserId).HasName("PK_identity.user_security_settings");
                b.Property(x => x.UserId).HasColumnName("user_id").ValueGeneratedNever();
                b.Property(x => x.TwoFactorEnabled).HasColumnName("two_factor_enabled").IsRequired();
                b.Property(x => x.FailedLoginCount).HasColumnName("failed_login_count").IsRequired();
                b.Property(x => x.LockedUntil).HasColumnName("locked_until").HasColumnType("datetime2");
                b.Property(x => x.LastPasswordChangedAt).HasColumnName("last_password_changed_at").HasColumnType("datetime2");
                b.Property(x => x.CreatedAt).HasColumnName("created_at").HasColumnType("datetime2");
                b.Property(x => x.UpdatedAt).HasColumnName("updated_at").HasColumnType("datetime2");
            });

        }
    }
}
