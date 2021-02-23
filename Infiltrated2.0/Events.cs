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

            Timing.CallDelayed(4.0f, () =>
            {
                foreach (Player player in Player.List)
                {
                    if (player.Role == RoleType.ClassD)
                    {
                        player.Broadcast(Infiltrated.Instance.Config.ClassDBroadcast.Duration, Infiltrated.Instance.Config.ClassDBroadcast.Content.Replace("{seconds}", Infiltrated.Instance.Config.ClassDSpawnTime.ToString()));
                    }
                }

            });


            Timing.CallDelayed(Infiltrated.Instance.Config.ClassDSpawnTime, () => ChooseClassD());
        }

        public void ChooseClassD()
        {
            List<Player> ClassD = Player.Get(RoleType.ClassD).ToList();
            Player infiltrated = ClassD[Rand.Next(ClassD.Count)];
            Timing.CallDelayed(0.5f, () => ClassDSpawn(infiltrated));
            Infiltrated.Instance.infiltrates.Add(infiltrated);
        }

        internal static void ClassDSpawn(Player player)
        {

            player.Ammo[(int)AmmoType.Nato556] = 250;
            player.Ammo[(int)AmmoType.Nato762] = 250;
            player.Ammo[(int)AmmoType.Nato9] = 250;
            player.MaxHealth = Infiltrated.Instance.Config.HealthAmount;
            player.Health = Infiltrated.Instance.Config.HealthAmount;
            if (Infiltrated.Instance.Config.InfiltratedItems.Count < 9)
            {
                player.ResetInventory(Infiltrated.Instance.Config.InfiltratedItems);

            }
            else
            {
                Log.Error("Error: You can't give the Infiltrated more than 9 items!");
            }
            player.ClearBroadcasts();
            player.Broadcast(Infiltrated.Instance.Config.InfiltratedBroadcast);

        }

        public void onDied(DiedEventArgs ev)
        {
            if (Infiltrated.Instance.infiltrates.Contains(ev.Target))
            {
                if (Infiltrated.Instance.Config.InfiltratedBroadcastDeath.Show)
                {
                    Map.Broadcast(Infiltrated.Instance.Config.InfiltratedBroadcastDeath.Duration, Infiltrated.Instance.Config.InfiltratedBroadcastDeath.Content.Replace("{player}", ev.Target.Nickname));
                    Infiltrated.Instance.infiltrates.Remove(ev.Target);
                }
            }
        }

        internal static void Kill(Player player)
        {
            player.ClearInventory();
            player.SetRole(RoleType.Spectator);
            Infiltrated.Instance.infiltrates.Remove(player);
        }

    }
}
