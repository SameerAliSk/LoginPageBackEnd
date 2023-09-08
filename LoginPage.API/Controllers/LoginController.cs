using LoginPage.API.Data;
using LoginPage.API.Dto;
using LoginPage.API.Models.Domain;
using LoginPage.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LoginPage.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserLoginDbContext dbContext;
        private readonly ILoginRepository loginRepository;

        public LoginController(UserLoginDbContext dbContext, ILoginRepository loginRepository)
        {
            this.dbContext = dbContext;
            this.loginRepository = loginRepository;
        }

        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            var loginsDomain = await loginRepository.GetAllAsync();
            var loginDto = new List<UserLoginDto>();
            foreach (var login in loginsDomain)
            {
                loginDto.Add(new UserLoginDto()
                {
                    Id = login.Id,
                    FullName = login.FullName,
                    Email = login.Email,
                    Password = login.Password
                });
            }
            return Ok(loginDto);

        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] AddUserLoginDto addUserLoginDto)
        {
            var loginDomainModel = new UserLogin
            {
                FullName = addUserLoginDto.FullName,
                Email = addUserLoginDto.Email,
                Password = addUserLoginDto.Password
            };
            loginDomainModel = await loginRepository.CreateAsync(loginDomainModel);
            if (loginDomainModel != null)
            {
                var loginDto = new UserLoginDto
                {
                    Id = loginDomainModel.Id,
                    FullName = loginDomainModel.FullName,
                    Email = loginDomainModel.Email,
                    Password = loginDomainModel.Password
                };
                return CreatedAtAction(nameof(CreateUser), new { id = loginDomainModel.Id }, loginDto);
            }else
            {
                return BadRequest();
            }
        }
        [HttpPut]
        //[Route("{email}")]
        public async Task<IActionResult> UpdateUserPassword([FromBody] UpdateUserPasswordDto updateUserPasswordDto)
        { 

           var loginDomainModel = new UserLogin
           {
               Email = updateUserPasswordDto.Email,
               Password = updateUserPasswordDto.Password
           };
            loginDomainModel = await loginRepository.UpdateAsync(loginDomainModel);
            if (loginDomainModel == null)
            {
                return NotFound();
            }
            var loginDto = new UserLoginDto
            {
                Id = loginDomainModel.Id,
                FullName = loginDomainModel.FullName,
                Email = loginDomainModel.Email,
                Password = loginDomainModel.Password
            };
            return Ok(loginDto);
        }
        [HttpPost]
        [Route("id")]
        public async Task<IActionResult> getUserDataByEmailAndPassword([FromBody] LoginPageDto loginPageDto)
        {
            var loginDomainModel = new UserLogin
            {
                Email = loginPageDto.Email,
                Password = loginPageDto.Password
            };
            loginDomainModel = await loginRepository.GetByEmailAndPasswordAsync(loginDomainModel);
            if (loginDomainModel == null)
            {
                return NotFound();
            }
            var loginDto = new UserLoginDto
            {
                Id = loginDomainModel.Id,
                FullName = loginDomainModel.FullName,
                Email = loginDomainModel.Email,
                Password = loginDomainModel.Password
            };
            return Ok(loginDto);

        }
    }
}
