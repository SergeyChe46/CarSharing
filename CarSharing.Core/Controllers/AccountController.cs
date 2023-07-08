using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarSharing.Entities;
using CarSharing.Entities.Identity;
using CarSharing.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarSharing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenCreationService _jwtService;
        public AccountController(UserManager<IdentityUser> userManager, ITokenCreationService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userManager.CreateAsync(
                new IdentityUser { UserName = user.UserName, Email = user.Email },
                user.Password
            );
            if (!result.Succeeded)
            {
                return BadRequest(result);
            }
            user.Password = null;
            return CreatedAtAction(nameof(GetUser), new { name = user.UserName }, user);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<User>> GetUser(string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            if (user == null)
            {
                return NotFound();
            }
            return new User
            {
                UserName = user.UserName,
                Email = user.Email
            };
        }

        [HttpPost("token")]
        public async Task<ActionResult<AuthentificationResponse>> CreateToken(AuthentificationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Неверные имя/пароль.");
            }
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return BadRequest($"Пользователь {request.UserName} не найден.");
            }
            var isValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isValidPassword)
            {
                return BadRequest("Неверный пароль.");
            }
            var token = _jwtService.CreateToken(user);
            return Ok(token);
        }
    }
}
