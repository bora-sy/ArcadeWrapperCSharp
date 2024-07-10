using HackclubArcadeAPIWrapper.HTTPRequest;

namespace TestApp
{
    internal class Program
    {
        static void LogResponse(HTTPRequestResponse resp)
        {
            if (resp.SendSuccess)
            {
                Console.WriteLine("Request suc");
                Console.WriteLine($"Status Code: {resp.StatusCode}");
                Console.WriteLine($"Response: {resp.StringContent}");
            }
            else
            {
                Console.WriteLine("Request failed");
            }
        }

        static async Task Main(string[] args)
        {
            var sender = new HTTPRequestSender("{api key}");

            Console.WriteLine("Sending Request");
            var resp = await sender.GET("https://hackhour.hackclub.com/api/session/{slack member id}");
            Console.WriteLine("Request Sent");


            LogResponse(resp);
        }
    }

}
