using System.Collections.Generic;

namespace Services.DTOs;

public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string ProfilePicture { get; set; }
    public string Description { get; set; }
    public List<UserLikeDto> LikedByUsers { get; set; }
    public string Views { get; set; }
}