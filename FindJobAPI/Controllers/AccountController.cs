using FindJobAPI.Data;
using FindJobAPI.Model.DTO;
using FindJobAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FindJobAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IAccount_Repository _accountRepository;

        public AccountController(AppDbContext appDbContext, IAccount_Repository accountRepository)
        {
            _appDbContext = appDbContext;
            _accountRepository = accountRepository;
        }

        [HttpGet ("Get-all-account")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var AllAccount = await _accountRepository.GetAll();
                return Ok(AllAccount);
            }
            catch { return BadRequest(); }
        }

        [HttpPost ("Create-account")]
        public async Task<IActionResult> CreateAccount (CreateAccount createAccount)
        {
            try
            {
                var AddAccount = await _accountRepository.CreateAccount(createAccount);
                if (AddAccount == null)
                    return BadRequest("Email đã tồn tại");
                return Ok(AddAccount);
            }
            catch { return BadRequest(); }
        }

        [HttpPut("Update-account")]
        public async Task<IActionResult> UpdateAccount ([Required] int id, UpdateAccount updateAccount)
        {
            try
            {
                var UpdateAccount = await _accountRepository.UpdateAccount(id, updateAccount);
                if (UpdateAccount == null)
                    return BadRequest($"Không tìm thấy account có id: {id}");
                return Ok(UpdateAccount);
            }
            catch { return BadRequest(); }
        }

        [HttpDelete("Delete-account")]
        public async Task<IActionResult> DeleteAccount ([Required] int id)
        {
            try
            {
                var DeleteAccount = await _accountRepository.DeleteAccount(id);
                if (DeleteAccount == null)
                    return BadRequest($"Không tìm thấy account có id: {id}");
                return Ok(DeleteAccount);
            }
            catch { return BadRequest(); }
        }
    }
}
