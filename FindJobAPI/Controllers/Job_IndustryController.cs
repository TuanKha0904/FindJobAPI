using FindJobAPI.Data;
using FindJobAPI.Model.DTO;
using FindJobAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FindJobAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Job_IndustryController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IJob_Industry_Repository _job_industry_repository;

        public Job_IndustryController (AppDbContext context, IJob_Industry_Repository job_industry_repository)
        {
            _context = context;
            _job_industry_repository = job_industry_repository;
        }

        [HttpGet("Get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var GetAll = await _job_industry_repository.GetAll();
                return Ok(GetAll);
            }
            catch { return BadRequest(); }
        }

        [HttpGet ("Get-by-id")]
        public async Task<IActionResult> GetById (int id)
        {
            try
            {
                var GetById = await _job_industry_repository.GetJobIndustryById(id);
                if(GetById == null) return BadRequest($"Không tìm thấy job industry có id : {id}");
                return Ok(GetById);
            }
            catch { return BadRequest(); }
        }

        [HttpPost ("Create-job-industry")]
        public async Task<IActionResult> CreateJobIndustry(Job_IndustryDTO job_IndustryDTO)
        {
            try
            {
                var Create = await _job_industry_repository.CreateJobIndustry (job_IndustryDTO);
                if(Create == null) return BadRequest($"Đã có industry với id : {job_IndustryDTO.id} và job : {job_IndustryDTO.job}");
                return Ok(Create);
            }
            catch { return BadRequest(); }
        }

        [HttpPut ("Update-job-industry")]
        public async Task<IActionResult> UpdateJobIndustry(int id, string job, Job_IndustryDTO job_IndustryDTO)
        {
            try
            {
                var Update = await _job_industry_repository.UpdateJobIndustry(id, job, job_IndustryDTO);
                if (Update == null) return BadRequest($"Không tìm thấy industry với id : {job_IndustryDTO.id} và job : {job_IndustryDTO.job}");
                return Ok(Update);
            }
            catch { return BadRequest(); }
        }

        [HttpDelete("Delete-job-industry")]
        public async Task<IActionResult> DeleteJobIndustry(int id, string job)
        {
            try
            {
                var Delete = await _job_industry_repository.DeleteJobIndustry(id, job);
                if (Delete == null) return BadRequest($"Không tìm thấy industry với id : {id} và job : {job}");
                return Ok(Delete);
            }
            catch { return BadRequest(); }
        }
    }
}
