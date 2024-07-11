using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackclubArcadeAPIWrapper.Models
{

    public class ArcadeHistorySession
    {
        /// <summary>
        /// The date and time when the session was created.
        /// </summary>
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; private set; }

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
        /// The current goal of the session.
        /// </summary>
        [JsonProperty("goal")]
        public string? Goal { get; private set; }

        /// <summary>
        /// Indicates whether the session has ended.
        /// </summary>
        [JsonProperty("ended")]
        public bool Ended { get; private set; }

        /// <summary>
        /// The work associated with the session.
        /// </summary>
        [JsonProperty("work")]
        public string? Work { get; private set; }
    }
}
