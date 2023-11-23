using Microsoft.AspNetCore.Components;

namespace LibraryBlazorClient.Base
{
    public class TableGridViewSelector : ComponentBase
    {
        protected bool _isTableView = true;
        protected bool _isTableViewDisabled => _isTableView;
        protected bool _isGridViewDisabled => !_isTableView;
       
        protected void SetTableView()
        {
            _isTableView = true;
        }

        protected void SetGridView()
        {
            _isTableView = false;
        }
    }
}
