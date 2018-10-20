using System.Net;
using System.Net.Http;
//using consumer.Base;

namespace consumer.Service
{
    public class ItemsServiceClient
    {
        //[Endpoint(PartialUrl = "api/issuebook-references/v1/contacts", Method = HttpMethod.Get)]
        public EndPoint GetContacts { get; set; }
    }
}