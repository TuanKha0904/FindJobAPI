using FindJobAPI.Data;
using FindJobAPI.Model.Domain;
using FindJobAPI.Model.DTO;
using FindJobAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FindJobAPI.Repository.Queries
{
    public class Job_Repository : IJob_Repository
    {
        private readonly AppDbContext _appDbContext;

        public Job_Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<JobDTO>> GetAll()
        {
            var AllJob =  _appDbContext.Job.AsQueryable();
            var ListJob = await AllJob.Select(job => new JobDTO
            {
                job_id = job.job_id,
                account_id = job.account_id,
                type_id = job.type_id,
                posted_date = job.posted_date,  
                deadline = job.deadline,
            }).ToListAsync();
            return ListJob;
        }

        public async Task<JobNoId> GetById(int id)
        {
            var JobDomain = await _appDbContext.Job.FirstOrDefaultAsync(j => j.job_id == id);
            if (JobDomain == null)
                return null!;
            var Job = new JobNoId
            {
                account_id = JobDomain.account_id,
                type_id = JobDomain.type_id,
                posted_date = JobDomain.posted_date,
                deadline = JobDomain.deadline
            };
            return Job;
        }

        public async Task<CreateJob> CreateJob(CreateJob createJob)
        {
            var CreateJob = new job
            {
                account_id = createJob.account_id,
                type_id = createJob.type_id,
                posted_date = DateTime.Now,
                deadline = createJob.deadline,
            };
            await _appDbContext.Job.AddAsync(CreateJob);
            await _appDbContext.SaveChangesAsync();
            var JobDetail = new job_detail
            {
                job_id = CreateJob.job_id,
                status = false
            };
            await _appDbContext.Job_Detail.AddAsync(JobDetail);
            await _appDbContext.SaveChangesAsync();
            return createJob;
        }

        public async Task<UpdateJob> UpdateJob(int id, UpdateJob updateJob)
        {
            var JobDomain = await _appDbContext.Job.FirstOrDefaultAsync(j => j.job_id == id);
            if ( JobDomain == null) return null!;
                JobDomain.type_id = updateJob.type_id;
            if (updateJob.deadline != JobDomain.deadline )
                JobDomain.deadline = updateJob.deadline;
            await _appDbContext.SaveChangesAsync();
            return updateJob;
        }

        public async Task<job> DeleteJob(int id)
        {
            var JobDomain = await _appDbContext.Job.FirstOrDefaultAsync(j => j.job_id == id);
            var JobDetailDomain = await _appDbContext.Job_Detail.FirstOrDefaultAsync (j => j.job_id == id);
            if( JobDomain == null ) return null!;
            _appDbContext.Job_Detail.Remove(JobDetailDomain!);
            _appDbContext.Job.Remove(JobDomain);
            await _appDbContext.SaveChangesAsync();
            return JobDomain;
        }
    }
}
