using HackClub.Arcade;

namespace Example
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string apiKey = "YOUR_API_KEY";

            ArcadeWrapper arcade = new ArcadeWrapper(apiKey);

            bool pingResult = arcade.Ping();

            if(pingResult) Console.WriteLine("Arcade API is online");
            else
            {
                Console.WriteLine("Arcade API is offline");
                Console.ReadLine();
                return;
            }

            var userStats = arcade.GetUserStats();
            Console.WriteLine($"You have {userStats.Sessions} sessions in total, which adds up to {userStats.Total} minutes!");

            var latestSession = arcade.GetLatestSession();
            Console.WriteLine($"Your latest session was '{latestSession.Work}' with {latestSession.Elapsed} minutes elapsed, {latestSession.Remaining} minutes remaining!");
        }
    }
}
