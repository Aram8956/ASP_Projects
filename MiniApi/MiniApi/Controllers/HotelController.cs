using Project.DTO;
using Project.Services;
using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController
    {
        private readonly IHotelServ _hotelService;
        public HotelController(IHotelServ hotelService)
        {
            _hotelService = hotelService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllHotels()
        {
            var hotels = await _hotelService.GetAllAsync();
            return new OkObjectResult(hotels);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotelById(int id)
        {
            var hotel = await _hotelService.GetByIdAsync(id);
            if (hotel == null) return new NotFoundResult();
            return new OkObjectResult(hotel);
        }
        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] HotelCreateDto dto)
        {
            var hotel = await _hotelService.CreateAsync(dto);
            return new CreatedAtActionResult(nameof(GetHotelById), "Hotel", new { id = hotel.Id }, hotel);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHotel(int id, [FromBody] HotelUpdateDto dto)
        {
            try
            {
                await _hotelService.UpdateAsync(id, dto);
                return new NoContentResult();
            }
            catch (KeyNotFoundException)
            {
                return new NotFoundResult();
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            try
            {
                await _hotelService.DeleteAsync(id);
                return new NoContentResult();
            }
            catch (KeyNotFoundException)
            {
                return new NotFoundResult();
            }
        }
    }
}
