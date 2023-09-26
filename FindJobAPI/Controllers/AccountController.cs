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
    [Authorize]
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

        [HttpPatch("Infor")]
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

        [HttpPatch("Photo")]
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

        [HttpPatch("Password")]
        public async Task<IActionResult> Password (Password password)
        {
            try
            {
                var userId = User.FindFirst("Id")?.Value;
                var updatePassword = await _accountRepository.Password(userId!, password);
                if (updatePassword == null) return BadRequest("Không tìm thấy account");
                return Ok("Cập nhật thành công");
            }
            catch { return BadRequest("Cập nhật thất bại"); }
        }


        [HttpDelete("Delete")]
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
