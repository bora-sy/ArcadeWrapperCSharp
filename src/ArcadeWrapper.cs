using HackclubArcadeAPIWrapper.Exceptions;
using HackclubArcadeAPIWrapper.HTTPRequest;
using HackclubArcadeAPIWrapper.Models;
using System;
using System.Net;
using System.Threading.Tasks;

namespace HackclubArcadeAPIWrapper
{
    /// <summary>
    /// Provides methods to interact with the Hackclub Arcade API for managing user sessions, statistics, goals, and history.
    /// </summary>
    public class ArcadeWrapper
    {
        private string? ArcadeAPIKey;
        private HTTPRequestSender RequestSender;

        /// <summary>
        /// Initializes a new instance of the ArcadeWrapper class with the specified Arcade API key.
        /// </summary>
        /// <param name="arcadeAPIKey">The API key for accessing the Arcade API.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="arcadeAPIKey"/> is null.</exception>
        /// <exception cref="ArcadeHTTPException">Thrown when there is an HTTP-related error while communicating with the Arcade API.</exception>
        /// <exception cref="ArcadeUnauthorizedException">Thrown when authentication with the Arcade API fails.</exception>
        public ArcadeWrapper(string arcadeAPIKey)
        {
            if (arcadeAPIKey == null) throw new ArgumentNullException(nameof(arcadeAPIKey));
            ArcadeAPIKey = arcadeAPIKey;
            RequestSender = new HTTPRequestSender(ArcadeAPIKey);

            _ = GetUserStats(); // Pre-fetch user stats upon initialization
        }

        #region Arcade Requests

        /// <summary>
        /// Sends an asynchronous request to the Arcade API and returns the response.
        /// </summary>
        /// <param name="url">The URL for the API endpoint.</param>
        /// <param name="method">The HTTP method (GET or POST).</param>
        /// <param name="JSONBody">Optional JSON body for POST requests.</param>
        /// <param name="asDataArray">Specifies if the response data should be parsed as an array.</param>
        /// <returns>The response from the Arcade API.</returns>
        /// <exception cref="ArcadeHTTPException">Thrown when there is an HTTP-related error while communicating with the Arcade API.</exception>
        /// <exception cref="ArcadeUnauthorizedException">Thrown when authentication with the Arcade API fails.</exception>
        /// <exception cref="ArcadeRateLimitException">Thrown when the rate limit for API requests is exceeded.</exception>
        private async Task<GenericArcadeResponse> GetArcadeResponseAsync(string url, HttpMethod method, string? JSONBody = null, bool asDataArray = false)
        {
            HTTPRequestResponse response = method == HttpMethod.Post ? await RequestSender.POSTAsync(url, JSONBody) : await RequestSender.GETAsync(url);

            if (!response.SendSuccess) throw new ArcadeHTTPException("Failed to get response from Arcade API");

            GenericArcadeResponse? arcadeResponse = GenericArcadeResponse.FromJSON(response.StringContent!, asDataArray);

            if (arcadeResponse == null) throw new ArcadeHTTPException("Failed to parse response from Arcade API");
            if (!arcadeResponse.OK)
            {
                if ((response.StatusCode == HttpStatusCode.NotFound && arcadeResponse.Error == "User not found") ||
                    (response.StatusCode == HttpStatusCode.Unauthorized))
                {
                    throw new ArcadeUnauthorizedException(ArcadeAPIKey);
                }

                if (arcadeResponse.Error!.ToLower().Contains("rate limit")) throw new ArcadeRateLimitException(arcadeResponse.Error);
            }

            return arcadeResponse;
        }

