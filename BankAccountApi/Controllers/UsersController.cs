using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BankAccountApi.DTOs;
using BankAccountApi.Entities;
using BankAccountApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountApi.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var appUsers = await _userRepository.GetUsersAsync();
            return Ok(appUsers);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(CreateUserDto createUserDto)
        {
            // check if the user is an admin
            Request.Headers.TryGetValue("Username", out var username);
            if (username != "Admin")
                return Unauthorized("Only admin can create a user");

            // check if the username already exists in the db
            if (await _userRepository.UsernameExists(createUserDto.Username))
                return BadRequest("Username is taken");

            // construct appUser from createUserDto
            var currentTime = DateTime.Now;
            var appUser = new AppUser
            {
                Username = createUserDto.Username,
                FirstName = createUserDto.FirstName,
                LastName = createUserDto.LastName,
                CreatedDate = currentTime,
                ModifiedDate = currentTime
            };

            var success = await _userRepository.CreateAsync(appUser);
            if (success)
            {
                return Ok(appUser);
            }
            return BadRequest("Failed to create user");
        }
    }
}