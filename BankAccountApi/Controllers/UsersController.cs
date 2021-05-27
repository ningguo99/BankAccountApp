using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BankAccountApi.Data;
using BankAccountApi.DTOs;
using BankAccountApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BankAccountApi.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ILogger<UsersController> _logger;
        public UsersController(DataContext context, ILogger<UsersController> logger)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            _logger.LogInformation("Hello from the Get() method!");
            _logger.LogError("wcnm");
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(CreateUserDto createUserDto)
        {
            // check if the user is an admin
            Request.Headers.TryGetValue("Username", out var username);
            if (username != "Admin") return Unauthorized("Only admin can create a user");

            // construct appUser from createUserDto
            var currentTime = DateTime.Now;
            var appUser = new AppUser{
                Username = createUserDto.Username,
                FirstName = createUserDto.FirstName,
                LastName = createUserDto.LastName,
                CreatedDate = currentTime,
                ModifiedDate = currentTime
            };

            await _context.Users.AddAsync(appUser);
            var success = await _context.SaveChangesAsync() > 0;
            if (success)
            {
                return Ok(appUser);
            }
            return BadRequest("Failed to create user");
        }
    }
}