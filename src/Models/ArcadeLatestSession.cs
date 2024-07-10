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
        [JsonProperty("id")]
        public string? SlackID { get; private set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; private set; }

        [JsonProperty("endTime")]
        public DateTime EndTime { get; private set; }

        [JsonProperty("time")]
        public int Time { get; private set; }

        [JsonProperty("elapsed")]
        public int Elapsed { get; private set; }

        [JsonProperty("remaining")]
        public int Remaining { get; private set; }

        [JsonProperty("goal")]
        public string? Goal { get; private set; }

        [JsonProperty("paused")]
        public bool Paused { get; private set; }

        [JsonProperty("completed")]
        public bool Completed { get; private set; }

        [JsonProperty("messageTs")]
        public string? MessageTs { get; private set; }
    }
}
