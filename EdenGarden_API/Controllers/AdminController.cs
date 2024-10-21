using EdenGarden_API.Data;
using EdenGarden_API.DTO;
using EdenGarden_API.Models.Entities;
using EdenGarden_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace EdenGarden_API.Controllers
{
    [Authorize]
    public class AdminController : BaseController
    {
        private readonly DataContext _dataContext;
        private readonly ITokenService _tokenService;
        public AdminController (DataContext dataContext, ITokenService tokenService)
        {
            _dataContext = dataContext;
            _tokenService = tokenService;
        }

        //[AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAccounts()
        {
            var acc = await _dataContext.Admin.ToListAsync();
            return acc;
        }


        [HttpGet]
        public async Task<ActionResult<Admin>> GetAccountByID(int id)
        {
            var acc = await _dataContext.Admin.FindAsync(id);
            return acc;
            //return await _dataContext.Room.FindAsync(id)
        }




        //[AllowAnonymous]
        [HttpPost] //POST: api/account/register
        public async Task<ActionResult<AccountDTO>> Register([FromBody] RegisterDTO registerDTO)
        {
            if (await UserAdminExist(registerDTO.Username))
                return BadRequest("Username is taken");
            using var hmac = new HMACSHA512();

            var admin = new Admin
            {
                Username = registerDTO.Username,
                PassWordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                PassWordSalt = hmac.Key
            };
            _dataContext.Admin.Add(admin);
            await _dataContext.SaveChangesAsync();
            return new AccountDTO
            {
                Username = admin.Username,
                Token = _tokenService.CreateToken(admin)
            };
        }



        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<AccountDTO>> Login ([FromBody] LoginDTO logindto)
        {
            var admin = await _dataContext.Admin.SingleOrDefaultAsync(x => x.Username == logindto.Username);
            if (admin == null)
                return Unauthorized("invalid username");
            using var hmac = new HMACSHA512(admin.PassWordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(logindto.Password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != admin.PassWordHash[i])
                    return Unauthorized("invalid password");
            }
            return new AccountDTO
            {
                Username = admin.Username,
                Token = _tokenService.CreateToken(admin)
            };
        }
            



        private async Task <bool> UserAdminExist (string username)
        {
            return await _dataContext.Admin.AnyAsync(x => x.Username == username.ToLower());
        }
    }
}
