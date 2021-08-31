using CommandSystem;
using Exiled.Permissions.Extensions;
using System;
using System.Collections.Generic;

namespace Infiltrated.Commands
{
    public class Spawn : ICommand
    {
        private List<Exiled.API.Features.Player> sh = null;
        private Exiled.API.Features.Player scp035 = null;
        public static Spawn Instance { get; } = new Spawn();

        public string Command { get; } = "spawn";

        public string[] Aliases { get; } = new[] {"s"};

        public string Description { get; } = "Spawn an infiltrated";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
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

            if (!target.IsScp && !target.IsDead && !Infiltrated.Singleton.TrackedPlayers.Contains(target))
            {
                response = $"Player {target.Nickname} has become Infiltrated";
                target.GameObject.AddComponent<InfiltratedComponent>();
                Infiltrated.Singleton.TrackedPlayers.Add(target);
                return true;
            }

            response = $"You can't spawn {target.Nickname}!";
            return false;
        }

        /*private IEnumerable<Exiled.API.Features.Player> TryGet035()
        {
            return Scp035.API.AllScp035;
        }

        private List<Exiled.API.Features.Player> TrySH()
        {
            return SerpentsHand.API.GetSHPlayers();
        }
        */
    }
}