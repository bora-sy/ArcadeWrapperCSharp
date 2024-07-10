using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackclubArcadeAPIWrapper.Models
{
    public class ArcadePauseResult
    {
        [JsonProperty("id")]
        public string? SessionID { get; private set; }

        [JsonProperty("slackId")]
        public string? SlackID { get; private set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; private set; }

        [JsonProperty("paused")]
        public bool Paused { get; private set; }
    }
}
