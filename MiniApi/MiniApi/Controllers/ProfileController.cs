using Project.DTO;
using Project.Services;
using Microsoft.AspNetCore.Mvc;


namespace Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController
    {
        private readonly IProfileServ _profileService;
        public ProfileController(IProfileServ profileService)
        {
            _profileService = profileService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProfiles()
        {
            var profiles = await _profileService.GetAllAsync();
            return new OkObjectResult(profiles);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfileById(int id)
        {
            var profile = await _profileService.GetByIdAsync(id);
            if (profile == null) return new NotFoundResult();
            return new OkObjectResult(profile);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProfile([FromBody] ProfileCreateDto dto)
        {
            var profile = await _profileService.CreateAsync(dto);
            return new CreatedAtActionResult(nameof(GetProfileById), "Profile", new { id = profile.Id }, profile);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfile(int id, [FromBody] ProfileUpdateDto dto)
        {
            try
            {
                await _profileService.UpdateAsync(id, dto);
                return new NoContentResult();
            }
            catch (KeyNotFoundException)
            {
                return new NotFoundResult();
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile(int id)
        {
            try
            {
                await _profileService.DeleteAsync(id);
                return new NoContentResult();
            }
            catch (KeyNotFoundException)
            {
                return new NotFoundResult();
            }
        }

    }
}
