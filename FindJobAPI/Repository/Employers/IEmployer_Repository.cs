using FindJobAPI.Model.Employers;
using FindJobAPI.Model.Seekers;

namespace FindJobAPI.Repository.Employers
{
    public interface IEmployer_Repository
    {
        Task<List<EmployerDTO>> GetAll();
        Task<EmployerNoId> GetById(int id);
        Task<EmployerNoId> UpdateEmployer(int id, EmployerNoId employerNoId);
    }
}
