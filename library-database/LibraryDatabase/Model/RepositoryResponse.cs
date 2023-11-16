using LibraryCore.Errors;

namespace LibaryDatabase.Model
{
    public class RepositoryResponse : ErrorStore
    {
        public bool InstructionSuccessfullyExecuted => ! HasError;

        public RepositoryResponse() : base()
        {
        }
    }
}
