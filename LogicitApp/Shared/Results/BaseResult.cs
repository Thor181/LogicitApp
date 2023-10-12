namespace LogicitApp.Shared.Results
{
    public class BaseResult
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}
