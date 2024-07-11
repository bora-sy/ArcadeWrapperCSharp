using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackClub.Arcade.Exceptions
{
    public class ArcadeRateLimitException : ArcadeException
    {
        public ArcadeRateLimitException(string message) : base(message) { }
    }
}
