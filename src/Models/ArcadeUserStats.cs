using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackclubArcadeAPIWrapper.Models
{
    public class ArcadeUserStats
    {
        /// <summary>
        /// The total number of sessions.
        /// </summary>
        [JsonProperty("sessions")]
        public int Sessions { get; private set; }

        /// <summary>
        /// The total time across all sessions in minutes.
        /// </summary>
        [JsonProperty("total")]
        public int Total { get; private set; }
    }

}
