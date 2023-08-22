﻿using FindJobAPI.Data;
using FindJobAPI.Model.DTO;
using FindJobAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FindJobAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobDetailController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IJob_Detail_Repository _repository;
        public JobDetailController(AppDbContext context, IJob_Detail_Repository repository)
        {
            _context = context;
            _repository = repository;
        }

        [HttpGet("Get-jobdetail-by-id")]
        public async Task<IActionResult> GetJobDetailById(int id)
        {
            try
            {
                var Job_Detail = await _repository.GetJobById(id);
                if (Job_Detail == null) return BadRequest($"Không tìm thấy job có id: {id}");
                return Ok(Job_Detail);
            }
            catch { return BadRequest(); }
        }

        [HttpPut("Update-job-detail")]
        public async Task<IActionResult> UpdateJobDetail (int id, Update_JobDetail update_JobDetail)
        {
            try
            {
                var Job_Detail = await _repository.UpdateJobDetail(id, update_JobDetail);
                if (Job_Detail == null) { return BadRequest($"Không tìm thấy job có id: {id}"); }
                return Ok(Job_Detail);
            }
            catch { return BadRequest(); }
        }

        [HttpPut("Update-status-job")]
        public async Task<IActionResult> UpdateStatus (int id, Update_Status_Job update_Status_Job)
        {
            try
            {
                var Job_Detail = await _repository.UpdateStatusJob(id, update_Status_Job);
                if (Job_Detail == null) { return BadRequest($"Không tìm thấy job có id: {id}"); }
                return Ok(Job_Detail);
            }
            catch { return BadRequest() ; }
        }
    }
}