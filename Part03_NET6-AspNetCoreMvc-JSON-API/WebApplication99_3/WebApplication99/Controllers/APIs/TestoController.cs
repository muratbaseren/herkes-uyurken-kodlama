using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication99.Helpers;
using WebApplication99.Models;

namespace WebApplication99.Controllers.APIs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class TestoController : ControllerBase
    {
        private readonly ITokenHelper _tokenHelper;

        public TestoController(ITokenHelper tokenHelper)
        {
            _tokenHelper = tokenHelper;
        }

        [HttpGet("get-data")]
        public IActionResult GetData()
        {
            return Ok(new { Name = "Murat", Surname = "Başeren" });
        }

        [HttpPost("add")]
        public IActionResult PostData([FromBody] PostDataApiModel model)
        {
            return Ok(model);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginViewModel model)
        {
            if (model.Username == "aaa" && model.Password == "123123")
            {
                string token = _tokenHelper.GenerateToken(model.Username, 0);

                return Ok(new { Error = false, Message = "Token created.", Data = token });
            }
            else
            {
                return BadRequest(new { Error = true, Message = "Incorrect identity." });
            }
        }
    }
}
