using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TasksManager.Domain.DTO;
using TasksManager.Domain.Interfaces.BL;

namespace TasksManager.API.Controllers
{
    public class AuthController
    {
        private readonly IAuthBL _authenticationBL;
        private readonly IUserBL _userBL;

        public AuthController(IAuthBL authBL, IUserBL userBL)
        {
            _authenticationBL = authBL;
            _userBL = userBL;
        }

        [AllowAnonymous]
        [HttpPost()]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginData)
        {
            throw new NotImplementedException();
        }
    }
}
