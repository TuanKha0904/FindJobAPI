﻿using FindJobAPI.Data;
using FindJobAPI.Model.Domain;
using FindJobAPI.Model.DTO;
using FindJobAPI.Repository.Interfaces;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Microsoft.EntityFrameworkCore;

namespace FindJobAPI.Repository.Queries
{
    public class Recruitment_Repository : IRecruitment_Repository
    {
        private readonly AppDbContext _appDbContext;
        private readonly FirebaseAuth _firebaseAuth;
        public Recruitment_Repository(AppDbContext appDbContext, FirebaseApp firebaseApp)
        {
            _appDbContext = appDbContext;
            _firebaseAuth = FirebaseAuth.GetAuth(firebaseApp);
        }

        public async Task<recruitment> Post(string userId, int job_id)
        {
            var job = await _appDbContext.Job.FindAsync(job_id);
            if (job == null) { return null!; }
            var recruitment = new recruitment()
            {
                UID = userId,
                job_id = job_id,
                status = false,
                registation_date = DateTime.Now.Date,
            };
            _appDbContext.Recruitment.Add(recruitment);
            await _appDbContext.SaveChangesAsync();
            return recruitment;
        }

        public async Task<recruitment> Delete(string userId, int job_id)
        {
            var recruitment = await _appDbContext.Recruitment.FindAsync(userId, job_id);
            if(recruitment == null) { return null!; }
            _appDbContext.Recruitment.Remove(recruitment);
            await _appDbContext.SaveChangesAsync();
            return recruitment;
        }

        public async Task<List<Seeker>> Seeker(string userId)
        {
            var allRecruitment =  _appDbContext.Recruitment.AsQueryable();
            var listRecruitment = await allRecruitment.Where(r => r.UID == userId).Select(recruitment => new Seeker()
            {
                id = recruitment.job_id,
                job_title = recruitment.job!.job_title,
                minimum_salary = recruitment.job.minimum_salary,
                maximum_salary = recruitment.job.maximum_salary,
                location = recruitment.job.location!.location_name,
                industry = recruitment.job.industry!.industry_name,
                type = recruitment.job.type!.type_name,
                logo = recruitment.job.employer!.employer_image
            }).ToListAsync();
            return listRecruitment;
        }

        public async Task<recruitment> Status(string userId, int job_id)
        {
            var recruitment = await _appDbContext.Recruitment.FindAsync(userId, job_id);
            if (recruitment == null) return null!;
            recruitment.status = true;
            await _appDbContext.SaveChangesAsync();
            return recruitment;
        }
    }
}
