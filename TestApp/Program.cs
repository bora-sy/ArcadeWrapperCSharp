using HackclubArcadeAPIWrapper;
using HackclubArcadeAPIWrapper.HTTPRequest;

namespace TestApp
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
            ArcadeWrapper wrapper = new ArcadeWrapper("apikey");

            var stats = wrapper.GetUserStats();

            Console.WriteLine(stats.Total);
        }
    }

}
