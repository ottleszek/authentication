using LibraryCore.Errors;

namespace LibraryDatabase.Model
{
    public class ServiceResponse : ErrorStore
    {
        public bool InstructionSuccessfullyExecuted => !HasError;

        public ServiceResponse() : base()
        {
        }
    }
}
