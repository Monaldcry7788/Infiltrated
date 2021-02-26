using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;
namespace Infiltrated.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    class Kill : ICommand
    {
        public string Command { get; } = "Infiltrated_kill";

        public string[] Aliases { get; } = new[] { "IKill", "infilt_kill" };

        public string Description { get; } = "Kill an Infiltrated";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("infiltrated.kill"))
            {
                response = "<color=red>You can't do this command!</color>";
                return false;
            }

            if (arguments.Count != 1)
            {
                response = "Usage: Infiltrated_kill/IKill <name or id>";
                return false;
            }

            if (!(Player.Get(arguments.At(0)) is Player target))
            {
                response = "Player not found";
                return false;
            }

            if (Infiltrated.Singleton.TrackedPlayers.Contains(target))
            {
                response = $"Player {target.Nickname} has been killed";
                Logic.Kill(target);
                return true;
            }

            response = $"You can't kill {target.Nickname} because they are not an infiltrated!";
            return false;
        }
    }
}
