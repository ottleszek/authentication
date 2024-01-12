using LibraryCore.Errors;

namespace LibraryCore.Responses
{
    public class ControllerResponse : ErrorStore
    {
        public bool IsSuccess => !HasError;

        public ControllerResponse() : base() { }

        public ControllerResponse(string Error)
        {
            ClearAndAddError(Error);
        }
    }
}
