using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackClub.Arcade.HTTPRequest
{
    internal static class Paths
    {
        public const string BASE_URL = "https://hackhour.hackclub.com";

        public const string Ping = BASE_URL + "/ping";

        public const string LatestSession = BASE_URL + "/api/session/a";

        public const string Stats = BASE_URL + "/api/stats/a";
        public const string Goals = BASE_URL + "/api/goals/a";
        public const string History = BASE_URL + "/api/history/a";

        public const string SessionStart = BASE_URL + "/api/start/a";
        public const string SessionPause = BASE_URL + "/api/pause/a";
        public const string SessionCancel = BASE_URL + "/api/cancel/a";

        // DISCLAIMER: SlackID is not a requirement for the users
    }
}
