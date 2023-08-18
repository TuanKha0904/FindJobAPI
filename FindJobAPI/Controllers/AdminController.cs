using FindJobAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using FindJobAPI.Model.DTO;
using FindJobAPI.Repository.Interfaces;

namespace FindJobAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IAdmin_repository _adminRepository;

        public AdminController(AppDbContext appDbContext, IAdmin_repository adminRepository)
        {
            _appDbContext = appDbContext;
            _adminRepository = adminRepository;
        }

        [HttpGet("Get-all-admin")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var allAdmin = await _adminRepository.GetAll();
                return Ok(allAdmin);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("Create-Admin")]
        public async Task<IActionResult> CreateAdmin(AdminNoId adminNoId)
        {
            try
            {
                var AddAdmin = await _adminRepository.CreateAdmin(adminNoId); 
                if (AddAdmin == null)
                {
                    return BadRequest($"Username đã tồn tại");
                }
                return Ok(AddAdmin);
            }
            catch
            {
                return BadRequest("Thêm admin không thành công");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAdmin([Required]string username, UpdateAdmin updateAdmin)
        {
            try
            {
                var AdminUpdate = await _adminRepository.UpdateAdmin(username, updateAdmin);
                if (AdminUpdate != null)
                    return Ok(AdminUpdate);
                return BadRequest($"Không tìm thấy username: {username}");
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAdmin([Required] int id)
        {
            try
            {
                var deleteAdmin = await _adminRepository.DeleteAdmin(id);
                if (deleteAdmin != null)
                    return Ok(deleteAdmin);
                return BadRequest($"Không tìm thấy admin có id: {id}");
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
