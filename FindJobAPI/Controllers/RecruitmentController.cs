using FindJobAPI.Data;
using FindJobAPI.Model.DTO;
using FindJobAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FindJobAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecruitmentController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        private readonly IRecruitment_Repository recruitmentRepository;
        public RecruitmentController(AppDbContext appDbContext, IRecruitment_Repository recruitmentRepository)
        {
            this.appDbContext = appDbContext;
            this.recruitmentRepository = recruitmentRepository;
        }

        [HttpGet ("Get-all-recruitment")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var RecruitmentDomain = await recruitmentRepository.GetAll();
                return Ok(RecruitmentDomain);
            }
            catch { return BadRequest(); }
        }

        [HttpGet ("Get-seeker-recruitment")]
        public async Task<IActionResult> GetSeekerRecruitment(int id)
        {
            try
            {
                var Check = await appDbContext.Recruitment.FirstOrDefaultAsync(r => r.account_id == id);
                if (Check == null) return BadRequest($"Không tìm thấy seeker có id: {id}");
                var Recruitment = await recruitmentRepository.GetSeekerRecruitment(id);
                return Ok(Recruitment);
            }
            catch { return BadRequest(); }
        }

        [HttpGet("Get-recruitment-job")]
        public async Task<IActionResult> GetRecruitmentJob (int id)
        {
            try
            {
                var Check = await appDbContext.Recruitment.FirstOrDefaultAsync(r => r.account_id == id);
                if (Check == null) return BadRequest($"Không tìm thấy job có id: {id}");
                var Recruitment = await recruitmentRepository.GetRecruitmentJob(id);
                return Ok(Recruitment);
            }
            catch { return BadRequest(); }
        }

        [HttpPost("Create-recruitment")]
        public async Task<IActionResult> CreateRecruitment (CreateRecruitment createRecruitment)
        {
            try
            {
                var Seeker = await appDbContext.Seeker.FirstOrDefaultAsync(s => s.account_id == createRecruitment.seeker_id);
                var Job = await appDbContext.Job.FirstOrDefaultAsync(j => j.job_id == createRecruitment.job_id);
                if (Seeker == null && Job == null) return BadRequest($"Không tìm thấy seeker có id: {createRecruitment.seeker_id} và job có id: {createRecruitment.job_id}");
                else if (Seeker == null) return BadRequest($"Không tìm thấy seeker có id: {createRecruitment.seeker_id}");
                else if (Job == null) return BadRequest($"Không tìm thấy job có id: {createRecruitment.job_id}");
                else
                {
                    var Add = await recruitmentRepository.CreateRecruitment(createRecruitment);
                    return Ok(Add);
                }
            }
            catch { return BadRequest(); }
        }

        [HttpPut("Update-recruitment")]
        public async Task<IActionResult> UpdateRecruitment (int seeker, int job, UpdateRecruitment updateRecruitment)
        {
            try
            {
                var Seeker = await appDbContext.Seeker.FirstOrDefaultAsync(s => s.account_id == seeker);
                var Job = await appDbContext.Job.FirstOrDefaultAsync(j => j.job_id ==job);
                if (Seeker == null && Job == null) return BadRequest($"Không tìm thấy seeker có id: {seeker} và job có id: {job}");
                else if (Seeker == null) return BadRequest($"Không tìm thấy seeker có id: {seeker}");
                else if (Job == null) return BadRequest($"Không tìm thấy job có id: {job}");
                else
                {
                    var Update = await recruitmentRepository.UpdateRecruitment(seeker,job,updateRecruitment);
                    return Ok(Update);
                }
            }
            catch { return BadRequest(); }
        }

        [HttpDelete ("Delete-recruitment")]
        public async Task<IActionResult> DeleteRecruitment (int seeker, int job)
        {
            try
            {
                var Seeker = await appDbContext.Seeker.FirstOrDefaultAsync(s => s.account_id == seeker);
                var Job = await appDbContext.Job.FirstOrDefaultAsync(j => j.job_id == job);
                if (Seeker == null && Job == null) return BadRequest($"Không tìm thấy seeker có id: {seeker} và job có id: {job}");
                else if (Seeker == null) return BadRequest($"Không tìm thấy seeker có id: {seeker}");
                else if (Job == null) return BadRequest($"Không tìm thấy job có id: {job}");
                else
                {
                    var Delete = await recruitmentRepository.DeleteRecruitment(seeker, job);
                    return Ok(Delete);
                }

            }
            catch { return BadRequest(); }
        }

    }
}
