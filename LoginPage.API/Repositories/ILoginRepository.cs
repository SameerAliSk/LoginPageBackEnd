using LoginPage.API.Models.Domain;

namespace LoginPage.API.Repositories
{
    public interface ILoginRepository
    {
        Task<List<UserLogin>> GetAllAsync();

        Task<UserLogin> CreateAsync(UserLogin userLogin);

        Task<UserLogin?> UpdateAsync(UserLogin userLogin);

        Task<UserLogin?> GetByEmailAndPasswordAsync(UserLogin userLogin);
    }
}
