using FindJobAPI.Data;
using FindJobAPI.Model.Domain;
using FindJobAPI.Model.DTO;
using FindJobAPI.Repository.Interfaces;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Microsoft.EntityFrameworkCore;

namespace FindJobAPI.Repository.Queries
{
    public class Job_Repository : IJob_Repository
    {
        private readonly AppDbContext _appDbContext;
        private readonly FirebaseAuth _firebaseAuth;

        public Job_Repository(AppDbContext appDbContext, FirebaseApp firebaseApp)
        {
            _appDbContext = appDbContext;
            _firebaseAuth = FirebaseAuth.GetAuth(firebaseApp);
        }

        public async Task<List<AllJob>> AllJobPost()
        {
            var allJobDomain =  _appDbContext.Job.AsQueryable();
            var listJob = await allJobDomain.Where(job => job.status == true).Select(job => new AllJob(){
                id = job.job_id,
                JobTitle = job.job_title,
                Location = job.location!.location_name,
                Requirement = job.requirement,
                Minimum_Salary = job.minimum_salary,
                Maximum_Salary = job.maximum_salary
            }).ToListAsync();
            return listJob;
        }

        public async Task<List<AllJob>> AllJobWait()
        {
            var allJobDomain = _appDbContext.Job.AsQueryable();
            var listJob = await allJobDomain.Where(job => job.status == false).Select(job => new AllJob()
            {
                id = job.job_id,
                JobTitle = job.job_title,
                Location = job.location!.location_name,
                Requirement = job.requirement,
                Minimum_Salary = job.minimum_salary,
                Maximum_Salary = job.maximum_salary
            }).ToListAsync();
            return listJob;
        }

        public async Task<List<AllJob>> AllJobTimeOut()
        {
            var allJobDomain = _appDbContext.Job.AsQueryable();
            var listJob = await allJobDomain.Where(job => job.deadline.Date < DateTime.Now.Date).Select(job => new AllJob()
            {
                id = job.job_id,
                JobTitle = job.job_title,
                Location = job.location!.location_name,
                Requirement = job.requirement,
                Minimum_Salary = job.minimum_salary,
                Maximum_Salary = job.maximum_salary
            }).ToListAsync();
            return listJob;
        }

        public async Task<JobDetail> JobDetail(int jobId)
        {
            var jobDomain = await _appDbContext.Job.FirstOrDefaultAsync(j =>  j.job_id == jobId);
            if (jobDomain == null) { return null!; }
            var jobDetail = new JobDetail()
            {
                jobTitle = jobDomain.job_title,
                jobDescription = jobDomain.job_description,
                minimum_salary = jobDomain.minimum_salary,
                maximum_salary = jobDomain.maximum_salary,
                requirement = jobDomain.requirement,
                location = jobDomain.location!.location_name,
                deadline = jobDomain.deadline.ToString("dd-MM-yyyy"),
                industry = jobDomain.industry!.industry_name,
                type = jobDomain.type!.type_name,
                posted_date = jobDomain.posted_date.ToString("dd-MM-yyyy"),
                employer_name = jobDomain.employer!.employer_name,
                email = jobDomain.employer.email,
                website = jobDomain.employer.employer_website,
                contact = jobDomain.employer.contact_phone,
                address = jobDomain.employer.employer_address
            };
            return jobDetail;
        }

        public async Task<List<ListJob>> JobPostList(string userId)
        {
            var employer = await _appDbContext.Employer.FirstOrDefaultAsync(e => e.UID == userId);
            var allJob = _appDbContext.Job.AsQueryable();
            var listJob = await allJob.Where(j => j.status == true && j.UID == userId).Select(job => new ListJob()
            {
                id = job.job_id,
                jobTitle = job.job_title,
                minimum_salary = job.minimum_salary,
                maximum_salary = job.maximum_salary,
                location = job.location!.location_name,
                industry = job.industry!.industry_name,
                type = job.type!.type_name,
                logo = employer!.employer_image,
                deadline = job.deadline.ToString("dd-MM-yyyy"),
            }).ToListAsync();
            return listJob;
        }

        public async Task<List<ListJob>> JobWaitList(string userId)
        {
            var employer = await _appDbContext.Employer.FirstOrDefaultAsync(e => e.UID == userId);
            var allJob = _appDbContext.Job.AsQueryable();
            var listJob = await allJob.Where(j => j.status == false && j.UID == userId).Select(job => new ListJob()
            {
                id = job.job_id,
                jobTitle = job.job_title,
                minimum_salary = job.minimum_salary,
                maximum_salary = job.maximum_salary,
                location = job.location!.location_name,
                industry = job.industry!.industry_name,
                type = job.type!.type_name,
                logo = employer!.employer_image,
                deadline = job.deadline.ToString("dd-MM-yyyy"),
            }).ToListAsync();
            return listJob;
        }

        public async Task<List<ApplyList>> ApplyList(int job_id)
        {
            var recruitmentAccount = _appDbContext.Recruitment.AsQueryable();
            var recruimentNoAccount = _appDbContext.Recruitment_No_Accounts.AsQueryable();
            var accountRecruitment = await recruitmentAccount.Where(r => r.job_id == job_id && r.status == false)
                .Select(recruitment => new ApplyList()
                {
                    UID = recruitment.UID,
                    Name = recruitment.seeker!.Name,
                    Email = recruitment.seeker.Email,
                    PhoneNumber = recruitment.seeker.PhoneNumber
                }).ToListAsync() ;
            var noAccountRecruitment = await recruimentNoAccount.Where(r => r.job_id == job_id && r.status == false)
                .Select(recruitment => new ApplyList()
                {
                    UID = recruitment.recruitment_ID.ToString(),
                    Name = recruitment.fullname,
                    Email = recruitment.email,
                    PhoneNumber = recruitment.phone_number
                }).ToListAsync();
           var listRecruitment = accountRecruitment.Concat(noAccountRecruitment).ToList() ;
            return listRecruitment;
        }

