using Project.Services;
using Project.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController
    {
        private readonly IRoomServ _roomService;
        public RoomController(IRoomServ roomService)
        {
            _roomService = roomService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRooms()
        {
            var rooms = await _roomService.GetAllAsync();
            return new OkObjectResult(rooms);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomById(int id)
        {
            var room = await _roomService.GetByIdAsync(id);
            if (room == null) return new NotFoundResult();
            return new OkObjectResult(room);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRoom([FromBody] RoomCreateDto dto)
        {
            var room = await _roomService.CreateAsync(dto);
            return new CreatedAtActionResult(nameof(GetRoomById), "Room", new { id = room.Id }, room);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom(int id, [FromBody] RoomUpdateDto dto)
        {
            try
            {
                await _roomService.UpdateAsync(id, dto);
                return new NoContentResult();
            }
            catch (KeyNotFoundException)
            {
                return new NotFoundResult();
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            try
            {
                await _roomService.DeleteAsync(id);
                return new NoContentResult();
            }
            catch (KeyNotFoundException)
            {
                return new NotFoundResult();
            }
        }
    }
}
