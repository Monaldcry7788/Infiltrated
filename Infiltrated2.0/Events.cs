using System.Linq;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;

namespace Infiltrated
{
    public class Events
    {
        public System.Random Rand = new System.Random();
        private readonly Infiltrated plugin;

        public Events(Infiltrated plugin)
        {
            this.plugin = plugin;
        }

        internal void OnPlayerVerify(VerifiedEventArgs ev)
        {
            if (!Database.LiteDatabase.GetCollection<Player>().Exists(p => p.Id == PlayerDB.GetRawUserId(ev.Player)))
            {
                Log.Info($"Player: {ev.Player.Nickname} ({ev.Player.UserId}) successfully added");
                plugin.PlayerDataDB.CreatePlayer(ev.Player);
            }

            var playerDB = ev.Player.GetPlayerDB();
            if (Database.PlayerData.ContainsKey(ev.Player)) return;
            Database.PlayerData.Add(ev.Player, playerDB);
            playerDB.Name = ev.Player.Nickname;
        }

        public void OnRoundStart()
        {
            Timing.CallDelayed(0.5f, () =>
            {
                foreach (var player in Exiled.API.Features.Player.List.Where(x => x.Role == RoleType.ClassD))
                    player.Broadcast(Infiltrated.Singleton.Config.ClassDBroadcast.Duration, Infiltrated.Singleton.Config.ClassDBroadcast.Content.Replace("{seconds}", Infiltrated.Singleton.Config.ClassDSpawnTime.ToString()));
                });
            plugin.TrackedPlayers.Clear();
            Timing.CallDelayed(plugin.Config.ClassDSpawnTime, () => plugin.Logic.ChooseClassD());
        }
    }
}