namespace modeling.Utils
{
    using System.Net;
    using System.Net.Http.Headers;

    public class Header
    {
        public string Name { get; set; }

        public object Value { get; set; }

        public Header() { }

        public Header(HttpRequestHeader header, string value)
        {
            Name = header.ToString();
            Value = value;
        }

        public Header(HttpResponseHeader header, string value)
        {
            Name = header.ToString();
            Value = value;
        }
    }
}