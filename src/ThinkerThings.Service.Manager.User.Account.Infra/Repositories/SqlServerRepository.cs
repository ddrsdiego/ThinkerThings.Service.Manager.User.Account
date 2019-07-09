using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using ThinkerThings.Service.Manager.User.Account.Infra.Options;

namespace ThinkerThings.Service.Manager.User.Account.Infra.Repositories
{
    public abstract class SqlServerRepository
    {
        private readonly ConnectionStringOption _connectionStringOptions;

        protected SqlServerRepository(ILogger logger, IOptions<ConnectionStringOption> connectionStringOptions)
        {
            Logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
            _connectionStringOptions = connectionStringOptions.Value ?? throw new System.ArgumentNullException(nameof(connectionStringOptions.Value));
        }

        protected ILogger Logger { get; }
        protected IDbConnection GetConnection() => new SqlConnection(_connectionStringOptions.ConnectionString);
    }
}