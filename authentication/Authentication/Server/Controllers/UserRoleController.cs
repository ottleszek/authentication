using Authentication.Shared.Models;
using LibraryApiTemplate.Controllers;
using LibraryDataBroker;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Roles = "admin")]
    public class UserRoleController : ListController<UserRole>
    {
        public UserRoleController(IListDataBroker repoList, IGetDataBroker repoGet) : base(repoList,repoGet)
        {
        }
    }

    [ApiController]
    [Route("api/userrole")]
    //[Authorize(Roles = "Administrator")]
    public class UserRoleUpdateController : UpdateController<User>
    {
        public UserRoleUpdateController(IUpdateDataBroker repoUpdate) : base(repoUpdate)
        {
        }
    }

    [ApiController]
    [Route("api/user")]
    //[Authorize(Roles = "Administrator")]
    public class UserRoleCrudController : CrudController<User>
    {
        public UserRoleCrudController(ICrudDataBroker repoCrud, IUpdateDataBroker repoUpdate) : base(repoCrud, repoUpdate)
        {
        }
    }
}
