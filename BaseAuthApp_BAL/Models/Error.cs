namespace BaseAuthApp_BAL.Models
{
    public class Error
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public List<string> Details { get; set; }

       public Error(string code, string message, List<string> details = null)
        {
            Code = code;
            Message = message;
            Details = details ?? new List<string>();
        }
    }
}
