using System;
using System.Threading;
using System.Threading.Tasks;
using Library.Message.Attributes;

namespace Library.Message.Commands
{
    public class MapCommand
    {
        [Command(commandName: "map", description: "Joins a map by its id.")]
        public static async void TestingCommandProcess(string argument)
        {
            int.TryParse(argument, out var mapId);
            {
                global::Game.Instance.SendTransferMapRequest(mapId, 1, 1, false);
            } 
        }
    }
}

