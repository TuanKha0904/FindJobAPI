﻿using FindJobAPI.Data;
using FindJobAPI.Model.Domain;
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
    public class RecruitmentController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        private readonly IRecruitment_Repository recruitmentRepository;
        public RecruitmentController(AppDbContext appDbContext, IRecruitment_Repository recruitmentRepository)
        {
            this.appDbContext = appDbContext;
            this.recruitmentRepository = recruitmentRepository;
        }

        [HttpPost ("Post")]
        public async Task<IActionResult> Post (int job_id)
        {
            try
            {
                var userId = User.FindFirst("Id")?.Value;
                var jobDomain = await appDbContext.Recruitment.FindAsync (job_id, userId);
                if (jobDomain == null) { return Ok("Bạn đã đăng kí công việc này"); }
                var create = await recruitmentRepository.Post(userId!, job_id);
                if (create == null) { return NotFound("Không tìm thấy công việc này"); }
                return Ok("Đăng kí thành công");
            }
            catch { return BadRequest ("Đăng kí không thành công"); }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete (int job_id)
        {
            try
            {
                var userId = User.FindFirst("Id")?.Value;
                var delete = await recruitmentRepository.Delete(userId!, job_id);
                if (delete == null) { return NotFound ("Không tìm thấy đăng kí công việc"); }
                return Ok("Xóa thành công");
            }
            catch { return BadRequest("Cập nhật thất bại"); }
        }

        [HttpGet("Seeker")]
        public async Task<IActionResult> Seeker()
        {
            try
            {
                var userId = User.FindFirst("Id")?.Value;
                var employer = await appDbContext.Recruitment.FindAsync(userId);
                if (employer == null) { return NotFound("Chưa có công việc đă đăng kí"); }
                var listRecruitment = await recruitmentRepository.Seeker(userId!);
                return Ok(listRecruitment);
            }
            catch { return BadRequest(); }
        }

        [HttpPatch ("Status")]
        public async Task<IActionResult> Status (int job_id)
        {
            try
            {
                var userId = User.FindFirst("Id")?.Value;
                var updateStatus = await recruitmentRepository.Status(userId!, job_id);
                if (updateStatus == null) { return NotFound("Không tìm thấy công việc đã ứng tuyển"); }
                return Ok("Cập nhật thành công");
            }
            catch { return BadRequest("Cập nhật thất bại"); }
        }
    }
}
