using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagement_API.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace TaskManagement_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {

        private readonly TaskManagementDbContext taskManagementDbContext;
        public UsersController(TaskManagementDbContext taskManagementDbContext)
        {
            this.taskManagementDbContext = taskManagementDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            var result = await taskManagementDbContext.Users.ToListAsync();

            return Ok(result);
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> getUserByEmail(string email)
        {
            var user = await taskManagementDbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null) return NotFound("Task not found");
            return Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> Register([FromBody] LoginModel login)
        {
            try
            {
                if (string.IsNullOrEmpty(login.Email))
                    return NotFound("Email invalid");

                var user = await taskManagementDbContext.Users.FirstOrDefaultAsync(x => x.Email == login.Email);
                if (user != null) return NotFound("Email already exists");

                byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: login.Password!,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8));

                user = new User();
                user.Id = Guid.NewGuid();
                user.Email = login.Email;
                user.Password = hashed;
                user.Salt = Convert.ToBase64String(salt);
                user.CreateDate = DateTime.Now;
                user.Active = 1;
                user.CreateUser = user.Id;
                user.Role = 0;

                taskManagementDbContext.Add(user);
                taskManagementDbContext.SaveChanges();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            try
            {
                if (string.IsNullOrEmpty(login.Email))
                    return NotFound("Email invalid");

                var user = await taskManagementDbContext.Users.FirstOrDefaultAsync(x => x.Email == login.Email);
                if (user == null) return NotFound("Email not exist");


                byte[] salt = Convert.FromBase64String(user.Salt);
                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: login.Password!,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8));

                return Ok(hashed == user.Password);
            }
            catch (Exception ex)
            {
                return NotFound("Error");
            }
        }

    }
}
