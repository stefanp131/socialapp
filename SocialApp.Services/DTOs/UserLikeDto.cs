using SocialApp.DataAccess.Entities;

namespace Services.DTOs
{
    public class UserLikeDto
    {
        public int SourceUserId { get; set; }
        public int TargetUserId { get; set; }
    }
}
