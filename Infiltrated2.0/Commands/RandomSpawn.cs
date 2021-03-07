using CommandSystem;
using Exiled.API.Enums;
using Exiled.Permissions.Extensions;
using scp035.API;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infiltrated.Commands
{
    public class RandomSpawn : ICommand
    {
        public System.Random Rand = new System.Random();
        public static RandomSpawn Instance { get; } = new RandomSpawn();
        List<Exiled.API.Features.Player> sh = null;
        Exiled.API.Features.Player scp035 = null;
        public string Command { get; } = "RandomSpawn";
        public string[] Aliases { get; } = new[] { "RSpawn" };
        public string Description { get; } = "Spawn a random infiltrated";
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
            if (!sender.CheckPermission("infiltrated.randomspawn"))
            {
                response = "<color=red>You can't do this command!</color>";
                return false;
            }
            if (arguments.Count != 0)
            {
                response = "Usage: RandomSpawn/RSpawn";
                return false;
            }
            List<Exiled.API.Features.Player> players = Exiled.API.Features.Player.List.Where(x => !Infiltrated.Singleton.TrackedPlayers.Contains(x) && !x.IsScp && !x.IsDead && x != scp035 && !sh.Contains(x)).ToList();
            if (!players.IsEmpty())
            {
                Exiled.API.Features.Player infiltrated = players[Rand.Next(players.Count)];
                ClassDSpawn(infiltrated);
                Infiltrated.Singleton.TrackedPlayers.Add(infiltrated);
                response = $"Player {infiltrated.Nickname} has become an infiltrated";
                return true;
            }
            response = "I don't found any player!";
            return false;
        }
        internal static void ClassDSpawn(Exiled.API.Features.Player player)
        {
            player.Ammo[(int)AmmoType.Nato556] = 250;
            player.Ammo[(int)AmmoType.Nato762] = 250;
            player.Ammo[(int)AmmoType.Nato9] = 250;
            player.Health = player.MaxHealth = Infiltrated.Singleton.Config.HealthAmount;
            player.ResetInventory(Infiltrated.Singleton.Config.InfiltratedItems);
            player.ClearBroadcasts();
            player.Broadcast(Infiltrated.Singleton.Config.InfiltratedBroadcast);
            if (Infiltrated.Singleton.Config.IsDatabaseEnaled)
            {
                player.GetPlayerDB().TotalRoundPlayed++;
                Database.LiteDatabase.GetCollection<Player>().Update(Database.PlayerData[player]);
            }
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
