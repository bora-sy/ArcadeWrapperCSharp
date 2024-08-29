using HackClub.Arcade;

namespace SimpleUsage
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string apiKey = "YOUR_API_KEY";

            const string slackMemberID = "YOUR_SLACK_MEM_ID";

            ArcadeWrapper arcade = new ArcadeWrapper(apiKey);

            bool pingResult = arcade.Ping();

            if(pingResult) Console.WriteLine("Arcade API is online\n");
            else
            {
                Console.WriteLine("Arcade API is offline");
                Console.ReadLine();
                return;
            }

            var userStats = arcade.GetUserStats();
            Console.WriteLine($"You have {userStats.Sessions} sessions in total, which adds up to {userStats.Total} minutes!\n");

            var latestSession = arcade.GetLatestSession(slackMemberID);
            Console.WriteLine($"Your latest session was '{latestSession.Work}' with {latestSession.Elapsed} minutes elapsed, {latestSession.Remaining} minutes remaining!");

            Console.ReadLine();
        }
    }
}
