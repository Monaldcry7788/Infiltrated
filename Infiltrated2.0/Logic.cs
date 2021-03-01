﻿using Exiled.API.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Infiltrated
{
    public class Logic
    {
        public System.Random Rand = new System.Random();
        private readonly Infiltrated plugin;
        public Logic(Infiltrated plugin) => this.plugin = plugin;
        public void ChooseClassD()
        {
            List<Exiled.API.Features.Player> ClassD = Exiled.API.Features.Player.Get(RoleType.ClassD).Where(classD => !plugin.TrackedPlayers.Contains(classD)).ToList();
            Exiled.API.Features.Player infiltrated = ClassD[Rand.Next(ClassD.Count)];
            ClassDSpawn(infiltrated);
            plugin.TrackedPlayers.Add(infiltrated);
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
                Infiltrated.Singleton.function.Save(player);
            }
        }
        internal static void Kill(Exiled.API.Features.Player player)
        {
            player.SetRole(RoleType.Spectator);
            Infiltrated.Singleton.TrackedPlayers.Remove(player);
        }
    }
}