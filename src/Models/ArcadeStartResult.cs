using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackclubArcadeAPIWrapper.Models
{
    public class ArcadeStartResult
    {
        /// <summary>
        /// The unique identifier of the session.
        /// </summary>
        [JsonProperty("id")]
        public string? SessionID { get; private set; }

        /// <summary>
        /// Slack Member ID.
        /// </summary>
        [JsonProperty("slackId")]
        public string? SlackID { get; private set; }

        /// <summary>
        /// The date and time when the session was created.
        /// </summary>
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; private set; }
    }

}
