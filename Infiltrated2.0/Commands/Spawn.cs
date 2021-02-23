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

                        else
                        {
                            response = $"You can't spawn {target.Nickname}!";
                            return false;
                        }

                    }
                    else
                    {
                        response = "Player not found";
                        return false;
                    }
                }


                else if (arguments.Count != 1)
                {
                    response = $"Usage: {Command}/ISpawn <player name / id>";
                    return false;
                }
            }
            else
            {
                response = "<color=red>You can't do this command!</color>";
                return false;
            }

            response = null;
            return false;
        }
    }
}