        public async Task<List<ApplyList>> Receive(int job_id)
        {
            var recruitmentAccount = _appDbContext.Recruitment.AsQueryable();
            var recruimentNoAccount = _appDbContext.Recruitment_No_Accounts.AsQueryable();
            var accountRecruitment = await recruitmentAccount.Where(r => r.job_id == job_id && r.status == true)
                .Select(recruitment => new ApplyList()
                {
                    UID = recruitment.UID,
                    Name = recruitment.seeker!.Name,
                    Email = recruitment.seeker.Email,
                    PhoneNumber = recruitment.seeker.PhoneNumber
                }).ToListAsync();
            var noAccountRecruitment = await recruimentNoAccount.Where(r => r.job_id == job_id && r.status == true)
                .Select(recruitment => new ApplyList()
                {
                    UID = recruitment.recruitment_ID.ToString(),
                    Name = recruitment.fullname,
                    Email = recruitment.email,
                    PhoneNumber = recruitment.phone_number
                }).ToListAsync();
            var listRecruitment = accountRecruitment.Concat(noAccountRecruitment).ToList();
            return listRecruitment;
        }

        public async Task<CreateJob> CreateJob(string userId, CreateJob createJob)
        {
            var employerDomain = await _appDbContext.Employer.FirstOrDefaultAsync(e => e.UID == userId);
            if (employerDomain == null) { return null!; }
            var Job = new job()
            {
                UID = userId,
                job_title = createJob.JobTitle,
                minimum_salary = createJob.Minimum_Salary,
                maximum_salary = createJob.Maximum_Salary,
                location_id = createJob.Location_id,
                industry_id = createJob.Industry_id,
                type_id = createJob.Location_id,
                deadline = DateTime.Parse(createJob.Deadline!),
                posted_date = DateTime.Now.Date,
                status = false
            };
            if(!string.IsNullOrEmpty(createJob.JobDescription)) 
                Job.job_description = createJob.JobDescription;
            if (!string.IsNullOrEmpty(createJob.Requirement))
                Job.requirement = createJob.Requirement;
            _appDbContext.Job.Add(Job);
            await _appDbContext.SaveChangesAsync();
            return createJob;
        }

        public async Task<List<AllJob>> Search(int industry_id, int type_id, int location_id)
        {
            var allJob = _appDbContext.Job.AsQueryable();
            var searchJob = await allJob
                .Where(j => j.industry_id == industry_id || j.type_id == type_id || j.location_id == location_id)
                .Select(job => new AllJob()
                {
                    id = job.job_id,
                    JobTitle = job.job_title,
                    Location = job.location!.location_name,
                    Requirement = job.requirement,
                    Minimum_Salary = job.minimum_salary,
                    Maximum_Salary = job.maximum_salary
                }).ToListAsync();
            return searchJob;
        }

        public async Task<UpdateJob> Update(int job_id, UpdateJob updateJob)
        {
            var jobDomain = await _appDbContext.Job.FirstOrDefaultAsync(j => j.job_id == job_id);
            if (jobDomain == null) { return null!; }
            if (!string.IsNullOrEmpty(updateJob.JobTitle)) 
                jobDomain.job_title = updateJob.JobTitle;
            if(!string.IsNullOrEmpty(updateJob.JobDescription))
                jobDomain.job_description = updateJob.JobDescription;
            if(updateJob.Minimum_Salary != 0 && updateJob.Minimum_Salary != jobDomain.minimum_salary)
                jobDomain.minimum_salary = updateJob.Minimum_Salary;
            if (updateJob.Maximum_Salary != 0 && updateJob.Maximum_Salary != jobDomain.maximum_salary)
                jobDomain.maximum_salary = updateJob.Maximum_Salary;
            if (!string.IsNullOrEmpty(updateJob.Requirement))
                jobDomain.requirement = updateJob.Requirement;
            if(updateJob.Location_id != 0)
                jobDomain.location_id = updateJob.Location_id;
            if(updateJob.Industry_id != 0)
                jobDomain.industry_id = updateJob.Industry_id;
            if(updateJob.Type_id != 0)
                jobDomain.type_id = updateJob.Type_id;
            if (!string.IsNullOrEmpty(updateJob.Deadline) && 
                (DateTime.Parse(updateJob.Deadline) != jobDomain.deadline && DateTime.Parse(updateJob.Deadline) != DateTime.Now.Date))
                    jobDomain.deadline = DateTime.Parse(updateJob.Deadline);
            await _appDbContext.SaveChangesAsync();
            return updateJob;
        }

        public async Task<job> Delete(int jobId)
        {
            var job = await _appDbContext.Job.FirstOrDefaultAsync(j => j.job_id == jobId);
            if (job != null)
            {
                _appDbContext.Job.Remove(job);
                await _appDbContext.SaveChangesAsync();
            }
            return null!;
        }

    }
}
