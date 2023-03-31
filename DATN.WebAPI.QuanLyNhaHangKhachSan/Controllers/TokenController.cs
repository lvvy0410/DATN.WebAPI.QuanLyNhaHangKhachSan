using DTO.Context;
using DTO.Model;
using DTO.Public;
using JWTAuth.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTAuth.WebApi.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly QuanLyNhaHangKhachSanContext _context;

        public TokenController(IConfiguration config, QuanLyNhaHangKhachSanContext context)
        {
            _configuration = config;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(NguoiDungAPI _userData)
        {
            if (_userData != null && _userData.userName != null && _userData.pass != null)
            {
                var user = await GetUser(_userData.userName, _userData.pass);

                if (user != null)
                {
                    //create claims details based on the user information
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                   
                     
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);
                    var itemToken = new ResponseTokenDTO
                    {
                        access_token = new JwtSecurityTokenHandler().WriteToken(token),
                        token_type = "Bearer token"
                    };

                    return Ok(itemToken);
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<NguoiDungAPI> GetUser(string user, string pass)
        {
            try
            {
                NguoiDungAPI nguoiDungAPI = new NguoiDungAPI();
                nguoiDungAPI.userName = "admin";
                nguoiDungAPI.pass = "123456";
                if ( user== nguoiDungAPI.userName && pass == nguoiDungAPI.pass)
                {
                    return nguoiDungAPI;
                }
                return null;
            }
            catch
            {
                throw;
            }
        }
    }
}