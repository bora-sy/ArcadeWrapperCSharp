using HackclubArcadeAPIWrapper;
using HackclubArcadeAPIWrapper.HTTPRequest;

namespace TestApp
{
    internal class Program
    {

        static void LogObj(object obj)
        {
            var type = obj.GetType();

            Console.WriteLine($"\n\n---{type.Name}---");
            if(type.IsArray)
            {
                Console.WriteLine("(ARRAY)");
                foreach (var item in (Array)obj)
                {
                    Console.WriteLine("-------");
                    item.GetType().GetProperties().ToList().ForEach(prop => Console.WriteLine($"{prop.Name}: {prop.GetValue(item)}"));
                }
            }
            else type.GetProperties().ToList().ForEach(prop => Console.WriteLine($"{prop.Name}: {prop.GetValue(obj)}"));
        }

        static async Task Main(string[] args)
        {
            ArcadeWrapper wrapper = new ArcadeWrapper("apikey");

            Console.WriteLine("Press ENTER to test actions");
            Console.ReadLine();

            var result = await wrapper.PauseSessionAsync();

        }
    }

}
