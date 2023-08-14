using FindJobAPI.Data;
using FindJobAPI.Model.Admins;
using FindJobAPI.Model.Roles;
using FindJobAPI.Repository.Roles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FindJobAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        private readonly IRole_Repository role_Repository;

        public RoleController(AppDbContext appDbContext, IRole_Repository role_Repository)
        {
            this.appDbContext = appDbContext;
            this.role_Repository = role_Repository;
        }

        [HttpGet("Get-all-role")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var allRole = await role_Repository.GetAll();
                return Ok(allRole);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("Create-Role")]
        public async Task<IActionResult> CreateRole(RoleNoId roleNoId)
        {
            try
            {
                var AddRole = await role_Repository.CreateRole(roleNoId);
                if (AddRole == null)
                {
                    return BadRequest($"role name đã tồn tại");
                }
                return Ok(AddRole);
            }
            catch
            {
                return BadRequest("Thêm role không thành công");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRole([Required] int id, RoleNoId roleNoId)
        {
            try
            {
                var RoleUpdate = await role_Repository.UpdateRole(id, roleNoId);
                if (RoleUpdate != null)
                    return Ok(RoleUpdate);
                return BadRequest($"Không tìm thấy role có id: {id}");
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRole([Required] int id)
        {
            try
            {
                var deleteRole = await role_Repository.DeleteRole(id);
                if (deleteRole != null)
                    return Ok(deleteRole);
                return BadRequest($"Không tìm thấy role có id: {id}");
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
