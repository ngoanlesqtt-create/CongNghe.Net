using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Server.DO;
using Server.IRepository;
using Server.Models;
using Server.Services;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private IConfiguration _config;
        private IRefreshToken _refreshTokenService;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration config,
            IRefreshToken refreshTokenService
            )
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._config = config;
            this._refreshTokenService = refreshTokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(userDTO.email);

                if (user == null) return NotFound("Ban nhap sai email");

                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, userDTO.password, false, false);

                if (result.Succeeded)
                {
                    var authClaims = new List<Claim> {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.PrimarySid, user.Id.ToString())
                    };

                    var newToken = CreateToken(authClaims);
                    var rawToken = new JwtSecurityTokenHandler().WriteToken(newToken);

                    //tao refreshToken
                    int expiredDays = Convert.ToInt32(_config["JWT:RefreshTokenValidityInDays"]);
                    var refreshTokenExpire = DateTime.Now.AddDays(expiredDays);

                    RefreshToken refresh = new RefreshToken();
                    refresh.DateTime = refreshTokenExpire;
                    refresh.UserID = user.Id;
                    refresh.Token = rawToken;

                    await _refreshTokenService.Insert(refresh);

                    string refreshToken = GenerateRefreshToken();

                    ReturnedLoginData data = new ReturnedLoginData();
                    data.Token = rawToken;
                    data.Name = user.UserName;
                    data.RefreshToken = refreshToken;

                    return Ok(data);
                }

                return BadRequest("Ban da nhap sai password");
            }

            return BadRequest("Ban chua nhap thong tin dang nhap");
        }


        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            await _refreshTokenService.Delete(token);

            await _signInManager.SignOutAsync();
            return Ok("Bạn đã logout thành công");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));
            _ = int.TryParse(_config["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: _config["JWT:ValidIssuser"],
                audience: _config["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        private static string GenerateRefreshToken()
        {
            var randomNumer = new byte[64];

            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumer);

            return Convert.ToBase64String(randomNumer);
        }
    }
}
