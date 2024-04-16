using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DotnetCoreApi.Data;
using Start_DotnetCoreApi.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using DotnetCoreApi.Models;

namespace Star_DotnetCoreApi.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _Config;
        private readonly ApplicationDbContext _dbContext;

        public AuthController(IConfiguration configuration, ApplicationDbContext dbContext)
        {
            this._Config = configuration;
            this._dbContext = dbContext;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _dbContext.Users.Include(p=>p.Role).FirstOrDefaultAsync(p => p.UserName == request.username);
            if (user == null)
                return NotFound();

            if (!IsuserValid(user, request))
                return Forbid();

            var Key = _Config.GetSection("Jwt:Key").Get<string>();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, Convert.ToBase64String(Encoding.UTF8.GetBytes(user.Id.ToString()))),
                new Claim(ClaimTypes.Role, user.Role.RoleName)
            };

            var Sectoken = new JwtSecurityToken(_Config["Jwt:Issuer"],
                _Config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

            return Ok(new
            {
                status = "OK",
                token = token
            });
        }

        private bool IsuserValid(User user, LoginRequest request)
            => user.UserName == request.username && request.password == user.PassWord;

    }

}
