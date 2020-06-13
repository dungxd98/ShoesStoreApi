using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoesStoreApi.Models;

namespace ShoesStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Route("ListUsersAdmin")]
        //GET : /api/User/ListUsersAdmin
        public IActionResult ListUsersAdmin()
        {
            var users = _userManager.GetUsersInRoleAsync("Admin").Result;
            return Ok(users);
        }
        [HttpGet]
        [Route("ListUsersCustomer")]
        //GET : /api/User/ListUsersCustomer
        public IActionResult ListUsersCustomer()
        {
            var users = _userManager.GetUsersInRoleAsync("Customer").Result;
            return Ok(users);
        }
        [HttpGet]
        [Route("ListUsersSale")]
        //GET : /api/User/ListUsersSale
        public IActionResult ListUsersSale()
        {
            var users = _userManager.GetUsersInRoleAsync("Sale").Result;
            return Ok(users);
        }
        [HttpGet]
        [Route("UserDetai")]
        //GET : /api/User/UserDetai
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            // GetRolesAsync returns the list of user Roles
            var userRoles = await _userManager.GetRolesAsync(user);

            var model = new ApplicationUserModel
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                UserName = user.UserName,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                /*Role = userRoles*/
            };

            return Ok(model);
        }
        [HttpPost]
        [Route("Register")]
        //POST : api/User/Register
        public async Task<Object> Register(ApplicationUserModel model)
        {

            model.Role = "Admin";
            var applicationUser = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                Status = "Active"
            };
            try
            {
                var result = await _userManager.CreateAsync(applicationUser, model.Password);
                await _userManager.AddToRoleAsync(applicationUser, model.Role);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        [Route("RegisterSale")]
        //POST : api/User/RegisterSale
        public async Task<Object> RegisterSale(ApplicationUserModel model)
        {

            model.Role = "Sale";
            var applicationUser = new ApplicationUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                Status = "Active"
            };
            try
            {
                var result = await _userManager.CreateAsync(applicationUser, model.Password);
                await _userManager.AddToRoleAsync(applicationUser, model.Role);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        [Route("UpdateUser")]
        //POST : /api/User/UpdateUser
        public async Task<IActionResult> UpdateUser(ApplicationUserModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            user.FullName = model.FullName;
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.Address = model.Address;
                user.PhoneNumber = model.PhoneNumber;

            await _userManager.UpdateAsync(user);

            return Ok(model);
        }
        [HttpPost]
        [Route("UpdateStatusDeActive")]
        //POST : /api/User/UpdateStatusDeActive
        public async Task<IActionResult> UpdateStatusDeActive(string UserName)
        {
            var user = await _userManager.FindByNameAsync(UserName);
            if(user.Status == "Active")
            {
                user.Status = "DeActive";
            }    
            else
            {
                return BadRequest(new { message = "Username đã được DeActive." });
            }    
            await _userManager.UpdateAsync(user);

            return NoContent();
        }
    }
}