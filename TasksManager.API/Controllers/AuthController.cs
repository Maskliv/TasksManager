using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TasksManager.Domain.DTO;
using TasksManager.Domain.Enum;
using TasksManager.Domain.Exceptions;
using TasksManager.Domain.Interfaces.BL;

namespace TasksManager.API.Controllers
{
    public class AuthController: BaseController
    {
        private readonly IAuthBL _authBL;
        private readonly IUserBL _userBL;

        public AuthController(IAuthBL authBL, IUserBL userBL)
        {
            _authBL = authBL;
            _userBL = userBL;
        }

        [AllowAnonymous]
        [HttpPost()]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginData)
        {
            var token = await _authBL.Login(loginData);
            if (token == null)
                return await GetResponseAsync<string?>(HttpStatusCode.BadRequest, "Usuario y/o contraseña incorrecto", null);
            return await GetResponseAsync(HttpStatusCode.OK, "Sesión iniciada correctamente", token);
        }

        [AllowAnonymous]
        [HttpPost()]
        [Route("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] UserDto newUser)
        {
            try 
            {
                var userCreatedDto = await _authBL.SignUp(newUser);
                return await GetResponseAsync(HttpStatusCode.OK, "Usuario creado correctamente", userCreatedDto);

            }
            catch(ClientException ex){
                return await GetResponseAsync<string?>(HttpStatusCode.BadRequest, ex.Message, null, ex.Message);
            }            
            
            
        }
    }
}
