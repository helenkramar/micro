using System;
using System.Net;
using System.Net.Http;

namespace consumer.Base
{
    public interface IHttpResponse
    {
        HttpStatusCode StatusCode { get; }

        string StatusDescription { get; }

        string CharacterSet { get; }

        string ContentEncoding { get; }

        long ContentLength { get; }

        string ContentType { get; }

        HttpMethod Method { get; }

        Version ProtocolVersion { get; }

        Uri ResponseUri { get; }

        string Server { get; }

        string ErrorMessage { get; }

        Exception ErrorException { get; }

        string Content { get; }

        byte[] RawBytes { get; }
    }
}