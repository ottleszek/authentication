namespace LibraryCore.Errors
{ 
    public class ModelStateErrors
    {

        public ModelStateErrors()
        {            
        }

        public Dictionary<string, List<string>>? Errors { get; set; } = new Dictionary<string, List<string>>();        

        public bool IsNotNull => Errors is not null;
        public bool HasErrors => Errors is not null && Errors.Count > 0;

        public void ClearAndAdd(string error, string errorMessage)
        {
            if (error is null || errorMessage is null)
            {
                return;
            }

            Errors= new Dictionary<string, List<string>>();
            List<string> errors = new List<string>();
            errors.Add(errorMessage);

            Errors.Add(error, errors);
        }
    }
}
