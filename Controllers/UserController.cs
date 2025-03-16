using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateAPI.Service.IServices; // Assuming your IUserService is in this namespace

namespace RealEstateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        // Constructor now only injects IUserService, since other services were ride-sharing specific.
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/User/GetUserDetails?userId=someId
        [HttpGet("[action]")]
        [Authorize]
        public async Task<IActionResult> GetUserDetails(string userId)
        {
            var response = await _userService.GetUserDetails(userId);
            return Ok(response);
        }

       
    }
}
