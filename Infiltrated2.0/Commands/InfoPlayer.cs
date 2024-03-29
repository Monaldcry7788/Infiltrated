﻿using CommandSystem;
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

        public string[] Aliases { get; } = new[] {"pi"};

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

            var target = Exiled.API.Features.Player.Get(arguments.At(0));
            var PlayerDB = target.GetPlayerDB();
            if (PlayerDB == null)
            {
                response = "Player not found";
                return false;
            }

            var text = StringBuilderPool.Shared.Rent().AppendLine();
            text.AppendLine($"[Player: {PlayerDB.Name} ({PlayerDB.Id}@{PlayerDB.Auth})]")
                .AppendLine($"[Total game played as Infiltrated: {PlayerDB.TotalRoundPlayed}]")
                .AppendLine($"[Total kill as Infiltrated: {PlayerDB.TotalKill}]")
                .AppendLine($"[Total death as Infiltrated {PlayerDB.TotalDeath}]")
                .AppendLine($"[Total Shots Fired: {PlayerDB.TotalShotsFired}]");
            response = StringBuilderPool.Shared.ToStringReturn(text);
            return true;
        }
    }
}