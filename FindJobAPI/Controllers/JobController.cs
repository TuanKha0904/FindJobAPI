using FindJobAPI.Data;
using FindJobAPI.Model.DTO;
using FindJobAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using FirebaseAdmin.Messaging;

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

        [HttpGet("GetAll")]
        [CheckAdmin("admin", "True")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var listJob = await _jobRepository.GetAll();
                return Ok(listJob);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }


        [HttpGet("AllJobPost")]
        [CheckAdmin("admin", "True")]
        public async Task<IActionResult> AllJobPost(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var listJob = await _jobRepository.AllJobPost(pageNumber, pageSize);
                return Ok(listJob);
            }
            catch { return BadRequest(); }
        }

        [HttpGet("AllJobWait")]
        [CheckAdmin("admin", "True")]
        public async Task<IActionResult> AllJobWait(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var listJob = await _jobRepository.AllJobWait(pageNumber, pageSize);
                return Ok(listJob);
            }
            catch { return BadRequest(); }
        }

        [HttpGet("AllJobTimeout")]
        [CheckAdmin("admin", "True")]
        public async Task<IActionResult> AllJobTimeOut(int pageNumber = 1, int pageSize = 20)
        {
            try
            {
                var listJob = await _jobRepository.AllJobTimeOut(pageNumber, pageSize);
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

        [HttpPut("Status")]
        [CheckAdmin("admin", "True")]
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

        [HttpGet("AllJob")]
        public async Task<IActionResult> AllJob(int pageNumber = 1, int pageSize = 5)
        {
            try
            {
                var userId = User.FindFirst("Id")?.Value;
                var allJob = await _jobRepository.AllJob(userId!, pageNumber, pageSize);
                return Ok(allJob);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet ("JobPostList")]
        public async Task<IActionResult> JobPostList(int pageNumber = 1, int pageSize = 5)
        {
            try
            {
                var userId = User.FindFirst("Id")?.Value;
                var jobPostList = await _jobRepository.JobPostList(userId!, pageNumber, pageSize);
                return Ok(jobPostList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("JobWaitList")]
        public async Task<IActionResult> JobWaitList(int pageNumber = 1, int pageSize = 5)
        {
            try
            {
                var userId = User.FindFirst("Id")?.Value;
                var jobWaitList = await _jobRepository.JobWaitList(userId!, pageNumber, pageSize);
                return Ok(jobWaitList);
            }
            catch { return BadRequest(); }

        }

        [HttpGet("JobTimeoutList")]
        public async Task<IActionResult> JobTimeoutList(int pageNumber = 1, int pageSize = 5)
        {
            try
            {
                var userId = User.FindFirst("Id")?.Value;
                var jobTimeoutList = await _jobRepository.JobTimeoutList(userId!, pageNumber, pageSize);
                return Ok(jobTimeoutList);
            }
            catch { return BadRequest(); }

        }


        [HttpGet("ApplyList")]
        public async Task<IActionResult> ApplyList(int job_id, int pageNumber = 1, int pageSize = 5)
        {
            try
            {
                var ApplyList = await _jobRepository.ApplyList(job_id, pageNumber, pageSize);
                return Ok(ApplyList);
            }
            catch { return BadRequest() ;}
        }

        [HttpGet("Receive")]
        public async Task<IActionResult> Receive(int job_id, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var listReceive = await _jobRepository.Receive(job_id, pageNumber, pageSize);
                return Ok(listReceive);
            }
            catch { return BadRequest(); }
        }


        [HttpPost("Post")]
        public async Task<IActionResult> Post (CreateJob createJob)
        {
            try
            {
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
        public async Task<IActionResult> Search(int industry_id, int type_id, string location, int pageNumber = 1, int pageSize = 5)
        {
            try
            {
                var searchJob = await _jobRepository.Search(industry_id, type_id, location, pageNumber, pageSize);
                return Ok(searchJob);
            }
            catch { return BadRequest() ; }
        }

        [HttpPut ("Update")]
        public async Task<IActionResult> Update(int job_id, UpdateJob updateJob)
        {
            try
            {
                if(!string.IsNullOrEmpty(updateJob.Location) || updateJob.Type_id == 0 || updateJob.Industry_id == 0) 
                {
                    var jobUpdate = await _jobRepository.Update(job_id, updateJob);
                    if (jobUpdate == null) { return BadRequest("Không tìm thấy công việc"); }
                    return Ok("Cập nhật thành công");

                }
                else
                {
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

        [HttpGet("CountJob")]
        [CheckAdmin("admin", "True")]
        public async Task<IActionResult> CountJob()
        {
            try
            {
                var count = await _jobRepository.CountJob();
                return Ok(count);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

    }
}
