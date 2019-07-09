using System;
using System.Threading.Tasks;
using System.Transactions;
using ThinkerThings.Service.Manager.User.Account.Domain.AggregateModels.UserAccountModel;
using ThinkerThings.Service.Manager.User.Account.Domain.SeedWorks;

namespace ThinkerThings.Service.Manager.User.Account.Api.Application.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUserAccountRepository _userAccountRepository;

        public UserAccountService(IUserAccountRepository userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
        }

        public async Task<Result<SituationRegistrationAccount>> CheckAccountAlreadyRegistered(string documentNumber, string userEmail)
        {
            if (documentNumber == null)
                return Result<SituationRegistrationAccount>.Fail(nameof(documentNumber));

            if (userEmail == null)
                return Result<SituationRegistrationAccount>.Fail(nameof(userEmail));

            try
            {
                var usuarioCpfTask = _userAccountRepository.GetUserAccountByDocumentNumber(documentNumber);
                var usuarioEmailTask = _userAccountRepository.GetUserAccountByEmail(userEmail);

                await Task.WhenAll(usuarioCpfTask, usuarioEmailTask).ConfigureAwait(false);

                var usuarioCpf = await usuarioCpfTask.ConfigureAwait(false);
                var usuarioEmail = await usuarioEmailTask.ConfigureAwait(false);

                if (usuarioCpf != null && usuarioEmail != null)
                    return Result<SituationRegistrationAccount>.Ok(SituationRegistrationAccount.AccountAlreadyRegistered);

                if (usuarioEmail != null)
                    return Result<SituationRegistrationAccount>.Ok(SituationRegistrationAccount.EmailAlreadyRegistered);

                if (usuarioCpf != null)
                    return Result<SituationRegistrationAccount>.Ok(SituationRegistrationAccount.DocumentNumberAlreadyRegistered);

                return Result<SituationRegistrationAccount>.Ok(SituationRegistrationAccount.AccountNotRegistered);
            }
            catch (Exception ex)
            {
                return Result<SituationRegistrationAccount>.Fail(ex.ToString());
            }
        }

        public async Task<Result<UserAccount>> GetUserAccountById(int userAccountId)
        {
            if (userAccountId <= 0)
                return Result<UserAccount>.Fail(nameof(userAccountId));

            try
            {
                var userAccount = await _userAccountRepository.GetUserAccountById(userAccountId).ConfigureAwait(false);
                if (userAccount == null)
                    return Result<UserAccount>.Ok(UserAccount.Default());

                return Result<UserAccount>.Ok(userAccount);
            }
            catch (Exception ex)
            {
                return Result<UserAccount>.Fail(ex.ToString());
            }
        }

        public async Task<Result<UserAccount>> RegisterUserAccount(UserAccount userAccount)
        {
            if (userAccount == null)
                return Result<UserAccount>.Fail(nameof(userAccount));

            try
            {
                using (var ts = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
                {
                    await _userAccountRepository.RegisterUserAccount(userAccount).ConfigureAwait(false);

                    userAccount = await _userAccountRepository.GetUserAccountByEmail(userAccount.Email).ConfigureAwait(false);
                    if (userAccount == null)
                        return Result<UserAccount>.Fail("");

                    ts.Complete();
                }

                return Result<UserAccount>.Ok(userAccount);
            }
            catch (Exception ex)
            {
                return Result<UserAccount>.Fail(ex.ToString());
            }
        }
    }
}