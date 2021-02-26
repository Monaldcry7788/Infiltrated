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
                            response = $"You can't kill {target.Nickname} Because he is not an infiltrated";
                            return false;
                    }
                        response = "Player not found";
                        return false;
                }
                response = "Usage: infiltrated_kill/IKill <name or id>";
                return false;
            }
            response = "<color=red>You can't do this command!</color>";
            return false;
        }
    }
}
