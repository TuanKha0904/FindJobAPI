using FindJobAPI.Model.Domain;
using FindJobAPI.Model.DTO;

namespace FindJobAPI.Repository.Interfaces
{
    public interface IJob_Repository
    {
        Task<List<JobDTO>> GetAll();
        Task<JobNoId> GetById(int id);
        Task<CreateJob> CreateJob (CreateJob createJob);
        Task<UpdateJob> UpdateJob(int id, UpdateJob updateJob);
        Task<job> DeleteJob(int id);
    }
}
