using Microsoft.AspNetCore.Mvc;
using Project.DTO;
using Project.Services;

namespace Project.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly IBookingServ _service;
    public BookingsController(IBookingServ service) { _service = service; }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var bookings = await _service.GetAllAsync();
        return Ok(bookings);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var b = await _service.GetByIdAsync(id);
        if (b == null) return NotFound();
        return Ok(b);
    }

    [HttpPost]
    public async Task<IActionResult> Book([FromBody] BookingCreateDto dto)
    {
        try
        {
            var booking = await _service.CreateBookingAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = booking.Id }, booking);
        }
        catch (Exception ex) { 
            return BadRequest(ex.Message); 
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Cancel(int id)
    {
        try { await _service.RemoveBookingAsync(id); return NoContent(); }
        catch (Exception ex) { return NotFound(ex.Message); }
    }
}
