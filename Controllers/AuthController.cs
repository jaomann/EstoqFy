using EstoqFy.Core.Contracts.Services;
using EstoqFy.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EstoqFy.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }
        [HttpPost("api/auth/login")]
        public async Task<IActionResult> Login([FromBody]UserLogin user)
        {
            try
            {
                if (user == null) 
                    return BadRequest();

                var entity = await _userService.GetByEmailAsync(user.Email);

                if (entity != null && _authService.VerifyHash(user.Password, entity.Password))
                {
                    _userService.SetCookieId(entity.Id, Response);
                    return Ok();
                }
                else
                    return NotFound();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
