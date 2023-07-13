using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace SocialApp.DataAccess.Entities;

public class AppUser : IdentityUser<int>
{
    public string Description { get; set; }
    public string ProfilePicture { get; set; }
    public ICollection<AppUserRole> UserRoles { get; set; }
    public ICollection<UserLike> LikedByUsers { get; set; }
    public ICollection<UserLike> LikedUsers { get; set; }
    public string Views { get; set; }
}