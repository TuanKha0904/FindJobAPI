using FindJobAPI.Data;
using FindJobAPI.Model.DTO;
using FindJobAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet("All")]
        [Authorize]
        public async Task<IActionResult> GetAllAccounts()
        {
            try
            {
                var allAccount = await _accountRepository.GetAll();
                return Ok(allAccount);

            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("Login")]
        [Authorize]
        public async Task<IActionResult> Login()
        {
            try
            {
                var accessToken = User.FindFirst("Id")?.Value;
                if (accessToken == null)
                {
                    return BadRequest("Không tìm thấy account");
                }
                var account = await _accountRepository.Login(accessToken);
                return Ok(account);
            }
            catch { return BadRequest(); }
        }

/*        [HttpPost("Create")]
        [Authorize]
        public async Task<IActionResult> CreateAccount()
        {
            try
            {
                var userId = User.FindFirst("Id")?.Value;
                var accountDomain = await _appDbContext.Account.FirstOrDefaultAsync(a => a.UID == userId);
                if (accountDomain == null)
                {
                    var addAccount = await _accountRepository.CreateAccount(userId!);
                    return Ok(addAccount);
                }
                return BadRequest("Account already exists");
            }
            catch (Exception ex) 
            {
                return BadRequest(ex);
            }
        }
*/
        [HttpPut("Infor")]
        [Authorize]
        public async Task<IActionResult> UpdateAccount(Infor infor)
        {
            try
            {
                var userId = User.FindFirst("Id")?.Value;
                var inforUpdate = await _accountRepository.Infor(userId!, infor);
                if (inforUpdate == null) return BadRequest("Không tìm thấy account");
                return Ok("Cập nhật thành công");
            }
            catch { return BadRequest("Cập nhật thất bại"); }
        }

        [HttpPut("Photo")]
        [Authorize]
        public async Task<IActionResult> Photo(Photo photo)
        {
            try
            {
                var userId = User.FindFirst("Id")?.Value;
                var inforUpdate = await _accountRepository.Photo(userId!, photo);
                if (inforUpdate == null) return BadRequest("Không tìm thấy account");
                return Ok("Cập nhật thành công");
            }
            catch { return BadRequest("Cập nhật thất bại"); }
        }


        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> DeleteAccount(string userId)
        {
            try
            {
                var DeleteAccount = await _accountRepository.DeleteAccount(userId);
                if (DeleteAccount == null)
                    return BadRequest("Không tìm thấy account");
                return Ok("Xóa thành công");
            }
            catch { return BadRequest("Xóa thất bại"); }
        }

    }
}
