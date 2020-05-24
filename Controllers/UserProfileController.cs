using System;
using System.Linq;
using System.Threading.Tasks;
using ShoesStoreApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ShoesStoreApi.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase {
        private UserManager<ApplicationUser> _userManager;
        public UserProfileController (UserManager<ApplicationUser> userManager) {
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        //GET : /api/UserProfile
        public async Task<Object> GetUserProfile () {
            string userId = User.Claims.First (c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync (userId);
            return new {
                user.FullName,
                    user.Email,
                    user.UserName,
                    user.Address,
                    user.PhoneNumber
            };
        }

        [HttpPost]
        [Authorize]
        [Route("UpdateUserProfile")]
        //POST : api/UserProfile/UpdateUserProfile
        public async Task<IActionResult> UpdateUserProfile(ApplicationUserModel model)
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);

            user.UserName = model.UserName;
                user.Email = model.Email;
                user.FullName = model.FullName;
                user.PhoneNumber = model.PhoneNumber;
                user.Address = model.Address;

            await _userManager.UpdateAsync(user);
            return NoContent();


        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("ForAdmin")]
        public string GetForAdmin()
        {
            return "Web method for Admin";
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        [Route("ForCustomer")]
        public string GetForCustomer()
        {
            return "Web method for Customer";
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Customer")]
        [Route("ForAdminOrCustomer")]
        public string GetForAdminOrCustomer()
        {
            return "Web method for Admin or Customer";
        }
    }
}