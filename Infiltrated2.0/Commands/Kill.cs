using CommandSystem;
using Exiled.Permissions.Extensions;
using System;

namespace Infiltrated.Commands
{
    public class Kill : ICommand
    {
        public static Kill Instance { get; } = new Kill();

        public string Command { get; } = "Kill";

        public string[] Aliases { get; } = new[] {"k"};

        public string Description { get; } = "Kill an infiltrated";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("infiltrated.kill"))
            {
                response = "<color=red>You can't do this command!</color>";
                return false;
            }

            if (arguments.Count != 1)
            {
                response = "Usage: Kill/k <name or id>";
                return false;
            }

            if (!(Exiled.API.Features.Player.Get(arguments.At(0)) is Exiled.API.Features.Player target))
            {
                response = "Player not found";
                return false;
            }

            if (Infiltrated.Singleton.TrackedPlayers.Contains(target))
            {
                response = $"Player {target.Nickname} has been killed";
                Infiltrated.Singleton.api.KillInfiltrated(target);
                return true;
            }

            response = $"You can't kill {target.Nickname} because they are not an infiltrated!";
            return false;
        }
    }
}