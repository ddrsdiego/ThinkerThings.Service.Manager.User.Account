using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThinkerThings.Service.Manager.User.Account.Domain.AggregateModels.UserAccountModel;
using ThinkerThings.Service.Manager.User.Account.Infra.Options;
using ThinkerThings.Service.Manager.User.Account.Infra.Statements;

namespace ThinkerThings.Service.Manager.User.Account.Infra.Repositories
{
    public class UserAccountSqlServerRepository : SqlServerRepository, IUserAccountRepository
    {
        public UserAccountSqlServerRepository(ILoggerFactory loggerFactory, IOptions<ConnectionStringOption> connectionStringOptions)
            : base(loggerFactory.CreateLogger<UserAccountSqlServerRepository>(), connectionStringOptions)
        {
        }

        public async Task<UserAccount> GetUserAccountByDocumentNumber(string documentNumber)
        {
            try
            {
                return await GetUserAccount(UserAccountStatements.GetUserAccountByDocumentNumber, new { documentNumber }).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                var exception = new Exception($"Falha ao obter usuário pelo documento: {documentNumber}. Erro: {ex.Message}", ex);

                Logger.LogError(exception, ex.ToString());
                throw exception;
            }
        }

        public async Task<UserAccount> GetUserAccountByEmail(string userAccountEmail)
        {
            try
            {
                return await GetUserAccount(UserAccountStatements.GetUserAccountByEmail, new { userAccountEmail }).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<UserAccount> GetUserAccountById(int userAccountId)
        {
            try
            {
                return await GetUserAccount(UserAccountStatements.GetUserAccountById, new { userAccountId }).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task RegisterUserAccount(UserAccount userAccount)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    await conn.ExecuteAsync(UserAccountStatements.RegisterUserAccount,
                        new
                        {
                            documentNumber = userAccount.DocumentNumber,
                            name = userAccount.Name,
                            email = userAccount.Email,
                            cellPhoneNumber = userAccount.CellPhoneNumber,
                            particularPhoneNumber = userAccount.ParticularPhoneNumber,
                            creationDate = userAccount.CreationDate
                        }).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<UserAccount> GetUserAccount(string sqlCommand, object param = null)
            => await ExecuteGetUserAccount(sqlCommand, param).ConfigureAwait(false);

        private async Task<UserAccount> ExecuteGetUserAccount(string sqlCommand, object param)
        {
            using (var conn = GetConnection())
            {
                return await conn.QuerySingleOrDefaultAsync<UserAccount>(sqlCommand, param).ConfigureAwait(false);
            }
        }

        public async Task<IEnumerable<UserAccount>> GetUserAccounts()
        {
            try
            {
                using (var conn = GetConnection())
                {
                    return await conn.QueryAsync<UserAccount>("SELECT * FROM USERACCOUNT WITH(NOLOCK)").ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                var exception = new Exception($"Falha ao obter usuários. Erro: {ex.Message}", ex);

                Logger.LogError(exception, ex.ToString());
                throw exception;
            }
        }

        public async Task DeleteUserAccount(int userAccount)
        {
            const string DELETE_COMMAND = "DELETE FROM USERACCOUNT WHERE USERACCOUNTID = @userAccount";

            using (var conn = GetConnection())
                await conn.ExecuteAsync(DELETE_COMMAND, new { userAccount }).ConfigureAwait(false);
        }
    }
}