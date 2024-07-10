﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackclubArcadeAPIWrapper.Models
{
    public class ArcadeGoal
    {
        [JsonProperty("name")]
        public string? Name { get; private set; }

        [JsonProperty("minutes")]
        public int Minutes { get; private set; }
    }
}
