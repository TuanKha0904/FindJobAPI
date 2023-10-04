using System.ComponentModel.DataAnnotations;
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
        [CheckAdmin("admin","True")]
        public async Task<IActionResult> GetAllAccounts(int pageNumber, 
            [Range(1, 20, ErrorMessage = "Số trang phải từ 1 đến 20")]
            int pageSize = 20 , bool sortDateCreate = false)
        {
            try
            {
                var allAccount = await _accountRepository.GetAll(sortDateCreate, pageNumber, pageSize);
                return Ok(allAccount);

            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post()
        {
            try
            {
                var accessToken = User.FindFirst("Id")?.Value;
                if (accessToken == null)
                {
                    return BadRequest("Không tìm thấy người dùng");
                }
                var account = await _accountRepository.Post(accessToken);
                return Ok(account);
            }
            catch { return BadRequest(); }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login (string email,  string password)
        {
            try
            {
                var login = await _accountRepository.Login(email, password);
                return Ok(login);
            }
            catch { return BadRequest(); }
        }

        [HttpPatch("Infor")]
        [Authorize]
        public async Task<IActionResult> UpdateAccount(Infor info)
        {
            try
            {
                var userId = User.FindFirst("Id")?.Value;
                var inforUpdate = await _accountRepository.Info(userId!, info);
                if (inforUpdate == null) return BadRequest("Không tìm thấy người dùng");
                return Ok("Cập nhật thành công");
            }
            catch { return BadRequest("Cập nhật thất bại"); }
        }

        [HttpPatch("Photo")]
        [Authorize]
        public async Task<IActionResult> Photo(Photo photo)
        {
            try
            {
                var userId = User.FindFirst("Id")?.Value;
                var inforUpdate = await _accountRepository.Photo(userId!, photo);
                if (inforUpdate == null) return BadRequest("Không tìm thấy người dùng");
                return Ok("Cập nhật thành công");
            }
            catch { return BadRequest("Cập nhật thất bại!. Không tìm thấy đường dẫn hình ảnh"); }
        }

        [HttpPatch("Password")]
        [Authorize]
        public async Task<IActionResult> Password (Password password)
        {
            try
            {
                var userId = User.FindFirst("Id")?.Value;
                var updatePassword = await _accountRepository.Password(userId!, password);
                if (updatePassword == null) return BadRequest("Không tìm thấy người dùng");
                return Ok("Cập nhật thành công");
            }
            catch { return BadRequest("Cập nhật thất bại"); }
        }


        [HttpDelete("Delete")]
        [Authorize]
        [CheckAdmin("admin", "True")]
        public async Task<IActionResult> DeleteAccount(string userId)
        {
            try
            {
                var DeleteAccount = await _accountRepository.DeleteAccount(userId);
                if (DeleteAccount == null)
                    return BadRequest("Không tìm thấy người dùng");
                return Ok("Xóa thành công");
            }
            catch { return BadRequest("Xóa thất bại"); }
        }

    }
}
