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
            if (sender.CheckPermission("infiltrated.kill"))
            {
                if (arguments.Count == 1)
                {
                    Player target = Player.Get(arguments.At(0));
                    if (target != null)
                    {
                        if (Infiltrated.Singleton.infiltrates.Contains(target))
                        {
                            response = $"Player {target.Nickname} has been killed";
                            Events.Kill(target);
                            return true;
                        }
                        else
                        {
                            response = $"You can't kill {target.Nickname} Because he is not an infiltrated";
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
                    response = $"Usage: {Command}/IKill <player name / id>";
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
