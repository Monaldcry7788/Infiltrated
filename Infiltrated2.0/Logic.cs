using Exiled.API.Enums;
using Exiled.API.Features;
using scp035.API;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infiltrated
{
    public class Logic
    {
        public System.Random Rand = new System.Random();
        int random = new Random().Next(0, 100);
        private readonly Infiltrated plugin;
        List<Exiled.API.Features.Player> sh = null;
        Exiled.API.Features.Player scp035 = null;
        public Logic(Infiltrated plugin) => this.plugin = plugin;
        public void ChooseClassD()
        {
            if (Exiled.API.Features.Player.List.Count() == plugin.Config.SpawnMinium)
            {
                if (random <= plugin.Config.SpawnChance)
                {
                    if (Infiltrated.is035)
                    {
                        scp035 = TryGet035();
                    }
                    if (Infiltrated.isSH)
                    {
                        sh = TrySH();
                    }
                    List<Exiled.API.Features.Player> ClassD = Exiled.API.Features.Player.Get(RoleType.ClassD).Where(classD => !plugin.TrackedPlayers.Contains(classD) && classD != scp035 && !sh.Contains(classD)).ToList();
                    if (ClassD.IsEmpty())
                    {
                        Log.Info("I don't found any player to spawn!");
                    }
                    Exiled.API.Features.Player infiltrated = ClassD[Rand.Next(ClassD.Count)];
                    ClassDSpawn(infiltrated);
                    plugin.TrackedPlayers.Add(infiltrated);
                }
            }
        }
        public void ClassDSpawn(Exiled.API.Features.Player player)
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
        public void Kill(Exiled.API.Features.Player player)
        {
            player.SetRole(RoleType.Spectator);
            Infiltrated.Singleton.TrackedPlayers.Remove(player);
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