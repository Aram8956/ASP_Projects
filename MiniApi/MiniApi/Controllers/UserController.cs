using Microsoft.AspNetCore.Mvc;
using Project.Services;
using Project.DTO;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController
    {
        private readonly IUserServ _userService;
        public UserController(IUserServ userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllAsync();
            return new OkObjectResult(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return new NotFoundResult();
            return new OkObjectResult(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto dto)
        {
            var user = await _userService.CreateAsync(dto);
            return new CreatedAtActionResult(nameof(GetUserById), "User", new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDto dto)
        {
            try
            {
                await _userService.UpdateAsync(id, dto);
                return new NoContentResult();
            }
            catch (KeyNotFoundException)
            {
                return new NotFoundResult();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _userService.DeleteAsync(id);
                return new NoContentResult();
            }
            catch (KeyNotFoundException)
            {
                return new NotFoundResult();
            }
        }
    }
}
