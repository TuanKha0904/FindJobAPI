/*using FindJobAPI.Data;
using FindJobAPI.Model.Domain;
using FindJobAPI.Model.DTO;
using FindJobAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FindJobAPI.Repository.Queries
{
    public class Recruitment_Repository : IRecruitment_Repository
    {
        private readonly AppDbContext _appDbContext;
        public Recruitment_Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<RecruitmentDTO>> GetAll()
        {
            var AllRecruitment =  _appDbContext.Recruitment.AsQueryable();
            var ListRecruitment = await AllRecruitment.Select(recruit => new RecruitmentDTO
            {
                seeker_id = recruit.account_id,
                job_id = recruit.job_id,
                seeker_desire = recruit.seeker_desire,
                registration_date = recruit.registation_date
            }).ToListAsync();
            return ListRecruitment;
        }

        public async Task<List<SeekerRecruitment>> GetSeekerRecruitment(int id)
        {
            var SeekerDomain = await _appDbContext.Recruitment.Where(r => r.account_id == id).Select(r => new SeekerRecruitment
            {
                job_id = r.job_id,
                seeker_desire = r.seeker_desire,
                registration_date = r.registation_date
            }).ToListAsync();
            return SeekerDomain;
        }

        public async Task<List<RecruitmentJob>> GetRecruitmentJob(int id)
        {
            var JobDomain = await _appDbContext.Recruitment.Where(r => r.job_id == id).Select(r => new RecruitmentJob
            {
                seeker_id = r.account_id,
                seeker_desire = r.seeker_desire,
                registration_date = r.registation_date
            }).ToListAsync();
            return JobDomain;
        }

        public async Task<CreateRecruitment> CreateRecruitment(CreateRecruitment createRecruitment)
        {
            var CreateRecruitment = new recruitment
            {
                account_id = createRecruitment.seeker_id,
                job_id = createRecruitment.job_id,
                seeker_desire = createRecruitment.seeker_desire,
                registation_date = DateTime.Now
            };
            await _appDbContext.Recruitment.AddAsync(CreateRecruitment);
            await _appDbContext.SaveChangesAsync();
            return createRecruitment;
        }

        public async Task<UpdateRecruitment> UpdateRecruitment(int seeker, int job, UpdateRecruitment updateRecruitment)
        {
            var RecruitmentDomain = await _appDbContext.Recruitment.FirstOrDefaultAsync(r => r.account_id == seeker && r.job_id == job);
            if (RecruitmentDomain == null) return null!;
            RecruitmentDomain.seeker_desire = updateRecruitment.seeker_desire;
            await _appDbContext.SaveChangesAsync();
            return updateRecruitment;
        }

        public async Task<recruitment> DeleteRecruitment(int seeker, int job)
        {
            var RecruitmentDomain = await _appDbContext.Recruitment.FirstOrDefaultAsync(r => r.account_id == seeker && r.job_id == job);
            if (RecruitmentDomain == null) return null!;
            _appDbContext.Recruitment.Remove(RecruitmentDomain);
            await _appDbContext.SaveChangesAsync();
            return RecruitmentDomain;
        }
    }
}
*/