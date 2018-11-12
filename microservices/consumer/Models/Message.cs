using System.Net;

namespace consumer.Models
{
    public class Message
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Text { get; set; }
    }
}