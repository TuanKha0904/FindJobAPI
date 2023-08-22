using FindJobAPI.Model.Domain;
using FindJobAPI.Model.DTO;

namespace FindJobAPI.Repository.Interfaces
{
    public interface IJob_Industry_Repository
    {

        Task<List<Job_IndustryDTO>> GetAll();
        Task<List<Job_IndustryNoId>> GetJobIndustryById (int id);
        Task<Job_IndustryDTO> CreateJobIndustry ( Job_IndustryDTO job_IndustryDTO);
        Task<Job_IndustryDTO> UpdateJobIndustry(int id, string job, Job_IndustryDTO job_IndustryDTO);
        Task<job_industry> DeleteJobIndustry (int id, string job);
    }
}
