using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using System.ComponentModel.DataAnnotations;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
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
                    return Ok("Ban da dang nhap thanh cong");

                return BadRequest("Ban da nhap sai password");
            }

            return BadRequest("Ban chua nhap thong tin dang nhap");
        }

        [HttpGet]
        [Route("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("Ban da logout thanh cong");
        }
    }
}
