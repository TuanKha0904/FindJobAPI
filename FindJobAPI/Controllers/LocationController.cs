using FindJobAPI.Data;
using FindJobAPI.Model.DTO;
using FindJobAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FindJobAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LocationController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        private readonly ILocation_Repository location_repository;

        public LocationController(AppDbContext appDbContext, ILocation_Repository location_repository)
        {
            this.appDbContext = appDbContext;
            this.location_repository = location_repository;
        }

        [HttpGet("Get-all")]
        public async Task<IActionResult> GetAll()
        {
            var ListLocation = await location_repository.GetAll();
            if (ListLocation == null)
                return BadRequest();
            return Ok(ListLocation);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateLocation(LocationNoId locationNoId)
        {
            var LocationDomain = await location_repository.CreateLocation(locationNoId);
            if (LocationDomain == null)
                return BadRequest("location đã tồn tại");
            return Ok(LocationDomain);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateLocation(int id, LocationNoId locationNoId)
        {
            var LocationDomain = await location_repository.UpdateLocation(id, locationNoId);
            if (LocationDomain == null)
                return BadRequest($"Không tìm thấy location có id: {id}");
            return Ok(LocationDomain);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var locationDomain = await location_repository.DeleteLocation(id);
            if (locationDomain == null)
                return BadRequest($"Không tìm thấy location có id: {id}");
            return Ok(locationDomain);
        }
    }
}