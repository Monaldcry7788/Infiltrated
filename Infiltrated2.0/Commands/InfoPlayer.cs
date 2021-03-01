using CommandSystem;
using Exiled.Permissions.Extensions;
using NorthwoodLib.Pools;
using System;
using System.Text;

namespace Infiltrated.Commands
{
    public class InfoPlayer : ICommand
    {
        public static InfoPlayer Instance { get; } = new InfoPlayer();

        public string Command { get; } = "InfoPlayer";

        public string[] Aliases { get; } = new[] { "pi" };

        public string Description { get; } = "Show player info";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("infiltrated.playerinfo"))
            {
                response = "<color=red>You can't do this command!</color>";
                return false;
            }
            if (arguments.Count != 1)
            {
                response = "Usage: InfoPlayer/pi <player name or id>";
                return false;
            }
            if (!Infiltrated.Singleton.Config.IsDatabaseEnaled)
            {
                response = "The command is disabled because the database has been deactivated from the server config";
                return false;
            }
            var target = Exiled.API.Features.Player.Get(arguments.At(0));
            var PlayerDB = target.GetPlayerDB();
            if (PlayerDB == null)
            {
                response = "Player not found";
                return false;
            }
            StringBuilder text = StringBuilderPool.Shared.Rent().AppendLine();
            text.AppendLine($"[Player: {PlayerDB.Name} ({PlayerDB.Id}@{PlayerDB.Auth})]")
                .AppendLine($"[Total Game Played as Infiltrated: {PlayerDB.TotalRoundPlayed}]");
            response = StringBuilderPool.Shared.ToStringReturn(text);
            return true;
        }
    }
}
