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
        public UserController(IIncludedDataBroker repoIncluded, IListDataBroker repoList, IGetDataBroker repoGet) : base(repoIncluded, repoList, repoGet)
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
