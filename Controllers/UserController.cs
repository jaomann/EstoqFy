using EstoqFy.Core.Contracts.Services;
using EstoqFy.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EstoqFy.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("api/user")]
        public async Task<IActionResult> CreateAsync([FromBody] User user)
        {
            if (user is null) return BadRequest();

            var exists = await _userService.ExistsAsync(user.Email, user.Cnpj);
            if (exists) return Ok();

            await _userService.CreateAsync(user);
            return Ok();
        }
        [HttpPut("api/user")]
        public async Task<IActionResult> UpdateAsync([FromBody] User user)
        {
            if (user is null) return BadRequest();

            var exists = await _userService.ExistsAsync(user.Email, user.Cnpj);
            if (!exists) return NotFound();

            await _userService.UpdateAsync(user);
            return Ok();
        }
        [HttpDelete("api/user")]
        public async Task<IActionResult> DeleteAsync([FromBody] User user)
        {
            if (user is null) return BadRequest();

            var exists = await _userService.ExistsAsync(user.Email, user.Cnpj);
            if (!exists) return Ok();

            await _userService.DeleteAsync(user);
            return Ok();
        }
        [HttpGet("api/user/{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();

            return Ok(user);
        }
        [HttpGet("api/user")]
        public async Task<IActionResult> GetAllAsync()
        {
            var user = await _userService.GetAllAsync();
            if (user == null) return NotFound();

            return Ok(user);
        }
    }
}
