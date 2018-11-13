using System.Net;

using Newtonsoft.Json.Linq;

namespace consumer.Models
{
    public class Message
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Text { get; set; }
        public JObject ExternalError { get; set; }
    }
}