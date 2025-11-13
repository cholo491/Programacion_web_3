using Microsoft.AspNetCore.Mvc;

namespace Apis.Controllers
{
    public class UserController
    {
        [ApiController]
        [Route("Apis/[Controller]")
        // update, updateemail and delete methods
        [Authorize]
        public class UserController : ControllerBase
        {
            private readonly IUserService _userService;
            public UserController(IUserService userService)
            {
                _userService = userService;
            }
            [HttpPut("update")]
            public IActionResult UpdateUser([FromBody] UpdateUserRequest request)
            {
                var result = _userService.UpdateUser(request);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            [HttpPut("updateemail")]
            public IActionResult UpdateEmail([FromBody] UpdateEmailRequest request)
            {
                var result = _userService.UpdateEmail(request);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            [HttpDelete("delete")]
            public IActionResult DeleteUser([FromBody] DeleteUserRequest request)
            {
                var result = _userService.DeleteUser(request);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            
           
        }
}
}

