namespace DayFlow.Modules.Identity.Domain.Users
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailReadOnlyAsync(string email, CancellationToken cancellationToken = default);
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    }
}
