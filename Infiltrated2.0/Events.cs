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
        private readonly Logic logic;
        public Events(Infiltrated plugin) => this.plugin = plugin;
        public Events(Logic logic) => this.logic = logic;

        public void OnRoundStart()
        {
            plugin.TrackedPlayers.Clear();
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
            Timing.CallDelayed(plugin.Config.ClassDSpawnTime, () => logic.ChooseClassD());
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
