using EstoqFy.Core.Contracts.Services;
using EstoqFy.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EstoqFy.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        public UserController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }
        [HttpPost("api/user")]
        public async Task<IActionResult> CreateAsync([FromBody] User user)
        {
            try
            {
                if (user is null) return BadRequest();
                user.Id = new Guid();
                var exists = await _userService.ExistsAsync(user.Email, user.Cnpj);
                if (exists) return Ok();

                user.Password = _authService.CreateHash(user.Password);

                await _userService.CreateAsync(user);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("api/user")]
        public async Task<IActionResult> UpdateAsync([FromBody] User user)
        {
            try
            {
                if (user is null) return BadRequest();

                var exists = await _userService.ExistsAsync(user.Email, user.Cnpj);
                if (!exists) return NotFound();

                await _userService.UpdateAsync(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("api/user")]
        public async Task<IActionResult> DeleteAsync([FromBody] User user)
        {
            try
            {
                if (user is null) return BadRequest();

                var exists = await _userService.ExistsAsync(user.Email, user.Cnpj);
                if (!exists) return Ok();

                await _userService.DeleteAsync(user);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("api/user")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);
                if (user == null) return NotFound();

                return Ok(user);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
