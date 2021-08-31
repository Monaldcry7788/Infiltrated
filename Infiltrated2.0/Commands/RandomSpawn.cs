using CommandSystem;
using Exiled.API.Enums;
using Exiled.Permissions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infiltrated.Commands
{
    public class RandomSpawn : ICommand
    {
        public Random Rand = new Random();
        public static RandomSpawn Instance { get; } = new RandomSpawn();
        private List<Exiled.API.Features.Player> sh = null;
        private Exiled.API.Features.Player scp035 = null;
        public string Command { get; } = "RandomSpawn";
        public string[] Aliases { get; } = new[] {"RSpawn"};
        public string Description { get; } = "Spawn a random infiltrated";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("infiltrated.randomspawn"))
            {
                response = "<color=red>You can't do this command!</color>";
                return false;
            }

            if (arguments.Count != 0)
            {
                response = "Usage: RandomSpawn/RSpawn";
                return false;
            }

            Infiltrated.Singleton.Logic.ChooseClassD();
            response = $"Player choosed!";
            return true;
        }
    }
}