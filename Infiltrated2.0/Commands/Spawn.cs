using CommandSystem;
using Exiled.Permissions.Extensions;
using System;

namespace Infiltrated.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    class Spawn : ICommand
    {
        public string Command { get; } = "Infiltrated_spawn";

        public string[] Aliases { get; } = new[] { "ISpawn", "Infiltrated_spwn" };

        public string Description { get; } = "";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            string target;
            if (sender.CheckPermission("infiltrated.spawn"))
            {
                if (arguments.Count == 1)
                {
                    target = arguments.Array[1].ToString();
                    var player = Exiled.API.Features.Player.Get(target);
                    if (!player.IsScp && !player.IsDead && !Infiltrated.Instance.infiltrated.Contains(player.Id))
                    {
                        response = "Player " + player.Nickname + " has become Infiltrated";
                        Events.ClassDSpawn(player);
                        Infiltrated.Instance.infiltrated.Add(player.Id);
                        return false;
                    }
                    else
                    {
                        response = "You can't spawn " + player.Nickname + " because he is an SCP, Spectator or infiltrated";
                        return false;
                    }
                }


                else if (arguments.Count > 1 || arguments.Count < 1)
                {
                    response = $"Usage: {Command}/ISpawn <player name / id>";
                    return false;
                }
            }
            else if (!sender.CheckPermission("infiltrated.spawn"))
            {
                response = "<color=red>You can't do this command!</color>";
                return false;
            }

            response = null;
            return true;
        }
    }
}
