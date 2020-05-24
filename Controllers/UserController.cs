﻿using System;
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
        [Route("ListUsers")]
        //GET : /api/User/ListUsers
        public IActionResult ListUsers()
        {
            var users = _userManager.Users;
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
                Email = user.Email,
                UserName = user.UserName,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                /*Role = userRoles*/
            };

            return Ok(model);
        }

        [HttpGet]
        [Route("ListUsersOfRole")]
        //GET : /api/User/ListUsersOfRole
        public IActionResult ListUsersOfRole()
        {
            var users = _userManager.GetUsersInRoleAsync("Customer").Result;
            return Ok(users);
        }
    }
}