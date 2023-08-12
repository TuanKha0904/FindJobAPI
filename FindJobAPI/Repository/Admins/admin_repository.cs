using FindJobAPI.Data;
using FindJobAPI.Model.Domain;
using FindJobAPI.Model.Admins;
using Microsoft.EntityFrameworkCore;

namespace FindJobAPI.Repository.Admins
{
    public class admin_repository : IAdmin_repository
    {
        private readonly AppDbContext _appDbContext;
        public admin_repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<AdminDTO>> GetAll()
        {
            var AllAdmin = _appDbContext.Admin.AsQueryable();
            var ListAdmin = await AllAdmin.Select(admin => new AdminDTO
            {
                Admin_Id = admin.admin_id,
                UserName = admin.username, 
                Password = admin.password
            }).ToListAsync();
            return ListAdmin!;
        }

        public async Task<AdminNoId> CreateAdmin(AdminNoId adminNoId)
        {
            var existingAdmin = await _appDbContext.Admin.FirstOrDefaultAsync(a => a.username == adminNoId.UserName);
            if (existingAdmin != null)
            {
                return null!;
            }
            var adminDomain = new admin
            {
                username = adminNoId.UserName, 
                password = adminNoId.Password
            };
            await _appDbContext.Admin.AddAsync(adminDomain);
            await _appDbContext.SaveChangesAsync();
            return adminNoId;
        }

        public async Task<UpdateAdmin> UpdateAdmin(string username, UpdateAdmin updateAdmin)
        {
            var AdminDomain = _appDbContext.Admin!.FirstOrDefault(a => a.username == username);
            if (AdminDomain != null)
            {
                AdminDomain.password = updateAdmin.Password;
                await _appDbContext.SaveChangesAsync();
                return updateAdmin;
            }
            return null!;
        }

        public async Task<admin> DeleteAdmin(int id)
        {
            var AdminDomain = _appDbContext.Admin!.SingleOrDefault(a => a.admin_id == id);
            if (AdminDomain != null)
            {
                _appDbContext.Admin.Remove(AdminDomain);
                await _appDbContext.SaveChangesAsync();
            }
            else return null!;
            return AdminDomain;
        }
    }
}
