using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BankAccountApi.DTOs;
using BankAccountApi.Entities;
using BankAccountApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountApi.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        // GET: apis/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReturnedUserDto>>> GetUsers()
        {
            var appUsers = await _userRepository.GetUsersAsync();
            var usersToReturn = _mapper.Map<IEnumerable<ReturnedUserDto>>(appUsers);
            return Ok(usersToReturn);
        }

        // POST: apis/users
        [HttpPost]
        public async Task<ActionResult<ReturnedUserDto>> CreateUser(CreateUserDto createUserDto)
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

            var appUser = _mapper.Map<AppUser>(createUserDto);

            var success = await _userRepository.CreateAsync(appUser);
            if (success)
            {
                var returnedUser = _mapper.Map<ReturnedUserDto>(appUser);
                return Ok(returnedUser);
            }
            return BadRequest("Failed to create user");
        }

    }
}