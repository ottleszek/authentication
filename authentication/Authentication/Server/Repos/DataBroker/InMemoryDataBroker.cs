using Authentication.Server.Context;
using LibraryApiTemplate.Repos;
using LibraryDataBrokerProject;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Server.Repos.DataBroker
{
    public class AccountInMemoryRepoDataBroker : AccountRepo<AuthenticationInMemoryContext>, IAccountRepo
    {
        public AccountInMemoryRepoDataBroker(IDbContextFactory<AuthenticationInMemoryContext> dbContextFactory) : base(dbContextFactory)
        {
        }
    }

    public class UserRefreshTokenRepoDataBroker : UserRefreshTokenRepo<AuthenticationInMemoryContext>, IUserRefreshTokenRepo
    {
        public UserRefreshTokenRepoDataBroker(IDbContextFactory<AuthenticationInMemoryContext> dbContextFactory) : base(dbContextFactory)
        {
        }
    }

    public class UserRoleRepoInMemoryDataBroker : UserRoleRepo<AuthenticationInMemoryContext>, IUserRoleRepo
    {
        public UserRoleRepoInMemoryDataBroker(IDbContextFactory<AuthenticationInMemoryContext> dbContextFactory) : base(dbContextFactory)
        {
        }
    }

    public class UserIdentificationRepoInMemoryDataBroker : UserIdentificationRepo<AuthenticationInMemoryContext>, IUserIdentificationRepo
    {
        public UserIdentificationRepoInMemoryDataBroker(IDbContextFactory<AuthenticationInMemoryContext> dbContextFactory) : base(dbContextFactory)
        {
        }
    }

    public class ListInMemoryDataBroker : RepoList<AuthenticationInMemoryContext>, IListDataBroker
    {
        public ListInMemoryDataBroker(IDbContextFactory<AuthenticationInMemoryContext> dbContextFactory) : base(dbContextFactory)
        {
        }
    }


}
