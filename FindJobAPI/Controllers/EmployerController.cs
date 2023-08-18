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
    public class EmployerController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IEmployer_Repository _employer_Repository;
        public EmployerController(AppDbContext appDbContext, IEmployer_Repository  employer_Repository)
        {
            _appDbContext = appDbContext;
           _employer_Repository = employer_Repository;
        }

        [HttpGet("Get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var ListEmployer = await _employer_Repository.GetAll();
                return Ok(ListEmployer);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("Get-employer-by-id")]
        public async Task<IActionResult> GetEmployerById([Required] int id)
        {
            try
            {
                var EmployerDomain = await _employer_Repository.GetById(id);
                if (EmployerDomain == null)
                    return BadRequest($"Không tìm thấy employer có id: {id}");
                return Ok(EmployerDomain);
            }
            catch { return BadRequest(); }
        }

        [HttpPut("Update-employer")]
        public async Task<IActionResult> UpdateEmployer([Required] int id, EmployerNoId employerNoId)
        {
            try
            {
                var EmployerDomain = await _employer_Repository.UpdateEmployer(id, employerNoId);
                if (EmployerDomain == null)
                    return BadRequest($"Không tìm thấy employer có id: {id}");
                return Ok(EmployerDomain);
            }
            catch { return BadRequest(); }
        }
    }
}
