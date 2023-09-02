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
    public class JobController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IJob_Repository _jobRepository;

        public JobController (AppDbContext appDbContext, IJob_Repository jobRepository)
        {
            _appDbContext = appDbContext;
            _jobRepository = jobRepository;
        }

        [HttpGet("Get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var Job = await _jobRepository.GetAll();
                return Ok(Job);
            }
            catch { return BadRequest(); }
        }

        [HttpGet ("Get-one")]        
        public async Task<IActionResult> GetJobById ([Required] int id)
        {
            try
            {
                var Job = await _jobRepository.GetById(id);
                if (Job == null)
                    return BadRequest($"Không tìm thấy job có id: {id}");
                return Ok(Job);
            }
            catch { return BadRequest(); }
        }

        [HttpPost ("Create")]
        public async Task<IActionResult> CreateJob (CreateJob createJob)
        {
            try
            {
                var ExistingEmployer = await _appDbContext.Employer.FirstOrDefaultAsync(e => e.account_id == createJob.account_id);
                var ExistingType = await _appDbContext.Type.FirstOrDefaultAsync(t => t.type_id == createJob.type_id);
                var ExistingIndustry = await _appDbContext.Industry.FirstOrDefaultAsync(i => i.industry_id == createJob.industry_id);
                if (ExistingEmployer == null && ExistingType == null && ExistingIndustry == null)
                    return BadRequest("Employer, Type and Industry not found");
                else if (ExistingEmployer == null)
                    return BadRequest("Employer not found");
                else if (ExistingType == null)
                    return BadRequest("Type not found");
                else if (ExistingIndustry == null)
                    return BadRequest("Industry not found");
                if (createJob.deadline < DateTime.Now)
                    return BadRequest("Deadline must more than today");
                var Job = await _jobRepository.CreateJob(createJob);
                return Ok(Job);
            }
            catch { return BadRequest(); }
        }

        [HttpPut ("Update")]
        public async Task<IActionResult> UpdateJob ([Required] int id, UpdateJob updateJob)
        {
            try
            {
                var Job = await _jobRepository.UpdateJob(id, updateJob);
                if (Job == null)
                    return BadRequest($"Không tìm thấy job có id: {id}");
                return Ok(Job);
            }
            catch { return BadRequest(); }
        }

        [HttpDelete ("Delete")]
        public async Task<IActionResult> DeleteJob ([Required] int id)
        {
            try
            {
                var job = await _jobRepository.DeleteJob(id);
                if (job == null)
                    return BadRequest($"Không tìm thấy job có id: {id}");
                return Ok(job);
            }
            catch { return BadRequest(); }
        }
    }
}
