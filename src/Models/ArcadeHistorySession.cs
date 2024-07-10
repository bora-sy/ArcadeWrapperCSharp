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
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; private set; }

        [JsonProperty("time")]
        public int Time { get; private set; }

        [JsonProperty("elapsed")]
        public int Elapsed { get; private set; }

        [JsonProperty("goal")]
        public string? Goal { get; private set; }

        [JsonProperty("ended")]
        public bool Ended { get; private set; }

        [JsonProperty("work")]
        public string? Work { get; private set; }
    }
}
