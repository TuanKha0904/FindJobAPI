using FindJobAPI.Data;
using FindJobAPI.Model.DTO;
using FindJobAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FindJobAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JobController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IJob_Repository _jobRepository;

        public JobController(AppDbContext appDbContext, IJob_Repository jobRepository)
        {
            _appDbContext = appDbContext;
            _jobRepository = jobRepository;
        }

        [HttpGet("AllJobPost")]
        public async Task<IActionResult> AllJobPost()
        {
            try
            {
                var listJob = await _jobRepository.AllJobPost();
                return Ok(listJob);
            }
            catch { return BadRequest(); }
        }

        [HttpGet("AllJobWait")]
        public async Task<IActionResult> AllJobWait()
        {
            try
            {
                var listJob = await _jobRepository.AllJobWait();
                return Ok(listJob);
            }
            catch { return BadRequest(); }
        }

        [HttpGet("AllJobTimeout")]
        public async Task<IActionResult> AllJobTimeOut()
        {
            try
            {
                var listJob = await _jobRepository.AllJobTimeOut();
                return Ok(listJob);
            }
            catch { return BadRequest(); }
        }

        [HttpGet("JobDetail")]
        public async Task<IActionResult> JobDetail(int jobId)
        {
            try
            {
                var jobDetail = await _jobRepository.JobDetail(jobId);
                if (jobDetail == null) return BadRequest("Không tìm thấy công việc");
                return Ok(jobDetail);
            }
            catch { return BadRequest(); }
        }

        [HttpPatch("Status")]
        public async Task<IActionResult> Status (int jobId)
        {
            try
            {
                var updateStatus = await _jobRepository.Status(jobId);
                if (updateStatus == null) return BadRequest("Không tìm thấy công việc");
                return Ok("Cập nhật thành công");
            }
            catch { return BadRequest("Cập nhật thất bại"); }
        }

        [HttpGet ("JobPostList")]
        public async Task<IActionResult> JobPostList()
        {
            try
            {
                var userId = User.FindFirst("Id")?.Value;
                var jobPostList = await _jobRepository.JobPostList(userId!);
                return Ok(jobPostList);
            }
            catch { return BadRequest(); }

        }

        [HttpGet("JobWaitList")]
        public async Task<IActionResult> JobWaitList()
        {
            try
            {
                var userId = User.FindFirst("Id")?.Value;
                var jobWaitList = await _jobRepository.JobWaitList(userId!);
                return Ok(jobWaitList);
            }
            catch { return BadRequest(); }

        }

        [HttpGet("JobTimeoutList")]
        public async Task<IActionResult> JobTimeoutList()
        {
            try
            {
                var userId = User.FindFirst("Id")?.Value;
                var jobTimeoutList = await _jobRepository.JobTimeoutList(userId!);
                return Ok(jobTimeoutList);
            }
            catch { return BadRequest(); }

        }


        [HttpGet("ApplyList")]
        public async Task<IActionResult> ApplyList(int job_id)
        {
            try
            {
                var ApplyList = await _jobRepository.ApplyList(job_id);
                return Ok(ApplyList);
            }
            catch { return BadRequest() ;}
        }

        [HttpGet("Receive")]
        public async Task<IActionResult> Receive(int job_id)
        {
            try
            {
                var listReceive = await _jobRepository.Receive(job_id);
                return Ok(listReceive);
            }
            catch { return BadRequest(); }
        }


        [HttpPost("Post")]
        public async Task<IActionResult> Post (CreateJob createJob)
        {
            try
            {
                var location = await _appDbContext.Location.FindAsync(createJob.Location_id);
                if (location == null) return BadRequest("Không tìm thấy vị trí này");
                var industry = await _appDbContext.Industry.FindAsync(createJob.Industry_id);
                if (industry == null) return BadRequest("Không tìm thấy lĩnh vực này");
                var type = await _appDbContext.Type.FindAsync(createJob.Type_id);
                if (type == null) return BadRequest("Không tìm thấy loại công việc này");

                var userId = User.FindFirst("Id")?.Value;
                var create = await _jobRepository.CreateJob(userId!, createJob);
                if (create == null) { return BadRequest("Không tìm thấy tài khoản nhà tuyển dụng"); }
                return Ok(create);
            }
            catch { return BadRequest(); }
        }

        [HttpPost("Search")]
        public async Task<IActionResult> Search(int industry_id, int type_id, int location_id)
        {
            try
            {
                var searchJob = await _jobRepository.Search(industry_id, type_id, location_id);
                return Ok(searchJob);
            }
            catch { return BadRequest() ; }
        }

        [HttpPut ("Update")]
        public async Task<IActionResult> Update(int job_id, UpdateJob updateJob)
        {
            try
            {
                if(updateJob.Location_id == 0 || updateJob.Type_id == 0 || updateJob.Industry_id == 0) 
                {
                    var jobUpdate = await _jobRepository.Update(job_id, updateJob);
                    if (jobUpdate == null) { return BadRequest("Không tìm thấy công việc"); }
                    return Ok("Cập nhật thành công");

                }
                else
                {
                    var location = await _appDbContext.Location.FindAsync(updateJob.Location_id);
                    if (location == null) { return BadRequest("Không tìm thấy vị trí"); }
                    var industry = await _appDbContext.Industry.FindAsync(updateJob.Industry_id);
                    if (industry == null) { return BadRequest("Không tìm thấy ngành công việc"); }
                    var type = await _appDbContext.Type.FindAsync(updateJob.Type_id);
                    if (type == null) { return BadRequest("Không tìm thấy loại công việc"); }
                    var jobUpdate = await _jobRepository.Update(job_id, updateJob);
                    if (jobUpdate == null) { return BadRequest("Không tìm thấy công việc"); }
                    return Ok("Cập nhật thành công");
                }
            }
            catch { return BadRequest(); }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int jobId)
        {
            try
            {
                var deleteJob = await _jobRepository.Delete(jobId);
                if (deleteJob == null) { return BadRequest("Không tìm thấy công việc cần xóa"); }
                return Ok("Xóa thành công");
            }
            catch { return BadRequest("Xóa thất bại"); }
        }

        
    }
}
