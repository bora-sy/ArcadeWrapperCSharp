using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackclubArcadeAPIWrapper.Models
{
    public class ArcadeLatestSession
    {
        /// <summary>
        /// Slack Member ID.
        /// </summary>
        [JsonProperty("id")]
        public string? SlackID { get; private set; }

        /// <summary>
        /// The date and time when the session was created.
        /// </summary>
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// The date and time when the session ended.
        /// </summary>
        [JsonProperty("endTime")]
        public DateTime EndTime { get; private set; }

        /// <summary>
        /// The total duration of the session in minutes.
        /// </summary>
        [JsonProperty("time")]
        public int Time { get; private set; }

        /// <summary>
        /// The elapsed time of the session in minutes.
        /// </summary>
        [JsonProperty("elapsed")]
        public int Elapsed { get; private set; }

        /// <summary>
        /// The remaining time of the session in minutes.
        /// </summary>
        [JsonProperty("remaining")]
        public int Remaining { get; private set; }

        /// <summary>
        /// The current goal of the session.
        /// </summary>
        [JsonProperty("goal")]
        public string? Goal { get; private set; }

        /// <summary>
        /// Indicates whether the session is paused.
        /// </summary>
        [JsonProperty("paused")]
        public bool Paused { get; private set; }

        /// <summary>
        /// Indicates whether the session is completed.
        /// </summary>
        [JsonProperty("completed")]
        public bool Completed { get; private set; }

        /// <summary>
        /// The timestamp of the message associated with the session.
        /// </summary>
        [JsonProperty("messageTs")]
        public string? MessageTs { get; private set; }
    }

}
