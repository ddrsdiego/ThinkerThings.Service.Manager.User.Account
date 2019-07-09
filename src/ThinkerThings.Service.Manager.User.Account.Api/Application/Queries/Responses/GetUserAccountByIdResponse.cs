namespace ThinkerThings.Service.Manager.User.Account.Api.Application.Queries.Responses
{
    public class GetUserAccountByIdResponse
    {
        public int UserAccountId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserCellPhoneNumber { get; set; }
        public string UserDocumentNumber { get; set; }
    }
}