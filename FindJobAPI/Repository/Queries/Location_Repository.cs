using FindJobAPI.Data;
using FindJobAPI.Model.Domain;
using FindJobAPI.Model.DTO;
using FindJobAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FindJobAPI.Repository.Queries
{
    public class Location_Repository : ILocation_Repository
    {
        private readonly AppDbContext _appDbContext;

        public Location_Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<LocationDTO>> GetAll()
        {
            var AllLocation = _appDbContext.Location.AsQueryable();
            var ListLocation = await AllLocation.Select(location => new LocationDTO
            {
                location_id = location.location_id,
                location = location.location_name
            }).ToListAsync();
            return ListLocation.OrderBy(l=>l.location).ToList();
        }

        public async Task<LocationNoId> CreateLocation(LocationNoId locationNoId)
        {
            var existingLocation = await _appDbContext.Location.FirstOrDefaultAsync(i => i.location_name == locationNoId.location);
            if (existingLocation == null)
            {
                var LocationDomain = new location
                {
                    location_name = locationNoId.location
                };
                await _appDbContext.Location.AddAsync(LocationDomain);
                await _appDbContext.SaveChangesAsync();
                return locationNoId;
            }
            return null!;
        }

        public async Task<LocationNoId> UpdateLocation(int id, LocationNoId locationNoId)
        {
            var LocationDomain = _appDbContext.Location.FirstOrDefault(i => i.location_id == id);
            if (LocationDomain == null)
                return null!;
            LocationDomain.location_name = locationNoId.location;
            await _appDbContext.SaveChangesAsync();
            return locationNoId;
        }

        public async Task<location> DeleteLocation(int id)
        {
            var LocationDomain = _appDbContext.Location.SingleOrDefault(i => i.location_id == id);
            if (LocationDomain != null)
            {
                _appDbContext.Location.Remove(LocationDomain);
                await _appDbContext.SaveChangesAsync();
                return LocationDomain;
            }
            return null!;
        }
    }
}