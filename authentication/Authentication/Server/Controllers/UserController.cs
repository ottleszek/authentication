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
		public UserController(IListDataBroker repoList, IGetDataBroker repoGet) : base(repoList, repoGet)
		{
		}
	}

    [ApiController]
    [Route("api/user")]
    //[Authorize(Roles = "Administrator")]
    public class UserUpdateController : UpdateController<User>
    {
        public UserUpdateController(IUpdateDataBroker repoUpdate) : base(repoUpdate)
        {
        }
    }
}
