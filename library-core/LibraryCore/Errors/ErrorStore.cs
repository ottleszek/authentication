namespace LibraryCore.Errors
{
    public class ErrorStore
    {
        public ErrorStore()
        {
            Message = string.Empty;
        }

        public string Message { get; set; } = string.Empty;
        public bool HasError => !string.IsNullOrEmpty(Message);

        public void ClearErrorStore()
        { 
            Message = string.Empty; 
        }

        public void ClearAndAddError(string error)
        {
            Message = error;
        }

        public void AppendNewError(string error)
        {
            Message = $"{Message}\n{error}";
        }
    }
}
