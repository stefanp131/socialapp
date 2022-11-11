using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace SocialApp.DataAccess.Entities;

public class AppRole : IdentityRole<int>
{
    public ICollection<AppUserRole> UserRoles { get; set; }
}