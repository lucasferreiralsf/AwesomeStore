using System;
using System.Net;
using System.Threading.Tasks;
using AwesomeStore.Domain.DTOs;
using AwesomeStore.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeStore.Application.Controllers
{
    [Route("api/[controller]")]
    public class LoginController: ControllerBase
    {
        private ILoginService _service;
        public LoginController(ILoginService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<object> Login([FromBody] LoginDto loginDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            if(loginDto == null) return BadRequest();

            try
            {
                var result = await _service.FindByEmail(loginDto);
                if(result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (ArgumentException e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, e);
            }
        }
    }
}