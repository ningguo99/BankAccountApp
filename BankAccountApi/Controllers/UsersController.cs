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
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository, IAccountRepository accountRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _accountRepository = accountRepository;
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

        [HttpPost("add-bank-account")]
        public async Task<ActionResult<AddBankAccountDto>> AddBankAccount(AddBankAccountDto addBankAccountDto)
        {
            // assume the user's Id will be included in the http header "UserId"
            Request.Headers.TryGetValue("UserId", out var id);

            var bankAccount = _mapper.Map<BankAccount>(addBankAccountDto);

            bankAccount.AccountNumber = await _accountRepository.GenerateAccountNumber();
            bankAccount.AppUserId = Int32.Parse(id);

            var success = await _accountRepository.Create(bankAccount);
            if (success) {
                var ReturnedBankAccountDto = _mapper.Map<ReturnedBankAccountDto>(bankAccount);
                return Ok(ReturnedBankAccountDto);
            }
            return BadRequest("Failed to create account");
        }



    }
}