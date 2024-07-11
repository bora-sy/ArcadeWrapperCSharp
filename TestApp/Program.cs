using HackclubArcadeAPIWrapper;
using HackclubArcadeAPIWrapper.HTTPRequest;

namespace TestApp
{
    internal class Program
    {

        static void LogObj(object obj)
        {
            Console.WriteLine($"\n\n---{obj}---");
            obj.GetType().GetProperties().ToList().ForEach(prop => Console.WriteLine($"{prop.Name}: {prop.GetValue(obj)}"));
        }

        static async Task Main(string[] args)
        {
            ArcadeWrapper wrapper = new ArcadeWrapper("apikey");

            LogObj(wrapper.GetGoals());
            LogObj(wrapper.GetLatestSession());

        }
    }

}
