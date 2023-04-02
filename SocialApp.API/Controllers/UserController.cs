using Microsoft.AspNetCore.Mvc;
using Services.DTOs;
using Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialApp.API.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetAllUsers()
        {
            return await this.userService.GetAllUsersAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<UserDto>>> GetUserById(int id)
        {
            var user = await this.userService.GetUserByIdAsync(id);
            
            if(user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> CreateLike([FromBody] UserLikeDto userLikeDto)
        {
            await this.userService.CreateLikeAsync(userLikeDto.SourceUserId, userLikeDto.TargetUserId);

            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteLike([FromQuery] int sourceId,[FromQuery] int targetId)
        {
            await this.userService.DeleteLikeAsync(sourceId, targetId);

            return NoContent();
        }
    }
}