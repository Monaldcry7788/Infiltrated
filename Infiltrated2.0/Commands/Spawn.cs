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
            if (sender.CheckPermission("infiltrated.spawn"))
            {
                if (arguments.Count == 1)
                {
                    Player target = Player.Get(arguments.At(0));
                    if (target != null)
                    {
                        if (!target.IsScp && !target.IsDead && !Infiltrated.Singleton.infiltrates.Contains(target))
                        {
                            response = $"Player {target.Nickname} has become Infiltrated";
                            Events.ClassDSpawn(target);
                            Infiltrated.Singleton.infiltrates.Add(target);
                            return true;
                        }
                            response = $"You can't spawn {target.Nickname}!";
                            return false;
                    }
                        response = "Player not found";
                        return false;
                }
                response = "Usage: Infiltrated_spawn/IKill <player name or id>";
                return false;
            }
                response = "<color=red>You can't do this command!</color>";
                return false;
        }
    }
}
