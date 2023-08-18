using FindJobAPI.Model.DTO;

namespace FindJobAPI.Repository.Interfaces
{
    public interface ISeeker_Repository
    {
        Task<List<SeekerDTO>> GetAll();
        Task<SeekerNoId> GetById(int id);
        Task<SeekerNoId> UpdateSeeker(int id, SeekerNoId seekerNoId);
    }
}
