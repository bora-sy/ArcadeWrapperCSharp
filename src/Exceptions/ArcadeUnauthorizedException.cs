using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackclubArcadeAPIWrapper.Exceptions
{
    public class ArcadeUnauthorizedException : ArcadeException
    {
        public string? ArcadeAPIKey { get; }

        public ArcadeUnauthorizedException(string? arcadeAPIKey) : base("Incorrect API Key")
        {
            ArcadeAPIKey = arcadeAPIKey;
        }
    }
}
