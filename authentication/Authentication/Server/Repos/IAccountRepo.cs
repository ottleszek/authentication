﻿using Authentication.Server.Repos.Base;
using Authentication.Shared.Models;
using LibraryDatabase.Model;

namespace Authentication.Server.Repos
{
    public interface IAccountRepo : IIUserGetRepoBase
    {
        public User? GetUserBy(Guid userId);
        public Task<RepositoryResponse> SaveNewUser(User user);
    }
}
