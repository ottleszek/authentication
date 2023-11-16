﻿using Authentication.Shared.Models;
using LibraryApiTemplate.Controllers;
using LibraryDataBrokerProject;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Roles = "admin")]
    public class UserRoleController : ListController<UserRole>
    {
        public UserRoleController(IListDataBroker repoList) : base(repoList)
        {
        }
    }
}
