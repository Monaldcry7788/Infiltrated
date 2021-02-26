using CommandSystem;
using Exiled.Permissions.Extensions;
using NorthwoodLib.Pools;
using System;
using System.Text;
using Exiled.API.Features;

namespace Infiltrated.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class List : ICommand
    {
        public string Command { get; } = "Infiltrated_list";

        public string[] Aliases { get; } = new[] { "IList", "infiltr_list" };

        public string Description { get; } = "Show all Infiltrated alives";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            /*if (sender.CheckPermission("infiltrated.list"))
            {
                if (arguments.Count == 0)
                {
                    StringBuilder text = StringBuilderPool.Shared.Rent();
                    text.AppendLine($"[INFILTRATED ALIVE ({Infiltrated.Singleton.TrackedPlayers.Count})]");

                    foreach (var infiltrated in Infiltrated.Singleton.TrackedPlayers)
                    {
                        text.AppendLine($"[Player: {infiltrated.Nickname} ({infiltrated.UserId})]")
                            .AppendLine($"[Player Health: {infiltrated.Health}HP]")
                            .AppendLine($"[Player Role: {infiltrated.Role}]");
                    }
                    response = StringBuilderPool.Shared.ToStringReturn(text);
                    return true;
                }
                    response = "Usage: Infiltrated_list/IList";
                    return false;
            }
            response = "<color=red>You can't do this command!</color>";
            return false;
            */

            if (!sender.CheckPermission("infiltrated.list"))
            {
                response = "<color=red>You can't do this command!</color>";
                return false;
            }

            if (arguments.Count != 0)
            {
                response = "Usage: Infiltrated_list/IList";
                return false;
            }

            StringBuilder text = StringBuilderPool.Shared.Rent();
            text.AppendLine($"[INFILTRATED ALIVE ({Infiltrated.Singleton.TrackedPlayers.Count})]");

            foreach (var infiltrated in Infiltrated.Singleton.TrackedPlayers)
            {
                text.AppendLine($"[Player: {infiltrated.Nickname} ({infiltrated.UserId})]")
                    .AppendLine($"[Player Health: {infiltrated.Health}HP]")
                    .AppendLine($"[Player Role: {infiltrated.Role}]");
            }
            response = StringBuilderPool.Shared.ToStringReturn(text);
            return true;

        }
    }
}
