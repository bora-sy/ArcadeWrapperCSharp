using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HackclubArcadeAPIWrapper.HTTPRequest
{
    public class HTTPRequestResponse
    {
        public HttpResponseMessage? RawResponse { get; private set; }


        public bool SendSuccess { get; private set; }
        public HttpStatusCode? StatusCode => RawResponse?.StatusCode;
        public string? StringContent => RawResponse?.Content.ReadAsStringAsync().Result;


        public HTTPRequestResponse(bool SendSuccess, HttpResponseMessage? resp = null)
        {
            this.SendSuccess = SendSuccess;
            RawResponse = resp;
        }

        ~HTTPRequestResponse()
        {
            if (RawResponse != null) RawResponse.Dispose();
        }
    }
}
