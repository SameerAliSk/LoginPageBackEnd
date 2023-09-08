using LoginPage.API.Data;
using LoginPage.API.Dto;
using LoginPage.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LoginPage.API.Repositories
{
    public class SQLUserLoginRepository : ILoginRepository
    {
        private readonly UserLoginDbContext dbContext;

        public SQLUserLoginRepository(UserLoginDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<UserLogin> CreateAsync(UserLogin userLogin)
        {
            var isUserAvailable = await dbContext.UserLogins.FirstOrDefaultAsync(x => x.Email == userLogin.Email);
            if (isUserAvailable == null)
            {
                await dbContext.UserLogins.AddAsync(userLogin);
                await dbContext.SaveChangesAsync();
                return userLogin;
            }
            return null;
        }

        public async Task<List<UserLogin>> GetAllAsync()
        {
             return await dbContext.UserLogins.ToListAsync();
        }

        public async Task<UserLogin?> GetByEmailAndPasswordAsync(UserLogin userLogin)
        {
            return await dbContext.UserLogins.FirstOrDefaultAsync(x => x.Email == userLogin.Email && x.Password == userLogin.Password);
        }

        public async Task<UserLogin?> UpdateAsync(UserLogin userLogin)
        {
            var loginDomainModel = await dbContext.UserLogins.FirstOrDefaultAsync(x => x.Email == userLogin.Email);
            if (loginDomainModel == null)
            {
                return null;
            }
            loginDomainModel.Password = userLogin.Password;
            await dbContext.SaveChangesAsync();
            return loginDomainModel;

        }
    }
}
