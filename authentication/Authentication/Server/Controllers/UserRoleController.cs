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
    public class UserRoleUpdateController : UpdateController<UserRole>
    {
        public UserRoleUpdateController(IUpdateDataBroker repoUpdate) : base(repoUpdate)
        {
        }
    }

    [ApiController]
    [Route("api/userrole")]
    //[Authorize(Roles = "Administrator")]
    public class UserRoleCrudController : CrudController<UserRole>
    {
        public UserRoleCrudController(ICrudDataBroker repoCrud, IUpdateDataBroker repoUpdate) : base(repoCrud, repoUpdate)
        {
        }
    }
}
