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
        private async Task<GenericArcadeResponse> GetArcadeResponseAsync(string url, HttpMethod method, string? JSONBody = null, bool asDataArray = false)
        {
            HTTPRequestResponse response = method == HttpMethod.Post ? await RequestSender.POSTAsync(url, JSONBody) : await RequestSender.GETAsync(url);

            if (!response.SendSuccess) throw new ArcadeHTTPException("Failed to get response from Arcade API");

            GenericArcadeResponse? arcadeResponse = GenericArcadeResponse.FromJSON(response.StringContent!, asDataArray);

            if (arcadeResponse == null) throw new ArcadeHTTPException("Failed to parse response from Arcade API");
            if(response.StatusCode == HttpStatusCode.NotFound && !arcadeResponse.OK && arcadeResponse.Error == "User not found") throw new ArcadeUnauthorizedException(ArcadeAPIKey);
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
        private GenericArcadeResponse GetArcadeResponse(string url, HttpMethod method, string? JSONBody = null, bool asDataArray = false)
        {
            HTTPRequestResponse response = method == HttpMethod.Post ? RequestSender.POST(url, JSONBody) : RequestSender.GET(url);

            if (!response.SendSuccess) throw new ArcadeHTTPException("Failed to get response from Arcade API");

            GenericArcadeResponse? arcadeResponse = GenericArcadeResponse.FromJSON(response.StringContent!, asDataArray);

            if (arcadeResponse == null) throw new ArcadeHTTPException("Failed to parse response from Arcade API");
            if (response.StatusCode == HttpStatusCode.NotFound && !arcadeResponse.OK && arcadeResponse.Error == "User not found") throw new ArcadeUnauthorizedException(ArcadeAPIKey);
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
            var resp = GetArcadeResponse(Paths.Goals, HttpMethod.Get, null, true);

            return resp.GetData<List<ArcadeGoal>>(true).ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArcadeHTTPException"></exception>
        /// <exception cref="ArcadeUnauthorizedException"></exception>
        public async Task<ArcadeGoal[]> GetGoalsAsync()
        {
            var resp = await GetArcadeResponseAsync(Paths.Goals, HttpMethod.Get, null, true);

            return resp.GetData<List<ArcadeGoal>>(true).ToArray();
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
            var resp = GetArcadeResponse(Paths.History, HttpMethod.Get, null, true);

            return resp.GetData<List<ArcadeHistorySession>>(true).ToArray();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArcadeHTTPException"></exception>
        /// <exception cref="ArcadeUnauthorizedException"></exception>
        public async Task<ArcadeHistorySession[]> GetSessionHistoryAsync()
        {
            var resp = await GetArcadeResponseAsync(Paths.History, HttpMethod.Get, null, true);

            return resp.GetData<List<ArcadeHistorySession>>(true).ToArray();
        }
        #endregion

        #region Start Session

        /// <summary>
        /// 
        /// </summary>
        /// <param name="work"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArcadeHTTPException"></exception>
        /// <exception cref="ArcadeUnauthorizedException"></exception>
        /// <exception cref="ArcadeSessionException"></exception>
        public ArcadeStartResult StartSession(string work)
        {
            if (work == null) throw new ArgumentNullException(nameof(work));

            var resp = GetArcadeResponse(Paths.SessionStart, HttpMethod.Post, $"{{\"work\":\"{work}\"}}");

            if(!resp.OK) throw new ArcadeSessionException(resp.Error!);

            return resp.GetData<ArcadeStartResult>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="work"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArcadeHTTPException"></exception>
        /// <exception cref="ArcadeUnauthorizedException"></exception>
        /// <exception cref="ArcadeSessionException"></exception>
        public async Task<ArcadeStartResult> StartSessionAsync(string work)
        {
            if (work == null) throw new ArgumentNullException(nameof(work));

            var resp = await GetArcadeResponseAsync(Paths.SessionStart, HttpMethod.Post, $"{{\"work\":\"{work}\"}}");

            if (!resp.OK) throw new ArcadeSessionException(resp.Error!);

            return resp.GetData<ArcadeStartResult>();
        }

        #endregion

        #region Pause Session

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArcadeHTTPException"></exception>
        /// <exception cref="ArcadeUnauthorizedException"></exception>
        /// <exception cref="ArcadeSessionException"></exception>
        public ArcadePauseResult PauseSession()
        {
            var resp = GetArcadeResponse(Paths.SessionPause, HttpMethod.Post);
            if (!resp.OK) throw new ArcadeSessionException(resp.Error!);

            return resp.GetData<ArcadePauseResult>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArcadeHTTPException"></exception>
        /// <exception cref="ArcadeUnauthorizedException"></exception>
        /// <exception cref="ArcadeSessionException"></exception>
        public async Task<ArcadePauseResult> PauseSessionAsync()
        {
            var resp = await GetArcadeResponseAsync(Paths.SessionPause, HttpMethod.Post);
            if (!resp.OK) throw new ArcadeSessionException(resp.Error!);

            return resp.GetData<ArcadePauseResult>();
        }

        #endregion

        #region Cancel Session

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArcadeHTTPException"></exception>
        /// <exception cref="ArcadeUnauthorizedException"></exception>
        public ArcadeCancelResult CancelSession()
        {
            var resp = GetArcadeResponse(Paths.SessionCancel, HttpMethod.Post);

            return resp.GetData<ArcadeCancelResult>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArcadeHTTPException"></exception>
        /// <exception cref="ArcadeUnauthorizedException"></exception>
        public async Task<ArcadeCancelResult> CancelSessionAsync()
        {

            var resp = await GetArcadeResponseAsync(Paths.SessionCancel, HttpMethod.Post);

            return resp.GetData<ArcadeCancelResult>();
        }

        #endregion

    }
}
