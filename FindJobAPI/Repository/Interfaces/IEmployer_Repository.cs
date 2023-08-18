using FindJobAPI.Model.DTO;

namespace FindJobAPI.Repository.Interfaces
{
    public interface IEmployer_Repository
    {
        Task<List<EmployerDTO>> GetAll();
        Task<EmployerNoId> GetById(int id);
        Task<EmployerNoId> UpdateEmployer(int id, EmployerNoId employerNoId);
    }
}
