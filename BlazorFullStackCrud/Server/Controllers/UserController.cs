using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using BlazorFullStackCrud.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace BlazorFullStackCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext _context;

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserContext> logger, UserContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        private async Task<List<User>> GetDbUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // Authentication Methods ---------------------------------

        [HttpPost("loginuser")]
        public async Task<ActionResult<User>> LoginUser(User user)
        {
            User loggedInUser = await _context.Users.Where(u => u.Email == user.Email && u.Password == user.Password).FirstOrDefaultAsync();

            if (loggedInUser != null)
            {
                // create claim
                var claim = new Claim(ClaimTypes.Name, loggedInUser.Email);

                // create claimsIdentity
                var claimsIdentity = new ClaimsIdentity(new[] { claim }, "serverAuth");

                // create claimsPrincipal
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                // Sing in User
                await HttpContext.SignInAsync(claimsPrincipal);
            }

            return await Task.FromResult(loggedInUser);
        }

        [HttpGet("getcurrentuser")]
        public async Task<ActionResult<User>> GetCurrentUser()
        {
            User currentUser = new User();

            if (User.Identity.IsAuthenticated)
            {
                var emailAddress = User.FindFirstValue(ClaimTypes.Name);
                currentUser = await _context.Users.Where(u => u.Email == emailAddress).FirstOrDefaultAsync();
            }

            return await Task.FromResult(currentUser);
        }

        [HttpGet("logoutuser")]
        public async Task<ActionResult<String>> LogOutUser()
        {
            await HttpContext.SignOutAsync();
            return "Success";
        }

        [HttpPut("updateprofile/{Id}")]
        public async Task<User> UpdateProfile(int Id, [FromBody] User user)
        {
            User userToUpdate = await _context.Users.Where(u => u.Id == Id).FirstOrDefaultAsync();

            userToUpdate.UserName = user.UserName;
            userToUpdate.Email = user.Email;

            await _context.SaveChangesAsync();

            return await Task.FromResult(user);
        }

        [HttpGet("getprofile/{Id}")]
        public async Task<User> GetProfile(int Id)
        {
            return await _context.Users.Where(u => u.Id == Id).FirstOrDefaultAsync();
        }

    }
}
