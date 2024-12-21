using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Server.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<ApplicationRole> _roleManager;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost("create-user")]
        [AllowAnonymous]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appUser = new ApplicationUser
                {
                    UserName = user.Name,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                };

                IdentityResult result = await _userManager.CreateAsync(appUser, user.Password);

                if (result.Succeeded)
                {
                    Console.WriteLine("test dong 36");
                    return Ok("Da tao user thanh cong");
                }


                foreach (IdentityError error in result.Errors)
                {
                    Console.WriteLine("test dong 41");
                    return BadRequest(error);

                }
            }
            return Ok();
        }

        [HttpPost("create-role")]
        public async Task<IActionResult> CreateRole(Role role)
        {
            if (ModelState.IsValid)
            {
                ApplicationRole appRole = new ApplicationRole
                {
                    Name = role.Name,
                };

                IdentityResult result = await _roleManager.CreateAsync(appRole);

                if (result.Succeeded)
                    return Ok("Da tao role thanh cong");

                foreach (IdentityError error in result.Errors)
                    return BadRequest(error);
            }

            return Ok("Da tao role thanh cong");
        }
    }
}
