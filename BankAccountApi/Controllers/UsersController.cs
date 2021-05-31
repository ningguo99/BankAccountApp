using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BankAccountApi.DTOs;
using BankAccountApi.Entities;
using BankAccountApi.Helpers;
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

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReturnedUserDto>>> GetUsers()
        {
            // check if the user is an admin
            Request.Headers.TryGetValue("Username", out var username);
            if (username != "Admin")
                return Unauthorized("Only admin can view all users");
            var appUsers = await _userRepository.GetUsersAsync();
            var usersToReturn = _mapper.Map<IEnumerable<ReturnedUserDto>>(appUsers);
            return Ok(usersToReturn);
        }

        // POST: api/users
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

        [HttpGet("{id}/total-balance")]
        public async Task<ActionResult<double>> GetUserTotalBalance(int id)
        {
            Request.Headers.TryGetValue("UserId", out var userId);
            if (Int32.Parse(userId) != id)
                return Unauthorized("You are not authorized to view this user's total balance");

            var appUser = await _userRepository.GetUserByIdAsync(id);
            var sum = appUser.BankAccounts.Sum(x => x.Balance);
            return Ok(sum);
        }

        [HttpPut("{id}/update-address")]
        public async Task<ActionResult<ReturnedUserDto>> UpdateUserAddress(int id, [FromBody] UpdateAddressDto updateAddressDto)
        {
            Request.Headers.TryGetValue("UserId", out var userId);
            if (Int32.Parse(userId) != id)
                return Unauthorized("You are not authorized to view this user's total balance");

            var appUser = await _userRepository.GetUserByIdAsync(id);
            if (!AddressHelper.ValidStateWithPostCode(updateAddressDto.State, updateAddressDto.PostCode))
                return BadRequest("The state and postcode don't match");

            if (appUser.Address == null)
            {
                appUser.Address = _mapper.Map<Address>(updateAddressDto);
            }
            else
            {
                appUser.Address.State = updateAddressDto.State;
                appUser.Address.PostCode = updateAddressDto.PostCode;
            }
            appUser.Address.ModifiedDate = DateTime.Now;
            appUser.Address.AppUserId = id;
            var success = await _userRepository.UpdateAsync(appUser);

            if (success)
            {
                var returnedUser = _mapper.Map<ReturnedUserDto>(appUser);
                return Ok(returnedUser.Address);
            }
            return BadRequest("Failed to update user's address");

        }
    }
}