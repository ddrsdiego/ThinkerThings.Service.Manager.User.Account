namespace ThinkerThings.Service.Manager.User.Account.Api.Application.Commands.Responses
{
    public class RegisterNewUserAccountResponse
    {
        public int UserAccountId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserDocumentNumber { get; set; }
    }
}