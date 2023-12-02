using LibraryCore.Errors;

namespace LibraryDatabase.Model
{
    public class RepositoryResponse : ErrorStore
    {
        public bool InstructionSuccessfullyExecuted => ! HasError;

        public RepositoryResponse() : base()
        {
        }
    }
}
