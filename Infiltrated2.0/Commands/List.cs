using CommandSystem;
using Exiled.Permissions.Extensions;
using NorthwoodLib.Pools;
using System;
using System.Text;

namespace Infiltrated.Commands
{
    public class List : ICommand
    {
        public static List Instance { get; } = new List();

        public string Command { get; } = "list";

        public string[] Aliases { get; } = new[] {"l"};

        public string Description { get; } = "Show list of alives infiltrated";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("infiltrated.list"))
            {
                response = "<color=red>You can't do this command!</color>";
                return false;
            }

            if (arguments.Count != 0)
            {
                response = "Usage: List/l";
                return false;
            }

            var text = StringBuilderPool.Shared.Rent();
            text.AppendLine($"[INFILTRATED ALIVE ({Infiltrated.Singleton.TrackedPlayers.Count})]");
            foreach (var infiltrated in Infiltrated.Singleton.TrackedPlayers)
                text.AppendLine($"[Player: {infiltrated.Nickname} ({infiltrated.UserId})]")
                    .AppendLine($"[Player Health: {infiltrated.Health}HP]")
                    .AppendLine($"[Player Role: {infiltrated.Role}]").AppendLine();

            response = StringBuilderPool.Shared.ToStringReturn(text);
            return true;
        }
    }
}