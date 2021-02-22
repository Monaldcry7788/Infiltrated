using CommandSystem;
using Exiled.Permissions.Extensions;
using System;
namespace Infiltrated.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    class Kill : ICommand
    {
        public string Command { get; } = "Infiltrated_kill";

        public string[] Aliases { get; } = new[] { "IKill", "infilt_kill" };

        public string Description { get; } = "";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            string target;
            if (sender.CheckPermission("infiltrated.kill"))
            {
                if (arguments.Count == 1)
                {
                    target = arguments.Array[1].ToString();
                    var player = Exiled.API.Features.Player.Get(target);
                    if (Infiltrated.Instance.infiltrated.Contains(player.Id))
                    {
                        response = "Player " + player.Nickname + " has been killed";
                        Events.Kill(player);
                        return false;
                    }
                    else
                    {
                        response = "You can't kill " + player.Nickname + " Because he is not an infiltrated";
                        return false;
                    }
                }


                else if (arguments.Count > 1 || arguments.Count < 1)
                {
                    response = $"Usage: {Command}/IKill <player name / id>";
                    return false;
                }
            }
            else if (!sender.CheckPermission("infiltrated.kill"))
            {
                response = "<color=red>You can't do this command!</color>";
                return false;
            }

            response = null;
            return true;
        }
    }
}
