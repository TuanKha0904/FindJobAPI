/*using FindJobAPI.Data;
using FindJobAPI.Model.Domain;
using FindJobAPI.Model.DTO;
using FindJobAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FindJobAPI.Repository.Queries
{
    public class Seeker_Repository : ISeeker_Repository
    {
        private readonly AppDbContext _appDbContext;
        public Seeker_Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<SeekerDTO>> GetAll()
        {
            var AllSeeker = _appDbContext.Seeker.AsQueryable();
            var ListSeeker = await AllSeeker.Select(seeker => new SeekerDTO
            {
                seeker_id = seeker.account_id,
                first_name = seeker.first_name,
                last_name = seeker.last_name,
                address = seeker.address,
                birthday = seeker.birthday,
                phone_number = seeker.phone_number,
                seeker_image = seeker.seeker_image,
                academic_level = seeker.academic_level,
                skills = seeker.skills,
                url_cv = seeker.url_cv,
                website_seeker = seeker.website_seeker,
            }).ToListAsync();
            return ListSeeker;
        }

        public async Task<SeekerNoId> GetById(int id)
        {
            var SeekerDomain = await _appDbContext.Seeker.FirstOrDefaultAsync(s => s.account_id == id);
            if (SeekerDomain == null)
                return null!;
            var Seeker = new SeekerNoId
            {
                first_name = SeekerDomain.first_name,
                last_name = SeekerDomain.last_name,
                address = SeekerDomain.address,
                birthday = SeekerDomain.birthday,
                phone_number = SeekerDomain.phone_number,
                seeker_image = SeekerDomain.seeker_image,
                academic_level = SeekerDomain.academic_level,
                skills = SeekerDomain.skills,
                url_cv = SeekerDomain.url_cv,
                website_seeker = SeekerDomain.website_seeker,
            };
            return Seeker;
        }

        public async Task<SeekerNoId> UpdateSeeker(int id, SeekerNoId seekerNoId)
        {
            var SeekerDomain = await _appDbContext.Seeker.FirstOrDefaultAsync(s => s.account_id == id);
            if (SeekerDomain == null)
                return null!;
            if (!string.IsNullOrEmpty(seekerNoId.first_name))
                SeekerDomain.first_name = seekerNoId.first_name;
            if (!string.IsNullOrEmpty(seekerNoId.last_name))
                SeekerDomain.last_name = seekerNoId.last_name;
            if (!string.IsNullOrEmpty(seekerNoId.address))
                SeekerDomain.address = seekerNoId.address;
            if (seekerNoId.birthday.Date != DateTime.Today.Date)
                SeekerDomain.birthday = seekerNoId.birthday;
            if (!string.IsNullOrEmpty(seekerNoId.phone_number))
                SeekerDomain.phone_number = seekerNoId.phone_number;
            if (!string.IsNullOrEmpty(seekerNoId.seeker_image))
                SeekerDomain.seeker_image = seekerNoId.seeker_image;
            if (!string.IsNullOrEmpty(seekerNoId.academic_level))
                SeekerDomain.academic_level = seekerNoId.academic_level;
            if (!string.IsNullOrEmpty(seekerNoId.skills))
                SeekerDomain.skills = seekerNoId.skills;
            if (!string.IsNullOrEmpty(seekerNoId.url_cv))
                SeekerDomain.url_cv = seekerNoId.url_cv;
            if (!string.IsNullOrEmpty(seekerNoId.website_seeker))
                SeekerDomain.website_seeker = seekerNoId.website_seeker;
            await _appDbContext.SaveChangesAsync();
            return seekerNoId;
        }
    }
}
*/