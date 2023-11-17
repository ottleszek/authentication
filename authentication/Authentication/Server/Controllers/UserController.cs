using Authentication.Shared.Models;
using LibraryApiTemplate.Controllers;
using LibraryDataBrokerProject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Server.Controllers
{
    [ApiController]    
    [Route("api/[controller]")]
    [Authorize(Roles = "admin")]
    public class UserController : ListController<User>
    {
        public UserController(IListDataBroker repoList) : base(repoList)
        {
        }
    }
}
