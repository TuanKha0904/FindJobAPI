using FindJobAPI.Data;
using FindJobAPI.Model.Domain;
using FindJobAPI.Model.DTO;
using FindJobAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FindJobAPI.Repository.Queries
{
    public class RecruitmentNoAccountRepository : IRecruitmentNoAccount_Repository
    {
        private readonly AppDbContext _context;
        public RecruitmentNoAccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Create> Post(Create create)
        {
            var jobDomain = await _context.Job.FindAsync(create.job_id);
            if (jobDomain == null) { return null!; }
            var recruitment = new recruitment_no_account()
            {
                job_id = create.job_id,
                fullname = create.name,
                email = create.email,
                phone_number = create.phone,
                birthday = DateTime.Parse(create.birthday!).Date,
                address = create.address,
                major = create.major,
                experience = create.experience,
                skills = create.skills,
                education = create.education
            };
            _context.Recruitment_No_Accounts.Add(recruitment);
            await _context.SaveChangesAsync();
            return create;
        }

        public async Task<Get> Get(int id)
        {
            var check = await _context.Recruitment_No_Accounts.FirstOrDefaultAsync(r => r.recruitment_ID == id);
            if (check == null) { return null!; }
            var infor = new Get()
            {
                   name = check.fullname,
                   email = check.email,
                   phone = check.phone_number,
                   birthday = check.birthday!.Value.ToString("dd-MM-yyyy"),
                   address = check.address,
                   major = check.major,
                   experience = check.experience,
                   skills = check.skills,
                   education = check.education
            };
            return infor;
        }

        public async Task<recruitment_no_account> Delete(int id)
        {
            var check = await _context.Recruitment_No_Accounts.FirstOrDefaultAsync(r => r.recruitment_ID == id);
            if (check == null) { return null!; }
            _context.Recruitment_No_Accounts.Remove(check);
            await _context.SaveChangesAsync();
            return check;
        }

        public async Task<recruitment_no_account> Status(int id, int job_id)
        {
            var recruitment = await _context.Recruitment_No_Accounts.FindAsync(id, job_id);
            if (recruitment == null) return null!;
            recruitment.status = true;
            await _context.SaveChangesAsync();
            return recruitment;
        }
    }
}
