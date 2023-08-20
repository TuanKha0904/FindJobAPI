using FindJobAPI.Model.DTO;
namespace FindJobAPI.Repository.Interfaces
{
    public interface IJob_Detail_Repository
    {
        Task<Job_DetailNoId> GetJobById(int id);
        Task<Update_JobDetail> UpdateJobDetail(int id, Update_JobDetail update_JobDetail);
        Task<Update_Status_Job> UpdateStatusJob (int id, Update_Status_Job update_Status_Job);
    }
}
