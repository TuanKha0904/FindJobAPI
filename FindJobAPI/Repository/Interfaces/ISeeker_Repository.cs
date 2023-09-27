using FindJobAPI.Model.DTO;

namespace FindJobAPI.Repository.Interfaces
{
    public interface ISeeker_Repository
    {
        Task<CV> CV(string userId);
/*        Task<SeekerNoId> GetById(int id);
        Task<SeekerNoId> UpdateSeeker(int id, SeekerNoId seekerNoId);
*/    }
}
