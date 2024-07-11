using HackclubArcadeAPIWrapper.Exceptions;
using HackclubArcadeAPIWrapper.HTTPRequest;
using HackclubArcadeAPIWrapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HackclubArcadeAPIWrapper
{
    public class ArcadeWrapper
    {
        private string? ArcadeAPIKey;
        private HTTPRequestSender RequestSender;


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArcadeHTTPException"></exception>
        /// <exception cref="ArcadeUnauthorizedException"></exception>
        public ArcadeWrapper(string arcadeAPIKey)
        {
            if(arcadeAPIKey == null) throw new ArgumentNullException(nameof(arcadeAPIKey));
            ArcadeAPIKey = arcadeAPIKey;
            RequestSender = new HTTPRequestSender(ArcadeAPIKey);

            _ = GetUserStats();
        }

        #region Arcade Requests

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="JSONBody"></param>
        /// <returns></returns>
        /// <exception cref="ArcadeHTTPException"></exception>
        /// <exception cref="ArcadeUnauthorizedException"></exception>
        private async Task<GenericArcadeResponse> GetArcadeResponseAsync(string url, HttpMethod method, string? JSONBody = null)
        {
            HTTPRequestResponse response = method == HttpMethod.Post ? await RequestSender.POSTAsync(url, JSONBody) : await RequestSender.GETAsync(url);

            if (!response.SendSuccess) throw new ArcadeHTTPException("Failed to get response from Arcade API");

            GenericArcadeResponse? arcadeResponse = GenericArcadeResponse.FromJSON(response.StringContent!);

            if (arcadeResponse == null) throw new ArcadeHTTPException("Failed to parse response from Arcade API");
            if(response.StatusCode == HttpStatusCode.NotFound && !arcadeResponse.OK && arcadeResponse.Json["error"]?.ToObject<string>() == "User not found") throw new ArcadeUnauthorizedException(ArcadeAPIKey);
            return arcadeResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="JSONBody"></param>
        /// <returns></returns>
        /// <exception cref="ArcadeHTTPException"></exception>
        /// <exception cref="ArcadeUnauthorizedException"></exception>
        private GenericArcadeResponse GetArcadeResponse(string url, HttpMethod method, string? JSONBody = null)
        {
            HTTPRequestResponse response = method == HttpMethod.Post ? RequestSender.POST(url, JSONBody) : RequestSender.GET(url);

            if (!response.SendSuccess) throw new ArcadeHTTPException("Failed to get response from Arcade API");

            GenericArcadeResponse? arcadeResponse = GenericArcadeResponse.FromJSON(response.StringContent!);

            if (arcadeResponse == null) throw new ArcadeHTTPException("Failed to parse response from Arcade API");
            if (response.StatusCode == HttpStatusCode.NotFound && !arcadeResponse.OK && arcadeResponse.Json["error"]?.ToObject<string>() == "User not found") throw new ArcadeUnauthorizedException(ArcadeAPIKey);
            return arcadeResponse;
        }

        #endregion

        #region Ping
        public bool Ping()
        {
            var res = RequestSender.GET(Paths.Ping);
            if (!res.SendSuccess) throw new ArcadeHTTPException("Failed to get response from Arcade API");

            return res.StatusCode == HttpStatusCode.OK && res.StringContent == "pong";
        }

        public async Task<bool> PingAsync()
        {
            var res = await RequestSender.GETAsync(Paths.Ping);
            if (!res.SendSuccess) throw new ArcadeHTTPException("Failed to get response from Arcade API");

            return res.StatusCode == HttpStatusCode.OK && res.StringContent == "pong";
        }
        #endregion

        #region Get User Stats

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArcadeHTTPException"></exception>
        /// <exception cref="ArcadeUnauthorizedException"></exception>
        public ArcadeUserStats GetUserStats()
        {
            var resp = GetArcadeResponse(Paths.Stats, HttpMethod.Get);

            return resp.GetData<ArcadeUserStats>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArcadeHTTPException"></exception>
        /// <exception cref="ArcadeUnauthorizedException"></exception>
        public async Task<ArcadeUserStats> GetUserStatsAsync()
        {
            var resp = await GetArcadeResponseAsync(Paths.Stats, HttpMethod.Get);

            return resp.GetData<ArcadeUserStats>();
        }

        #endregion

        #region Get Latest Session

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArcadeHTTPException"></exception>
        /// <exception cref="ArcadeUnauthorizedException"></exception>
        public ArcadeLatestSession GetLatestSession()
        {
            var resp = GetArcadeResponse(Paths.LatestSession, HttpMethod.Get);

            return resp.GetData<ArcadeLatestSession>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArcadeHTTPException"></exception>
        /// <exception cref="ArcadeUnauthorizedException"></exception>
        public async Task<ArcadeLatestSession> GetLatestSessionAsync()
        {
            var resp = await GetArcadeResponseAsync(Paths.LatestSession, HttpMethod.Get);

            return resp.GetData<ArcadeLatestSession>();
        }

        #endregion

        #region Get Goals
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArcadeHTTPException"></exception>
        /// <exception cref="ArcadeUnauthorizedException"></exception>
        public ArcadeGoal[] GetGoals()
        {
            var resp = GetArcadeResponse(Paths.Goals, HttpMethod.Get);

            return resp.GetData<List<ArcadeGoal>>().ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArcadeHTTPException"></exception>
        /// <exception cref="ArcadeUnauthorizedException"></exception>
        public async Task<ArcadeGoal[]> GetGoalsAsync()
        {
            var resp = await GetArcadeResponseAsync(Paths.Goals, HttpMethod.Get);

            return resp.GetData<List<ArcadeGoal>>().ToArray();
        }
        #endregion

        #region Get Session History

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArcadeHTTPException"></exception>
        /// <exception cref="ArcadeUnauthorizedException"></exception>
        public ArcadeHistorySession[] GetSessionHistory()
        {
            var resp = GetArcadeResponse(Paths.History, HttpMethod.Get);

            return resp.GetData<List<ArcadeHistorySession>>().ToArray();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArcadeHTTPException"></exception>
        /// <exception cref="ArcadeUnauthorizedException"></exception>
        public async Task<ArcadeHistorySession[]> GetSessionHistoryAsync()
        {
            var resp = await GetArcadeResponseAsync(Paths.History, HttpMethod.Get);

            return resp.GetData<List<ArcadeHistorySession>>().ToArray();
        }
        #endregion

    }
}
