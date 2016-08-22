namespace BossrCoreAPI.Models.Requests
{
    public class PasswordRequest
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
