using FindJobAPI.Model.Seekers;
namespace FindJobAPI.Repository.Seekers
{
    public interface ISeeker_Repository
    {
        Task<List<SeekerDTO>> GetAll();
        Task<SeekerNoId> GetById(int id);
        Task<SeekerNoId> UpdateSeeker(int id, SeekerNoId seekerNoId);
    }
}
