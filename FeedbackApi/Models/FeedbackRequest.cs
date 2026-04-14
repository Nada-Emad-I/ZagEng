namespace FeedbackAPI.Models
{
    public class FeedbackRequest
    {
        public string UserName { get; set; } = string.Empty;

        public int Rate { get; set; }

        public string Comment { get; set; } = string.Empty;
    }
}
