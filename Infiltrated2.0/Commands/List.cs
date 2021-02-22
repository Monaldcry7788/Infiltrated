using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;

namespace Infiltrated.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    class List : ICommand
    {
        public string Command { get; } = "Infiltrated_list";

        public string[] Aliases { get; } = new[] { "IList", "infiltr_list" };

        public string Description { get; } = "";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            string target;
            if (sender.CheckPermission("infiltrated.list"))
            {
                if (arguments.Count == 0)
                {
                    string list = "Alive Infiltrated:\n";
                    foreach (var player in Player.List)
                    {
                        if (Infiltrated.Instance.infiltrated.Contains(player.Id)) list += $"Player: {player.Nickname} ({player.UserId})\n";
                    }
                    if (Infiltrated.Instance.infiltrated.Count == 0) list = "No Infiltrated alive";
                    response = list;
                    return true;
                }


                else if (arguments.Count > 0)
                {
                    response = $"Usage: {Command}/IList";
                    return false;
                }
            }
            else if (!sender.CheckPermission("infiltrated.list"))
            {
                response = "<color=red>You can't do this command!</color>";
                return false;
            }

            response = null;
            return true;
        }
    }
}
