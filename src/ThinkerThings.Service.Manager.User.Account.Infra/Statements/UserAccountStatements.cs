namespace ThinkerThings.Service.Manager.User.Account.Infra.Statements
{
    internal static class UserAccountStatements
    {
        public const string GetUserAccountByEmail = "SELECT * FROM USERACCOUNT WITH(NOLOCK) WHERE EMAIL = @userAccountEmail";

        public const string GetUserAccountByDocumentNumber = "SELECT * FROM USERACCOUNT WITH(NOLOCK) WHERE DOCUMENTNUMBER = @documentNumber";

        public const string GetUserAccountById = "SELECT * FROM USERACCOUNT WITH(NOLOCK) WHERE USERACCOUNTID = @userAccountId";

        public const string RegisterUserAccount = @"
            INSERT INTO UserAccount
            (
	            DocumentNumber
	            ,Name
	            ,Email
	            ,CellPhoneNumber
	            ,ParticularPhoneNumber
	            ,CreationDate
            )
            VALUES(
	            @documentNumber
	            ,@name
	            ,@email
	            ,@cellPhoneNumber
	            ,@particularPhoneNumber
	            ,@creationDate
            )";
    }
}