using FindJobAPI.Data;
using FindJobAPI.Model.DTO;
using FindJobAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet ("Get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var AllAccount = await _accountRepository.GetAll();
                return Ok(AllAccount);
            }
            catch { return BadRequest(); }
        }

        [HttpGet("Get-one")]
        public async Task<IActionResult> GetOne( [Required] string email, [Required] string password)
        {
            try
            {
                var checkemail = await _appDbContext.Account.FirstOrDefaultAsync(c => c.email == email);
                if (checkemail == null) return BadRequest($"{email} chưa được đăng kí");
                var Account = await _accountRepository.GetOne(email, password);
                if (Account != null)
                    return Ok(Account);
                else
                    return BadRequest("Sai email hoặc mật khẩu");
            }
            catch { return BadRequest(); }
        }

        [HttpPost ("Create")]
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

        [HttpPut("Update")]
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

        [HttpDelete("Delete")]
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
