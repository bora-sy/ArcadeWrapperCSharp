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
        [JsonProperty("sessions")]
        public int Sessions { get; private set; }

        [JsonProperty("total")]
        public int Total { get; private set; }
    }
}
