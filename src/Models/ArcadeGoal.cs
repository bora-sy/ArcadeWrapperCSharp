using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackclubArcadeAPIWrapper.Models
{

    public class ArcadeGoal
    {
        /// <summary>
        /// Goal Name
        /// </summary>
        [JsonProperty("name")]
        public string? Name { get; private set; }

        /// <summary>
        /// Goal Minutes
        /// </summary>
        [JsonProperty("minutes")]
        public int Minutes { get; private set; }
    }
}
