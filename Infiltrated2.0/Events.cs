using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;
using System.Collections.Generic;
using System.Linq;
using Player = Exiled.API.Features.Player;
namespace Infiltrated
{
    public class Events
    {
        public System.Random Rand = new System.Random();
        private readonly Infiltrated plugin;
        public Events(Infiltrated plugin) => this.plugin = plugin;


        public void OnRoundStart()
        {
            float timeFloat = Infiltrated.Instance.Config.ClassDSpawnTime;
            string time = System.Convert.ToString(timeFloat);
            foreach (Player player in Player.List)
            {
                player.SetRole(RoleType.ClassD);
            }
            Timing.CallDelayed(4.0f, () =>
            {
                foreach (Player player in Player.List)
                {
                    if (player.Role == RoleType.ClassD)
                    {
                        player.Broadcast(Infiltrated.Instance.Config.ClassDAnnounceDuration, Infiltrated.Instance.Config.ClassDAnnounceMessage.Replace("{seconds}", time));
                    }
                }

            });


            Timing.CallDelayed(Infiltrated.Instance.Config.ClassDSpawnTime, () => ChooseClassD());
        }

        public void ChooseClassD()
        {
            List<Player> ClassD = Player.Get(RoleType.ClassD).ToList();
            Player p1 = ClassD[Rand.Next(ClassD.Count)];
            Timing.CallDelayed(0.5f, () => Logic(p1));
            Infiltrated.Instance.infiltrated.Add(p1.Id);
        }

        internal static void Logic(Player player)
        {
            ClassDSpawn(player);

        }

        internal static void ClassDSpawn(Player player)
        {

            player.Ammo[(int)AmmoType.Nato556] = 250;
            player.Ammo[(int)AmmoType.Nato762] = 250;
            player.Ammo[(int)AmmoType.Nato9] = 250;
            player.MaxHealth = Infiltrated.Instance.Config.HealtAmount;
            player.Health = Infiltrated.Instance.Config.HealtAmount;
            if (Infiltrated.Instance.Config.InfiltratedItem.Count < 9)
            {
                for (int x = 0; x < Infiltrated.Instance.Config.InfiltratedItem.Count; x++)
                {
                    player.Inventory.AddNewItem((ItemType)Infiltrated.Instance.Config.InfiltratedItem[x]);
                }
            }
            else
            {
                Log.Error("Error: You can't give the Infiltrator more than 9 items!");
            }
            player.ClearBroadcasts();
            player.Broadcast(Infiltrated.Instance.Config.InfiltratedBroadCastDuration, Infiltrated.Instance.Config.InfiltratedBroadcastMessage);

        }

        public void onDied(DiedEventArgs ev)
        {
            if (Infiltrated.Instance.infiltrated.Contains(ev.Target.Id))
            {
                if (Infiltrated.Instance.Config.IsEnabledDeathAnnounceInfiltrated)
                {
                    Map.Broadcast(Infiltrated.Instance.Config.InfiltratedDeathAnnounceDuration, Infiltrated.Instance.Config.InfiltratedBroadcastDeath.Replace("{player}", ev.Target.Nickname));
                    Infiltrated.Instance.infiltrated.Remove(ev.Target.Id);
                }
            }
            else
            {
                return;
            }
        }

        public void onRoundEnd(EndingRoundEventArgs ev)
        {
            Infiltrated.Instance.infiltrated.Clear();
        }

        internal static void Kill(Player player)
        {
            player.ClearInventory();
            player.SetRole(RoleType.Spectator);
            Infiltrated.Instance.infiltrated.Remove(player.Id);
        }

    }
}
