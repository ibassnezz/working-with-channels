using System;
using System.Threading.Tasks;
using Channels.Core;

namespace WorkingWithChannels
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var space = new ChannelSpace<string>();
            var writer = space.RetrieveWriter(new Identification("Writer"));
            space.ApplyReader(new ReaderAction(new Identification("Reader_1")));
            space.ApplyReader(new ReaderAction(new Identification("Reader_2")));

            var input = "";
            while (input != "q")
            {
                input = Console.ReadLine();
                await writer.Push(input);
            }
            
            space.Complete();
        }
    }
}
