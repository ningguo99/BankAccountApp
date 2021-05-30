using System;
using System.Threading.Tasks;
using AutoMapper;
using BankAccountApi.DTOs;
using BankAccountApi.Entities;
using BankAccountApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountApi.Controllers
{
    public class BankAccountsController : BaseApiController
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public BankAccountsController(IAccountRepository accountRepository, IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _accountRepository = accountRepository;
            _userRepository = userRepository;
        }

        // POST: api/BankAccounts
        [HttpPost]
        public async Task<ActionResult<ReturnedBankAccountDto>> CreateBankAccount(AddBankAccountDto addBankAccountDto)
        {
            // since authentication is out of scope, 
            // assume the user's Id will be included in the http header "UserId"
            Request.Headers.TryGetValue("UserId", out var userId);

            var bankAccount = _mapper.Map<BankAccount>(addBankAccountDto);
            bankAccount.AccountNumber = await _accountRepository.GenerateAccountNumber();
            bankAccount.AppUserId = Int32.Parse(userId);

            var success = await _accountRepository.CreateAsync(bankAccount);
            if (success)
            {
                var returnedBankAccountDto = _mapper.Map<ReturnedBankAccountDto>(bankAccount);
                return Ok(returnedBankAccountDto);
            }
            return BadRequest("Failed to create account");
        }

        // PUT: api/BankAccounts/{accountId}/deposit
        [HttpPut("{accountId}/deposit")]
        public async Task<ActionResult> Deposit([FromQuery] double amount, int accountId)
        {
            Request.Headers.TryGetValue("UserId", out var userId);
            if (!(await _userRepository.AccountBelongsToUser(Int32.Parse(userId), accountId)))
                return Unauthorized("You are not authorized to deposit to this account");
            if (amount < 0)
                return BadRequest("Deposit amount cannot be negative");

            var bankAccount = await _accountRepository.GetBankAccountByIdAsync(accountId);
            bankAccount.Balance += amount;
            bankAccount.ModifiedDate = DateTime.Now;

            await _accountRepository.UpdateAsync(bankAccount);
            return Ok(_mapper.Map<ReturnedBankAccountDto>(bankAccount));
        }

        // PUT: api/BankAccounts/{accountId}/withdraw
        [HttpPut("{accountId}/withdraw")]
        public async Task<ActionResult> Withdraw([FromQuery] double amount, int accountId)
        {
            Request.Headers.TryGetValue("UserId", out var userId);
            if (!(await _userRepository.AccountBelongsToUser(Int32.Parse(userId), accountId)))
                return Unauthorized("You are not authorized to deposit to this account");
            if (amount < 0)
                return BadRequest("Withdraw amount cannot be negative");

            var bankAccount = await _accountRepository.GetBankAccountByIdAsync(accountId);
            bankAccount.Balance -= amount;
            if (bankAccount.Balance < 0)
                return BadRequest("Banlance must be greater than 0");
            bankAccount.ModifiedDate = DateTime.Now;

            await _accountRepository.UpdateAsync(bankAccount);
            return Ok(_mapper.Map<ReturnedBankAccountDto>(bankAccount));
        }

        // GET: api/BankAccounts/{accountId}/balance
        [HttpGet("{accountId}/balance")]
        public async Task<ActionResult<double>> GetBalance(int accountId) {
            Request.Headers.TryGetValue("UserId", out var userId);
            if (!(await _userRepository.AccountBelongsToUser(Int32.Parse(userId), accountId)))
                return Unauthorized("You are not authorized to deposit to this account");
            
            var bankAccount = await _accountRepository.GetBankAccountByIdAsync(accountId);

            return Ok(bankAccount.Balance);
        }
    }
}