using FindJobAPI.Data;
using FindJobAPI.Model.Domain;
using FindJobAPI.Model.DTO;
using FindJobAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FindJobAPI.Repository.Queries
{
    public class Job_Industry_Repository : IJob_Industry_Repository
    {
        private readonly AppDbContext _appDbContext;
        public Job_Industry_Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Job_IndustryDTO>> GetAll()
        {
            var AllJob =  _appDbContext.Job_Industry.AsQueryable();
            var ListJob = await AllJob.Select(job => new Job_IndustryDTO
            {
                id = job.industry_id,
                job = job.job
            }).ToListAsync();
            return ListJob;
        }

        public async Task<List<Job_IndustryNoId>> GetJobIndustryById(int id)
        {
            var Domain = await _appDbContext.Job_Industry.Where(d => d.id == id).Select(job => new Job_IndustryNoId
            {
                job = job.job
            }).ToListAsync();
            if (Domain == null) return null!;
            return Domain;
        }

        public async Task<Job_IndustryDTO> CreateJobIndustry(Job_IndustryDTO job_IndustryDTO)
        {
            var Add = await _appDbContext.Job_Industry.FirstOrDefaultAsync(j => j.industry_id == job_IndustryDTO.id && j.job == job_IndustryDTO.job);
            if (Add == null) return null!;
            var AddJob = new job_industry
            {
                industry_id = job_IndustryDTO.id,
                job = job_IndustryDTO.job
            };
            _appDbContext.Job_Industry.Add(AddJob);
            await _appDbContext.SaveChangesAsync();
            return job_IndustryDTO;
        }

        public async Task<Job_IndustryDTO> UpdateJobIndustry(int id, string job, Job_IndustryDTO job_IndustryDTO)
        {
            var Domain = await _appDbContext.Job_Industry.FirstOrDefaultAsync(j => j.industry_id == id && j.job == job);
            if (Domain == null) return null!;
            if (job_IndustryDTO.id != id)
                Domain.industry_id = job_IndustryDTO.id;
            if (job_IndustryDTO.job != job)
                Domain.job = job_IndustryDTO.job;
            await _appDbContext.SaveChangesAsync();
            return job_IndustryDTO;
    
        }

        public async Task<job_industry> DeleteJobIndustry(int id, string job)
        {
            var Domain = await _appDbContext.Job_Industry.FirstOrDefaultAsync(j => j.industry_id == id && j.job == job);
            if (Domain == null) return null!;
            _appDbContext.Job_Industry.Remove(Domain);
            await _appDbContext.SaveChangesAsync();
            return Domain;
        }
    }
}
