using System.Net;

namespace provider.Models
{
    public class Message
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Text { get; set; }
    }
}