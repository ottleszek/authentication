﻿using Microsoft.EntityFrameworkCore;
using LibraryDatabase.Model;
using Authentication.Shared.Models;
using Authentication.Server.Repos.Base;

namespace Authentication.Server.Repos
{
    public class AccountRepo<TDbContext> : UserGetRepoBase<TDbContext>,  IAccountRepo where TDbContext : DbContext
    {
        private readonly DbSet<User>? _userSet;
        private readonly DbSet<UserRole>? _userRole;
        private readonly TDbContext _dbContext;

        public AccountRepo(IDbContextFactory<TDbContext> dbContextFactory)
            : base(dbContextFactory)
        {

            TDbContext dbContext = dbContextFactory.CreateDbContext();
            _dbContext = dbContext;

            try
            {
                _userSet = dbContext.GetDbSet<User>();
            }
            catch
            {
                LibraryLogging.LoggingBroker.LogError($"{nameof(User)} táblát nem lehet elérni.");
            }
            try
            {
                _userRole = dbContext.GetDbSet<UserRole>();
            }
            catch
            {
                LibraryLogging.LoggingBroker.LogError($"{nameof(UserRole)} táblát nem lehet elérni.");
            }

        }



        public User? GetUserBy(Guid userId)
        {
            if (_userSet is not null)
            {
                return _userSet.Where(user => user.Id==userId).FirstOrDefault();
            }
            return null;
        }

        public async Task<RepositoryResponse> SaveNewUser(User user)
        {
            RepositoryResponse response = new RepositoryResponse();

            try
            {
                if (_userSet is not null)
                {
                    _userSet.Add(user);
                    await _dbContext.SaveChangesAsync();             
                }
            }
            catch (Exception ex)
            {
                LibraryLogging.LoggingBroker.LogError($"{nameof(AccountRepo<TDbContext>)}\nSql utasítás nem hajtható végre.\n{ex.Message}");
                response.ClearAndAddError($"{nameof(AccountRepo<TDbContext>)}\nSQL lekérdezés nem hajtható végre!\n{ex.Message}");
            }
            return response;
        }
    }
}


