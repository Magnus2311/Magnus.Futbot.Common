using MongoDB.Bson;

namespace Magnus.Futbot.Common.Models.Selenium.Profiles
{
    public class SubmitCodeDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public ObjectId UserId { get; set; }
    }
}
