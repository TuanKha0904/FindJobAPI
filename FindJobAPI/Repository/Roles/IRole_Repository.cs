using FindJobAPI.Model.Admins;
using FindJobAPI.Model.Domain;
using FindJobAPI.Model.Roles;

namespace FindJobAPI.Repository.Roles
{
    public interface IRole_Repository
    {
        Task<List<RoleDTO>> GetAll();
        Task<RoleNoId> CreateRole(RoleNoId roleNoId);
        Task<RoleNoId> UpdateRole(int id, RoleNoId roleNoId);
        Task<role> DeleteRole(int id);
    }
}
