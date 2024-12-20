﻿using Authentication.Shared.Models;

namespace Authentication.Server.Repos.Base
{
    public interface IIUserGetRepoBase
    {
        public Task<bool> IsUserExsist(string email);
        public Task<User?> GetUserBy(string email);
        public Task<Guid?> GetIdBy(string email);
    }
}
