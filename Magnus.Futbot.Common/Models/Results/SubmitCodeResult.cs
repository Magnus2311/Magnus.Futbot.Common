using Magnus.Futbot.Common.Models.Selenium.Profiles;
using MongoDB.Bson;

namespace Magnus.Futbot.Common.Models.Results
{
    public class SubmitCodeResult
    {
        public SubmitCodeResult(SubmitCodeDTO submitCodeDTO, ConfirmationCodeStatusType confirmationCodeStatusType)
        {
            Email = submitCodeDTO.Email;
            UserId = submitCodeDTO.UserId;
            ConfirmationCodeStatus = confirmationCodeStatusType;
        }

        public string Email { get; }
        public ConfirmationCodeStatusType ConfirmationCodeStatus { get; }
        public string UserId { get; }
    }
}
