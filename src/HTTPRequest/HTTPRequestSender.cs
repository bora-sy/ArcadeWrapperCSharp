using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HackclubArcadeAPIWrapper.HTTPRequest
{


    public class HTTPRequestSender
    {
        private string? _authorization;

        public HTTPRequestSender(string? Authorization = null)
        {
            _authorization = Authorization;
        }


        public async Task<HTTPRequestResponse> GET(string url, bool useAuthorization = true)
        {
            HttpClient client = new HttpClient();

            try
            {
                HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Get, url);

                if (useAuthorization && _authorization != null) msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _authorization);

                HttpResponseMessage response = await client.SendAsync(msg);


                return new HTTPRequestResponse(true, response);
            }
            catch (Exception)
            {
                return new HTTPRequestResponse(false);
            }
            finally
            {
                if (client != null) client.Dispose();
            }
        }

        public async Task<HTTPRequestResponse> POST(string url, string? JSONBody = null, bool useAuthorization = true)
        {
            HttpClient client = new HttpClient();

            try
            {
                HttpContent? content = new StringContent(JSONBody ?? "{}", Encoding.UTF8, "application/json");


                if (useAuthorization && _authorization != null) content.Headers.Add("Authorization", $"Bearer {_authorization}");

                HttpResponseMessage response = await client.PostAsync(url, content);


                return new HTTPRequestResponse(true, response);
            }
            catch (Exception)
            {
                return new HTTPRequestResponse(false);
            }
            finally
            {
                if (client != null) client.Dispose();
            }
        }
    }
}