        /// <summary>
        /// Sends a synchronous request to the Arcade API and returns the response.
        /// </summary>
        /// <param name="url">The URL for the API endpoint.</param>
        /// <param name="method">The HTTP method (GET or POST).</param>
        /// <param name="JSONBody">Optional JSON body for POST requests.</param>
        /// <param name="asDataArray">Specifies if the response data should be parsed as an array.</param>
        /// <returns>The response from the Arcade API.</returns>
        /// <exception cref="ArcadeHTTPException">Thrown when there is an HTTP-related error while communicating with the Arcade API.</exception>
        /// <exception cref="ArcadeUnauthorizedException">Thrown when authentication with the Arcade API fails.</exception>
        /// <exception cref="ArcadeRateLimitException">Thrown when the rate limit for API requests is exceeded.</exception>
        private GenericArcadeResponse GetArcadeResponse(string url, HttpMethod method, string? JSONBody = null, bool asDataArray = false)
        {
            HTTPRequestResponse response = method == HttpMethod.Post ? RequestSender.POST(url, JSONBody) : RequestSender.GET(url);

            if (!response.SendSuccess) throw new ArcadeHTTPException("Failed to get response from Arcade API");

            GenericArcadeResponse? arcadeResponse = GenericArcadeResponse.FromJSON(response.StringContent!, asDataArray);

            if (arcadeResponse == null) throw new ArcadeHTTPException("Failed to parse response from Arcade API");
            if (!arcadeResponse.OK)
            {
                if ((response.StatusCode == HttpStatusCode.NotFound && arcadeResponse.Error == "User not found") ||
                    (response.StatusCode == HttpStatusCode.Unauthorized))
                {
                    throw new ArcadeUnauthorizedException(ArcadeAPIKey);
                }

                if (arcadeResponse.Error!.ToLower().Contains("rate limit")) throw new ArcadeRateLimitException(arcadeResponse.Error);
            }

            return arcadeResponse;
        }

        #endregion

        #region Ping

        /// <summary>
        /// Checks if the Arcade API server is reachable.
        /// </summary>
        /// <returns>True if the server is reachable (returns "pong"), false otherwise.</returns>
        /// <exception cref="ArcadeHTTPException">Thrown when there is an HTTP-related error while communicating with the Arcade API.</exception>
        public bool Ping()
        {
            var res = RequestSender.GET(Paths.Ping);
            if (!res.SendSuccess) throw new ArcadeHTTPException("Failed to get response from Arcade API");

            return res.StatusCode == HttpStatusCode.OK && res.StringContent == "pong";
        }

        /// <summary>
        /// Asynchronously checks if the Arcade API server is reachable.
        /// </summary>
        /// <returns>True if the server is reachable (returns "pong"), false otherwise.</returns>
        /// <exception cref="ArcadeHTTPException">Thrown when there is an HTTP-related error while communicating with the Arcade API.</exception>
        public async Task<bool> PingAsync()
        {
            var res = await RequestSender.GETAsync(Paths.Ping);
            if (!res.SendSuccess) throw new ArcadeHTTPException("Failed to get response from Arcade API");

            return res.StatusCode == HttpStatusCode.OK && res.StringContent == "pong";
        }

        #endregion

        #region Get User Stats

        /// <summary>
        /// Retrieves statistics for the current user from the Arcade API.
        /// </summary>
        /// <returns>User statistics.</returns>
        /// <exception cref="ArcadeHTTPException">Thrown when there is an HTTP-related error while communicating with the Arcade API.</exception>
        /// <exception cref="ArcadeUnauthorizedException">Thrown when authentication with the Arcade API fails.</exception>
        /// <exception cref="ArcadeRateLimitException">Thrown when the rate limit for API requests is exceeded.</exception>
        public ArcadeUserStats GetUserStats()
        {
            var resp = GetArcadeResponse(Paths.Stats, HttpMethod.Get);

            return resp.GetData<ArcadeUserStats>();
        }

