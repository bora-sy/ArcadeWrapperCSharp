using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HackClub.Arcade.HTTPRequest
{


    internal class HTTPRequestSender
    {
        private string? _authorization;

        public HTTPRequestSender(string? Authorization = null)
        {
            _authorization = Authorization;
        }


        public async Task<HTTPRequestResponse> SendAsync(string url, HttpMethod method, string? JSONBody = null, bool useAuthorization = true)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage msg = new HttpRequestMessage(method, url);

            try
            {
                if (JSONBody != null) msg.Content = new StringContent(JSONBody, Encoding.UTF8, "application/json");

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
                if (msg != null) msg.Dispose();
            }
        }

        public HTTPRequestResponse Send(string url, HttpMethod method, string? JSONBody = null, bool useAuthorization = true)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage msg = new HttpRequestMessage(method, url);

            try
            {
                if (JSONBody != null) msg.Content = new StringContent(JSONBody, Encoding.UTF8, "application/json");

                if (useAuthorization && _authorization != null) msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _authorization);

                HttpResponseMessage response = client.Send(msg);


                return new HTTPRequestResponse(true, response);
            }
            catch (Exception)
            {
                return new HTTPRequestResponse(false);
            }
            finally
            {
                if (client != null) client.Dispose();
                if (msg != null) msg.Dispose();
            }
        }

        public async Task<HTTPRequestResponse> GETAsync(string url, bool useAuthorization = true)
        {
            return await SendAsync(url, HttpMethod.Get, null, useAuthorization);
        }

        public HTTPRequestResponse GET(string url, bool useAuthorization = true)
        {
            return Send(url, HttpMethod.Get, null, useAuthorization);
        }

        public async Task<HTTPRequestResponse> POSTAsync(string url, string? JSONBody = null, bool useAuthorization = true)
        {
            return await SendAsync(url, HttpMethod.Post, JSONBody, useAuthorization);
        }

        public HTTPRequestResponse POST(string url, string? JSONBody = null, bool useAuthorization = true)
        {
            return Send(url, HttpMethod.Post, JSONBody, useAuthorization);
        }
    }
}