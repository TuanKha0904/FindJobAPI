using FindJobAPI.Model.Domain;
using FindJobAPI.Model.DTO;

namespace FindJobAPI.Repository.Interfaces
{
    public interface IJob_Repository
    {
        Task<CreateJob> CreateJob(string userId, CreateJob createJob);
        Task<List<AllJob>> AllJobPost();
        Task<List<AllJob>> AllJobWait();
        Task<List<AllJob>> AllJobTimeOut();
        Task<JobDetail> JobDetail(int jobId);
        Task<List<ListJob>> JobPostList (string userId);
        Task<List<ListJob>> JobWaitList(string userId);
        Task<List<ListJob>> JobTimeoutList(string userId);
        Task<List<AllJob>> Search(int industry_id, int type_id, int location_id);
        Task<UpdateJob> Update(int job_id, UpdateJob updateJob);
        Task<job> Delete(int jobId);
        Task<List<ApplyList>> ApplyList(int job_id);
        Task<List<ApplyList>> Receive(int job_id);
        Task<job> Status(int job_id);
    }
}