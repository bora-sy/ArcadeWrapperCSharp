using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackClub.Arcade.Exceptions
{
    public class ArcadeHTTPException : ArcadeException
    {
        public ArcadeHTTPException(string message) : base(message)
        {
        }
    }
}
