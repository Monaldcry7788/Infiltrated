using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;
namespace Infiltrated
{
    public class Events
    {
        public System.Random Rand = new System.Random();
        private readonly Infiltrated plugin;
        public Events(Infiltrated plugin) => this.plugin = plugin;

        internal void OnPlayerVerify(VerifiedEventArgs ev)
        {
            if (plugin.Config.IsDatabaseEnaled && !Database.LiteDatabase.GetCollection<Player>().Exists(p => p.Id == PlayerDB.GetRawUserId(ev.Player)))
            {
                Log.Info($"Player: {ev.Player.Nickname} ({ev.Player.UserId}) successfully added");
                plugin.PlayerDataDB.CreatePlayer(ev.Player);
            }
            if (plugin.Config.IsDatabaseEnaled)
            {
                var playerDB = ev.Player.GetPlayerDB();
                if (Database.PlayerData.ContainsKey(ev.Player)) return;
                Database.PlayerData.Add(ev.Player, playerDB);
                playerDB.Name = ev.Player.Nickname;
            }
        }
        public void OnRoundStart()
        {
            plugin.TrackedPlayers.Clear();
            foreach (Exiled.API.Features.Player player in Exiled.API.Features.Player.List)
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
