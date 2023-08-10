using FindJobAPI.Model.Domain;
using FindJobAPI.Model.Admins;

namespace FindJobAPI.Repository.Admins
{
    public interface IAdmin_repository
    {
        Task<List<AdminDTO>> GetAll();
        Task<AdminNoId> CreateAdmin(AdminNoId adminNoId);
        Task<UpdateAdmin> UpdateAdmin(string username, UpdateAdmin updateAdmin);
        Task<admin> DeleteAdmin (int id);
    }
}
