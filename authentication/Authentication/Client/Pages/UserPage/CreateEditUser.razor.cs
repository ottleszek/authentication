using Microsoft.AspNetCore.Components;

namespace Authentication.Client.Pages.UserPage
{
    public partial class CreateEditUser
    {
        [Parameter]public Guid Id { get; set; }
    }
}
