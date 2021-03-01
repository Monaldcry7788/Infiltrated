using Exiled.API.Features;

namespace Infiltrated
{
    public class Functions
    {
        private readonly Infiltrated plugin;
        public Functions(Infiltrated plugin) => this.plugin = plugin;

        public void Save(Exiled.API.Features.Player p)
        {
            if (p != null && Database.PlayerData.ContainsKey(p) && p.Nickname != "Dedicated Server")
            {
                if (Round.IsStarted && plugin.TrackedPlayers.Contains(p))
                {
                    var PlayerDB = p.GetPlayerDB();
                    PlayerDB.TotalRoundPlayed++;
                    Database.LiteDatabase.GetCollection<Player>().Update(PlayerDB);
                }
            }
        }
    }
}
