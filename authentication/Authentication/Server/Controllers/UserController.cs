using Authentication.Shared.Models;
using LibraryApiTemplate.Controllers;
using LibraryDataBroker;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Server.Controllers
{
    [ApiController]    
    [Route("api/[controller]")]
    //[Authorize(Roles = "Administrator")]
    public class UserController : ListController<User>
    {
        public UserController(IListDataBroker repoList) : base(repoList)
        {
        }
    }

	public class UserGetController : GetController<User>
	{
		public UserGetController(IGetDataBroker repo) : base(repo)
		{
		}
	}
}
