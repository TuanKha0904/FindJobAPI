/*using FindJobAPI.Data;
using FindJobAPI.Model.DTO;
using FindJobAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace FindJobAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeekerController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly ISeeker_Repository seeker_Repository;
        public SeekerController (AppDbContext appDbContext, ISeeker_Repository seeker_repository)
        {
            _appDbContext = appDbContext;
            seeker_Repository = seeker_repository;
        }

        [HttpGet("Get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var ListSeeker = await seeker_Repository.GetAll();
                return Ok(ListSeeker);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("Get-one")]
        public async Task<IActionResult> GetSeekerById([Required] int id)
        {
            try
            {
                var SeekerDomain = await seeker_Repository.GetById(id);
                if (SeekerDomain == null)
                    return BadRequest($"Không tìm thấy seeker có id: {id}");
                return Ok(SeekerDomain);
            }
            catch { return BadRequest(); }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateSeeker([Required] int id, SeekerNoId seekerNoId)
        {
            try
            {
                var SeekerDomain = await seeker_Repository.UpdateSeeker(id, seekerNoId);
                if (SeekerDomain == null)
                    return BadRequest($"Không tìm thấy seeker có id: {id}");
                return Ok(SeekerDomain);
            }
            catch { return BadRequest(); }
        }
    }
}
*/