        /// <summary>
        /// Asynchronously retrieves statistics for the current user from the Arcade API.
        /// </summary>
        /// <returns>User statistics.</returns>
        /// <exception cref="ArcadeHTTPException">Thrown when there is an HTTP-related error while communicating with the Arcade API.</exception>
        /// <exception cref="ArcadeUnauthorizedException">Thrown when authentication with the Arcade API fails.</exception>
        /// <exception cref="ArcadeRateLimitException">Thrown when the rate limit for API requests is exceeded.</exception>
        public async Task<ArcadeUserStats> GetUserStatsAsync()
        {
            var resp = await GetArcadeResponseAsync(Paths.Stats, HttpMethod.Get);

            return resp.GetData<ArcadeUserStats>();
        }

        #endregion

        #region Get Latest Session

        /// <summary>
        /// Retrieves the latest session information for the current user from the Arcade API.
        /// </summary>
        /// <returns>Latest session information.</returns>
        /// <exception cref="ArcadeHTTPException">Thrown when there is an HTTP-related error while communicating with the Arcade API.</exception>
        /// <exception cref="ArcadeUnauthorizedException">Thrown when authentication with the Arcade API fails.</exception>
        /// <exception cref="ArcadeRateLimitException">Thrown when the rate limit for API requests is exceeded.</exception>
        public ArcadeLatestSession GetLatestSession()
        {
            var resp = GetArcadeResponse(Paths.LatestSession, HttpMethod.Get);

            return resp.GetData<ArcadeLatestSession>();
        }

        /// <summary>
        /// Asynchronously retrieves the latest session information for the current user from the Arcade API.
        /// </summary>
        /// <returns>Latest session information.</returns>
        /// <exception cref="ArcadeHTTPException">Thrown when there is an HTTP-related error while communicating with the Arcade API.</exception>
        /// <exception cref="ArcadeUnauthorizedException">Thrown when authentication with the Arcade API fails.</exception>
        /// <exception cref="ArcadeRateLimitException">Thrown when the rate limit for API requests is exceeded.</exception>
        public async Task<ArcadeLatestSession> GetLatestSessionAsync()
        {
            var resp = await GetArcadeResponseAsync(Paths.LatestSession, HttpMethod.Get);

            return resp.GetData<ArcadeLatestSession>();
        }

        #endregion

        #region Get Goals

        /// <summary>
        /// Retrieves the goals associated with the current user from the Arcade API.
        /// </summary>
        /// <returns>An array of user goals.</returns>
        /// <exception cref="ArcadeHTTPException">Thrown when there is an HTTP-related error while communicating with the Arcade API.</exception>
        /// <exception cref="ArcadeUnauthorizedException">Thrown when authentication with the Arcade API fails.</exception>
        /// <exception cref="ArcadeRateLimitException">Thrown when the rate limit for API requests is exceeded.</exception>
        public ArcadeGoal[] GetGoals()
        {
            var resp = GetArcadeResponse(Paths.Goals, HttpMethod.Get, null, true);

            return resp.GetData<List<ArcadeGoal>>(true).ToArray();
        }

        /// <summary>
        /// Asynchronously retrieves the goals associated with the current user from the Arcade API.
        /// </summary>
        /// <returns>An array of user goals.</returns>
        /// <exception cref="ArcadeHTTPException">Thrown when there is an HTTP-related error while communicating with the Arcade API.</exception>
        /// <exception cref="ArcadeUnauthorizedException">Thrown when authentication with the Arcade API fails.</exception>
        /// <exception cref="ArcadeRateLimitException">Thrown when the rate limit for API requests is exceeded.</exception>
        public async Task<ArcadeGoal[]> GetGoalsAsync()
        {
            var resp = await GetArcadeResponseAsync(Paths.Goals, HttpMethod.Get, null, true);

            return resp.GetData<List<ArcadeGoal>>(true).ToArray();
        }

        #endregion

        #region Get Session History

        /// <summary>
        /// Gets the session history for the user.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArcadeHTTPException"></exception>
        /// <exception cref="ArcadeUnauthorizedException"></exception>
        /// <exception cref="ArcadeRateLimitException"></exception>
        public ArcadeHistorySession[] GetSessionHistory()
        {
            var resp = GetArcadeResponse(Paths.History, HttpMethod.Get, null, true);

            return resp.GetData<List<ArcadeHistorySession>>(true).ToArray();
        }


