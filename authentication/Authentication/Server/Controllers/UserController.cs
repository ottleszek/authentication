using Authentication.Server.Repos;
using Authentication.Shared.Models;
using LibraryApiTemplate.Controllers;
using LibraryDataBroker;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Roles = "Administrator")]
    public class UserController : IncludedController<User>
    {
        public UserController(IUserRepo repoIncluded, IListDataBroker repoList, IGetDataBroker repoGet) : base(repoIncluded, repoList, repoGet)
        {
        }
    }

    [ApiController]
    [Route("api/user")]
    //[Authorize(Roles = "Administrator")]
    public class UserCrudController : CrudController<User>
    {
        public UserCrudController(ICrudDataBroker repoCrud, IUpdateDataBroker repoUpdate) : base(repoCrud, repoUpdate)
        {
        }
    }
}
