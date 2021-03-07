using CommandSystem;
using Exiled.Permissions.Extensions;
using System;
using System.Collections.Generic;
using scp035.API;

namespace Infiltrated.Commands
{
    public class Spawn : ICommand
    {
        List<Exiled.API.Features.Player> sh = null;
        Exiled.API.Features.Player scp035 = null;
        public static Spawn Instance { get; } = new Spawn();

        public string Command { get; } = "spawn";

        public string[] Aliases { get; } = new[] { "s" };

        public string Description { get; } = "Spawn an infiltrated";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (Infiltrated.is035)
            {
                scp035 = TryGet035();
            }
            if (Infiltrated.isSH)
            {
                sh = TrySH();
            }
            if (!sender.CheckPermission("infiltrated.spawn"))
            {
                response = "<color=red>You can't do this command!</color>";
                return false;
            }

            if (arguments.Count != 1)
            {
                response = "Usage: Spawn/s <player name or id>";
                return false;
            }

            if (!(Exiled.API.Features.Player.Get(arguments.At(0)) is Exiled.API.Features.Player target))
            {
                response = "Player not found";
                return false;
            }

            if (!target.IsScp && !target.IsDead && !Infiltrated.Singleton.TrackedPlayers.Contains(target) && !sh.Contains(target) && target != scp035)
            {
                response = $"Player {target.Nickname} has become Infiltrated";
                Infiltrated.Singleton.Logic.ClassDSpawn(target);
                Infiltrated.Singleton.TrackedPlayers.Add(target);
                return true;
            }

            response = $"You can't spawn {target.Nickname}!";
            return false;
        }
        private Exiled.API.Features.Player TryGet035()
        {
            return Scp035Data.GetScp035();
        }
        private List<Exiled.API.Features.Player> TrySH()
        {
            return SerpentsHand.API.SerpentsHand.GetSHPlayers();
        }
    }
}
