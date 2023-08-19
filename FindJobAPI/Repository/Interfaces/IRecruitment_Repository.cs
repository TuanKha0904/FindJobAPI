using FindJobAPI.Model.Domain;
using FindJobAPI.Model.DTO;

namespace FindJobAPI.Repository.Interfaces
{
    public interface IRecruitment_Repository
    {
        Task<List<RecruitmentDTO>> GetAll();
        Task<List<SeekerRecruitment>> GetSeekerRecruitment(int id);
        Task<List<RecruitmentJob>> GetRecruitmentJob(int id);
        Task<CreateRecruitment> CreateRecruitment(CreateRecruitment createRecruitment);
        Task<UpdateRecruitment> UpdateRecruitment (int seeker, int job, UpdateRecruitment updateRecruitment);
        Task<recruitment> DeleteRecruitment (int seeker, int job);
    }
}
