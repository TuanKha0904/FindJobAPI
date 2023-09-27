using FindJobAPI.Data;
using FindJobAPI.Model.Domain;
using FindJobAPI.Model.DTO;
using FindJobAPI.Repository.Interfaces;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Microsoft.EntityFrameworkCore;

namespace FindJobAPI.Repository.Queries
{
    public class Seeker_Repository : ISeeker_Repository
    {
        private readonly AppDbContext _appDbContext;
        private readonly FirebaseAuth _firebaseAuth;
        public Seeker_Repository(AppDbContext appDbContext, FirebaseApp firebaseApp)
        {
            _appDbContext = appDbContext;
            _firebaseAuth = FirebaseAuth.GetAuth(firebaseApp);
        }

        public async Task<CV> CV(string userId)
        {
            var seekerDomain = await _appDbContext.Seeker.FirstOrDefaultAsync(s => s.UID == userId);
            var accountFB = await _firebaseAuth.GetUserAsync(userId);
            if (seekerDomain == null || accountFB == null) return null!;
            var seeker = new CV()
            {
                Name = accountFB.DisplayName,
                Email = accountFB.Email,
                Phone_Number = accountFB.PhoneNumber,
                Birthday = seekerDomain.birthday!.Value.ToString("dd-MM-yyyy"),
                Address = seekerDomain.address,
                Experience = seekerDomain.experience,
                Skills = seekerDomain.skills,
                Education = seekerDomain.education,
                Major = seekerDomain.major
            };
            return seeker;
        }

/*        public async Task<SeekerNoId> GetById(int id)
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
*/
/*        public async Task<SeekerNoId> UpdateSeeker(int id, SeekerNoId seekerNoId)
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
*/    }
}
