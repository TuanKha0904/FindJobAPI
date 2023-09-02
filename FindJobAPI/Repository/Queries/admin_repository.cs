using FindJobAPI.Data;
using FindJobAPI.Model.Domain;
using Microsoft.EntityFrameworkCore;
using FindJobAPI.Model.DTO;
using FindJobAPI.Repository.Interfaces;

namespace FindJobAPI.Repository.Queries
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

        public async Task<AdminDTO> GetOne(string username, string password)
        {
            var AdminDomain = await _appDbContext.Admin.FirstOrDefaultAsync(a => a.username == username && a.password == password);
            if (AdminDomain == null) return null!;
            var Admin = new AdminDTO
            {
                Admin_Id = AdminDomain.admin_id,
                UserName = AdminDomain.username,
                Password = AdminDomain.password
            };
            return Admin;
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
