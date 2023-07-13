using System;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs;
using Services.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;
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
        public async Task<ActionResult<List<UserDto>>> GetAllUsers([FromQuery] string stringTerm)
        {
            if (stringTerm != null)
            {
                var users = await this.userService.GetUsersByStringTermAsync(stringTerm);
            
                return Ok(users);
            }
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
        
        [HttpPost("view")]
        public async Task<ActionResult> CreateView([FromBody] ViewDto viewDto)
        {
            await userService.SaveViewsAsync(viewDto.LoggedInUser, viewDto.ViewedProfileId, viewDto.UserName);

            return Ok();
        }
        
        [HttpDelete("view")]
        public async Task<ActionResult> ClearViews([FromQuery] int loggedInUser)
        {
            await userService.ClearViewsAsync(loggedInUser);

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