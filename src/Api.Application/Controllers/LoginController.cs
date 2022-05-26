using System;
using System.Net;
using System.Threading.Tasks;
using Api.Domain.Dtos;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<object> Login([FromBody] LoginDto loginDTO, [FromServices] ILoginService service)
        {
            _logger.LogInformation("Action Post :: LoginController => " + DateTime.Now.ToLongTimeString());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (loginDTO == null)
            {
                return BadRequest();
            }

            try
            {
                var result = await service.FindByLogin(loginDTO);

                if (result != null)
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
                _logger.LogError("Action Post :: LoginController => " + DateTime.Now.ToLongTimeString() + " " + e.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
