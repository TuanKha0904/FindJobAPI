/*using FindJobAPI.Data;
using FindJobAPI.Model.Domain;
using FindJobAPI.Model.DTO;
using FindJobAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FindJobAPI.Repository.Queries
{
    public class Employer_Repository : IEmployer_Repository
    {
        private readonly AppDbContext _appDbContext;

        public Employer_Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<EmployerDTO>> GetAll()
        {
            var AllEmployer = _appDbContext.Employer.AsQueryable();
            var ListEmployer = await AllEmployer.Select(employer => new EmployerDTO
            {
                employer_id = employer.account_id,
                employer_name = employer.employer_name,
                employer_about = employer.employer_about,
                employer_address = employer.employer_address,
                contact_phone = employer.contact_phone,
                employer_image = employer.employer_image,
                employer_website = employer.employer_website
            }).ToListAsync();
            return ListEmployer;
        }

        public async Task<EmployerNoId> GetById(int id)
        {
            var EmployerDomain = await _appDbContext.Employer.FirstOrDefaultAsync(e => e.account_id == id);
            if (EmployerDomain == null)
                return null!;
            var Employer = new EmployerNoId
            {
                employer_name = EmployerDomain.employer_name,
                employer_about = EmployerDomain.employer_about,
                employer_address = EmployerDomain.employer_address,
                contact_phone = EmployerDomain.contact_phone,
                employer_image = EmployerDomain.employer_image,
                employer_website = EmployerDomain.employer_website
            };
            return Employer;
        }

        public async Task<EmployerNoId> UpdateEmployer(int id, EmployerNoId employerNoId)
        {
            var EmployerDomain = await _appDbContext.Employer.FirstOrDefaultAsync(e => e.account_id == id);
            if (EmployerDomain == null)
                return null!;
            if (!string.IsNullOrEmpty(employerNoId.employer_name))
                EmployerDomain.employer_name = employerNoId.employer_name;
            if (!string.IsNullOrEmpty(employerNoId.employer_about))
                EmployerDomain.employer_about = employerNoId.employer_about;
            if (!string.IsNullOrEmpty(employerNoId.employer_address))
                EmployerDomain.employer_address = employerNoId.employer_address;
            if (!string.IsNullOrEmpty(employerNoId.contact_phone))
                EmployerDomain.contact_phone = employerNoId.contact_phone;
            if (!string.IsNullOrEmpty(employerNoId.employer_name))
                EmployerDomain.employer_name = employerNoId.employer_image;
            if (!string.IsNullOrEmpty(employerNoId.employer_name))
                EmployerDomain.employer_image = employerNoId.employer_image;
            if (!string.IsNullOrEmpty(employerNoId.employer_website))
                EmployerDomain.employer_website = employerNoId.employer_website;
            await _appDbContext.SaveChangesAsync();
            return employerNoId;
        }
    }
}
*/