using FindJobAPI.Data;
using FindJobAPI.Model.Admins;
using FindJobAPI.Model.Domain;
using FindJobAPI.Model.Roles;
using Microsoft.EntityFrameworkCore;

namespace FindJobAPI.Repository.Roles
{
    public class Role_Repository : IRole_Repository
    {
        private readonly AppDbContext _appDbContext;
        public Role_Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<RoleDTO>> GetAll()
        {
            var AllRole = _appDbContext.Role.AsQueryable();
            var ListRole = await AllRole.Select(role => new RoleDTO
            {
                role_id = role.role_id,
                role_name = role.role_name,
            }).ToListAsync();
            return ListRole;
        }
        public async Task<RoleNoId> CreateRole(RoleNoId roleNoId)
        {
            var existingRole = await _appDbContext.Role.FirstOrDefaultAsync(r => r.role_name == roleNoId.role_name);
            if (existingRole != null)
            {
                return null!;
            }
            var RoleDomain = new role
            {
                role_name = roleNoId.role_name,
            };
            await _appDbContext.Role.AddAsync(RoleDomain);
            await _appDbContext.SaveChangesAsync();
            return roleNoId;
        }

        public async Task<RoleNoId> UpdateRole(int id, RoleNoId roleNoId)
        {
            var RoleDomain = _appDbContext.Role!.FirstOrDefault(r => r.role_id == id);
            if (RoleDomain != null)
            {
                RoleDomain.role_name = roleNoId.role_name;
                await _appDbContext.SaveChangesAsync();
                return roleNoId;
            }
            return null!;
        }

        public async Task<role> DeleteRole(int id)
        {
            var RoleDomain = _appDbContext.Role!.SingleOrDefault(r => r.role_id == id);
            if (RoleDomain != null)
            {
                _appDbContext.Role.Remove(RoleDomain);
                await _appDbContext.SaveChangesAsync();
            }
            else return null!;
            return RoleDomain;
        }
    }
}
