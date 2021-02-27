using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;
namespace Infiltrated.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    class Spawn : ICommand
    {
        public string Command { get; } = "Infiltrated_spawn";

        public string[] Aliases { get; } = new[] { "ISpawn", "Infiltrated_spwn" };

        public string Description { get; } = "Spawn an Infiltrated";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("infiltrated.spawn"))
            {
                response = "<color=red>You can't do this command!</color>";
                return false;
            }

            if (arguments.Count != 1)
            {
                response = "Usage: Infiltrated_spawn/IKill <player name or id>";
                return false;
            }

            if (!(Player.Get(arguments.At(0)) is Player target))
            {
                response = "Player not found";
                return false;
            }

            if (!target.IsScp && !target.IsDead && !Infiltrated.Singleton.TrackedPlayers.Contains(target))
            {
                response = $"Player {target.Nickname} has become Infiltrated";
                Logic.ClassDSpawn(target);
                Infiltrated.Singleton.TrackedPlayers.Add(target);
                return true;
            }

            response = $"You can't spawn {target.Nickname}!";
            return false;
        }
    }
}
