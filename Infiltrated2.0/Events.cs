﻿using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;
using System.Collections.Generic;
using System.Linq;
namespace Infiltrated
{
    public class Events
    {
        public System.Random Rand = new System.Random();
        private readonly Infiltrated plugin;
        public Events(Infiltrated plugin) => this.plugin = plugin;

        public void OnRoundStart()
        {
            Infiltrated.Singleton.infiltrates.Clear();
            Timing.CallDelayed(2.0f, () =>
            {
                foreach (Player player in Player.List)
                {
                    if (player.Role == RoleType.ClassD)
                    {
                        player.Broadcast(Infiltrated.Singleton.Config.ClassDBroadcast.Duration, Infiltrated.Singleton.Config.ClassDBroadcast.Content.Replace("{seconds}", Infiltrated.Singleton.Config.ClassDSpawnTime.ToString()));
                    }
                }
            });
            Timing.CallDelayed(Infiltrated.Singleton.Config.ClassDSpawnTime, () => ChooseClassD());
        }

        public void ChooseClassD()
        {
            List<Player> ClassD = Player.Get(RoleType.ClassD).Where(classD => !Infiltrated.Singleton.infiltrates.Contains(classD)).ToList();
            Player infiltrated = ClassD[Rand.Next(ClassD.Count)];
                Timing.CallDelayed(0.5f, () => ClassDSpawn(infiltrated));
                Infiltrated.Singleton.infiltrates.Add(infiltrated);   
        }

        internal static void ClassDSpawn(Player player)
        {
            player.Ammo[(int)AmmoType.Nato556] = 250;
            player.Ammo[(int)AmmoType.Nato762] = 250;
            player.Ammo[(int)AmmoType.Nato9] = 250;
            player.Health = player.MaxHealth = Infiltrated.Singleton.Config.HealthAmount;
            player.ResetInventory(Infiltrated.Singleton.Config.InfiltratedItems);
            player.ClearBroadcasts();
            player.Broadcast(Infiltrated.Singleton.Config.InfiltratedBroadcast);
        }

        public void OnDied(DiedEventArgs ev)
        {
            if (Infiltrated.Singleton.infiltrates.Contains(ev.Target) && Infiltrated.Singleton.Config.InfiltratedBroadcastDeath.Show)
            {
                    Map.Broadcast(Infiltrated.Singleton.Config.InfiltratedBroadcastDeath.Duration, Infiltrated.Singleton.Config.InfiltratedBroadcastDeath.Content.Replace("{player}", ev.Target.Nickname));
                    Infiltrated.Singleton.infiltrates.Remove(ev.Target);
            }
        }

        internal static void Kill(Player player)
        {
            player.SetRole(RoleType.Spectator);
            Infiltrated.Singleton.infiltrates.Remove(player);
        }

    }
}