        /// <summary>
        /// Gets the session history for the user.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArcadeHTTPException"></exception>
        /// <exception cref="ArcadeUnauthorizedException"></exception>
        /// <exception cref="ArcadeRateLimitException"></exception>
        public async Task<ArcadeHistorySession[]> GetSessionHistoryAsync()
        {
            var resp = await GetArcadeResponseAsync(Paths.History, HttpMethod.Get, null, true);

            return resp.GetData<List<ArcadeHistorySession>>(true).ToArray();
        }
        #endregion

        #region Start Session

        /// <summary>
        /// Starts a new session for the user.
        /// </summary>
        /// <param name="work"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArcadeHTTPException"></exception>
        /// <exception cref="ArcadeUnauthorizedException"></exception>
        /// <exception cref="ArcadeSessionException"></exception>
        /// <exception cref="ArcadeRateLimitException"></exception>
        public ArcadeStartResult StartSession(string work)
        {
            if (work == null) throw new ArgumentNullException(nameof(work));

            var resp = GetArcadeResponse(Paths.SessionStart, HttpMethod.Post, $"{{\"work\":\"{work}\"}}");

            if(!resp.OK) throw new ArcadeSessionException(resp.Error!);

            return resp.GetData<ArcadeStartResult>();
        }

        /// <summary>
        /// Starts a new session for the user.
        /// </summary>
        /// <param name="work"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArcadeHTTPException"></exception>
        /// <exception cref="ArcadeUnauthorizedException"></exception>
        /// <exception cref="ArcadeSessionException"></exception>
        /// <exception cref="ArcadeRateLimitException"></exception>
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
        /// Pauses or resumes the current session for the user, depending on the current state.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArcadeHTTPException"></exception>
        /// <exception cref="ArcadeUnauthorizedException"></exception>
        /// <exception cref="ArcadeSessionException"></exception>
        /// <exception cref="ArcadeRateLimitException"></exception>
        public ArcadePauseResult PauseSession()
        {
            var resp = GetArcadeResponse(Paths.SessionPause, HttpMethod.Post);
            if (!resp.OK) throw new ArcadeSessionException(resp.Error!);

            return resp.GetData<ArcadePauseResult>();
        }

        /// <summary>
        /// Pauses or resumes the current session for the user, depending on the current state.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArcadeHTTPException"></exception>
        /// <exception cref="ArcadeUnauthorizedException"></exception>
        /// <exception cref="ArcadeSessionException"></exception>
        /// <exception cref="ArcadeRateLimitException"></exception>
        public async Task<ArcadePauseResult> PauseSessionAsync()
        {
            var resp = await GetArcadeResponseAsync(Paths.SessionPause, HttpMethod.Post);
            if (!resp.OK) throw new ArcadeSessionException(resp.Error!);

            return resp.GetData<ArcadePauseResult>();
        }

        #endregion

        #region Cancel Session

        /// <summary>
        /// Cancels the current session for the user.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArcadeHTTPException"></exception>
        /// <exception cref="ArcadeUnauthorizedException"></exception>
        /// <exception cref="ArcadeRateLimitException"></exception>
        public ArcadeCancelResult CancelSession()
        {
            var resp = GetArcadeResponse(Paths.SessionCancel, HttpMethod.Post);

            return resp.GetData<ArcadeCancelResult>();
        }

        /// <summary>
        /// Cancels the current session for the user.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArcadeHTTPException"></exception>
        /// <exception cref="ArcadeUnauthorizedException"></exception>
        /// <exception cref="ArcadeRateLimitException"></exception>
        public async Task<ArcadeCancelResult> CancelSessionAsync()
        {

            var resp = await GetArcadeResponseAsync(Paths.SessionCancel, HttpMethod.Post);

            return resp.GetData<ArcadeCancelResult>();
        }

        #endregion

    }
}
