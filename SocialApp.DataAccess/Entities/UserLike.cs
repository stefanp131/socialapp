namespace SocialApp.DataAccess.Entities;

public class UserLike
{
    public int Id { get; set; }
    public AppUser SourceUser { get; set; }
    public int SourceUserId { get; set; }
    public AppUser TargetUser { get; set; }
    public int TargetUserId { get; set; }
}