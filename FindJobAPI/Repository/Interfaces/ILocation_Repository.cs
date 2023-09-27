using FindJobAPI.Model.Domain;
using FindJobAPI.Model.DTO;

namespace FindJobAPI.Repository.Interfaces
{
    public interface ILocation_Repository
    {
        Task<List<LocationDTO>> GetAll();

        Task<LocationNoId> CreateLocation(LocationNoId locationNoId);

        Task<LocationNoId> UpdateLocation(int id, LocationNoId locationNoId);

        Task<location> DeleteLocation(int id);
    }
}