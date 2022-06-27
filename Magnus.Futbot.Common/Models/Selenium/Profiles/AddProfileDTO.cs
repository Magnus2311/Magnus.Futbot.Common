﻿using MongoDB.Bson;

namespace Magnus.Futbot.Common.Models.Selenium.Profiles
{
    public class AddProfileDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public ObjectId UserId { get; set; }

        public override int GetHashCode()
        {
            return HashCode.Combine(Email);
        }

    }
}
