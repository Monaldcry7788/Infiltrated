using Exiled.API.Enums;
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
            plugin.TrackedPlayers.Clear();
            foreach (Player player in Player.List)
            {
                if (player.Role == RoleType.ClassD)
                {
                    player.Broadcast(Infiltrated.Singleton.Config.ClassDBroadcast.Duration, Infiltrated.Singleton.Config.ClassDBroadcast.Content.Replace("{seconds}", Infiltrated.Singleton.Config.ClassDSpawnTime.ToString()));
                }
            }
            Timing.CallDelayed(plugin.Config.ClassDSpawnTime, () => plugin.Logic.ChooseClassD());
        }
        public void OnDied(DiedEventArgs ev)
        {
            if (plugin.TrackedPlayers.Contains(ev.Target) && Infiltrated.Singleton.Config.InfiltratedBroadcastDeath.Show)
            {
                Map.Broadcast(Infiltrated.Singleton.Config.InfiltratedBroadcastDeath.Duration, Infiltrated.Singleton.Config.InfiltratedBroadcastDeath.Content.Replace("{player}", ev.Target.Nickname));
                plugin.TrackedPlayers.Remove(ev.Target);
            }
        }
    }
}
