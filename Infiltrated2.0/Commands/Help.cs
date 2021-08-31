using CommandSystem;
using Exiled.Permissions.Extensions;
using NorthwoodLib.Pools;
using System;
using System.Text;

namespace Infiltrated.Commands
{
    public class Help : ICommand
    {
        public static Help Instance { get; } = new Help();

        public string Command { get; } = "help";

        public string[] Aliases { get; } = new[] {"h"};

        public string Description { get; } = "Help command";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("infiltrated.help"))
            {
                response = "<color=red>You can't do this command!</color>";
                return false;
            }

            if (arguments.Count != 0)
            {
                response = $"Usage: {Command}/h";
                return false;
            }

            var text = StringBuilderPool.Shared.Rent();
            text.AppendLine($"[ADMIN COMMANDS]");
            text.AppendLine($"Spawn Command:")
                .AppendLine($"Usage: Spawn <player name or id>")
                .AppendLine($"Permission: infiltrated.spawn")
                .AppendLine("Description: Spawn an infiltrated")
                .AppendLine("Alias: s").AppendLine()
                .AppendLine("Kill Command:")
                .AppendLine("Usage: Kill <player name or id>")
                .AppendLine("Permission: infiltrated.kill")
                .AppendLine("Description: Kill an Infiltrated")
                .AppendLine("Alias: k").AppendLine()
                .AppendLine("List Command")
                .AppendLine("Usage: List")
                .AppendLine("Permission: infiltrated.list")
                .AppendLine("Description: Show list of alives Infiltrated")
                .AppendLine("Alias: l").AppendLine()
                .AppendLine("InfoPlayer Command:")
                .AppendLine("Usage: InfoPlayer <player name or id>")
                .AppendLine("Permission: infiltrated.playerinfo")
                .AppendLine("Description: show stats of player")
                .AppendLine("Alias: pi").AppendLine()
                .AppendLine("RandomSpawn Command")
                .AppendLine("Usage: RandomSpawn")
                .AppendLine("Permission: infiltrated.randomspawn")
                .AppendLine("Description: Spawn a random Infiltrated")
                .AppendLine("Alias: rspawn");
            response = StringBuilderPool.Shared.ToStringReturn(text);
            return true;
        }
    }
}