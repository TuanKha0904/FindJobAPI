using FindJobAPI.Model.Domain;
using FindJobAPI.Model.DTO;

namespace FindJobAPI.Repository.Interfaces
{
    public interface IAdmin_repository
    {
        Task<List<AdminDTO>> GetAll();
        Task<AdminDTO> GetOne(string username, string password);
        Task<AdminNoId> CreateAdmin(AdminNoId adminNoId);
        Task<UpdateAdmin> UpdateAdmin(string username, UpdateAdmin updateAdmin);
        Task<admin> DeleteAdmin(int id);
    }
}
