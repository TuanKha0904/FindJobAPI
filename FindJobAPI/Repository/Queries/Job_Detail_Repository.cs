using FindJobAPI.Data;
using FindJobAPI.Model.Domain;
using FindJobAPI.Model.DTO;
using FindJobAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FindJobAPI.Repository.Queries
{
    public class Job_Detail_Repository : IJob_Detail_Repository
    {
        private readonly AppDbContext _context;
        public Job_Detail_Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Job_DetailNoId> GetJobById(int id)
        {
            var JobDetail = await _context.Job_Detail.FirstOrDefaultAsync(j => j.job_id == id);
            if (JobDetail == null) return null!;
            var Job = new Job_DetailNoId
            {
                job_title = JobDetail.job_title,
                job_description = JobDetail.job_description,
                location = JobDetail.location,
                requirement = JobDetail.requirement,
                minimum_salary = JobDetail.minimum_salary,
                maximum_salary = JobDetail.maximum_salary,
                status = JobDetail.status,
                industry_id = JobDetail.industry_id
            };
            return Job;
        }

        public async Task<Update_JobDetail> UpdateJobDetail(int id, Update_JobDetail update_JobDetail)
        {
            var JobDetailDomain = await _context.Job_Detail.FirstOrDefaultAsync(j => j.job_id == id);
            if (JobDetailDomain == null) return null!;
            if(string.IsNullOrEmpty(update_JobDetail.job_title))
                JobDetailDomain.job_title = update_JobDetail.job_title;
            if (string.IsNullOrEmpty(update_JobDetail.job_description))
                JobDetailDomain.job_description = update_JobDetail.job_description;
            if (string.IsNullOrEmpty(update_JobDetail.location))
                JobDetailDomain.location = update_JobDetail.location;
            if (string.IsNullOrEmpty (update_JobDetail.requirement))
                JobDetailDomain.requirement = update_JobDetail.requirement;
            if (JobDetailDomain.minimum_salary != update_JobDetail.minimum_salary) 
                JobDetailDomain.minimum_salary = update_JobDetail.minimum_salary;
            if (JobDetailDomain.maximum_salary != update_JobDetail.maximum_salary)
                JobDetailDomain.maximum_salary = update_JobDetail.maximum_salary;
            if (JobDetailDomain.industry_id != update_JobDetail.industry_id)
                JobDetailDomain.industry_id = update_JobDetail.industry_id;
            await _context.SaveChangesAsync();
            return update_JobDetail;
        }

        public async Task<Update_Status_Job> UpdateStatusJob(int id, Update_Status_Job update_Status_Job)
        {
            var JobDetailDomain = await _context.Job_Detail.FirstOrDefaultAsync(j => j.job_id == id);
            if (JobDetailDomain == null) return null!;
            JobDetailDomain.status = update_Status_Job.status;
            await _context.SaveChangesAsync();
            return update_Status_Job;
        }

    }
}
