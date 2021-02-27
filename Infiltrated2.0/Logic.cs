using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;
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
            List<Player> ClassD = Player.Get(RoleType.ClassD).Where(classD => !plugin.TrackedPlayers.Contains(classD)).ToList();
            Player infiltrated = ClassD[Rand.Next(ClassD.Count)];
            Timing.CallDelayed(0.5f, () => ClassDSpawn(infiltrated));
            plugin.TrackedPlayers.Add(infiltrated);
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
            internal static void Kill(Player player)
            {
                player.SetRole(RoleType.Spectator);
                Infiltrated.Singleton.TrackedPlayers.Remove(player);
            }
        }
    }
