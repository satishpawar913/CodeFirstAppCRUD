namespace CodeFirstApproachCRUD.Models
{
    public class ErrorViewModel
    {
        internal string? Message;

        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string? StackTrace { get; internal set; }
    }
}
