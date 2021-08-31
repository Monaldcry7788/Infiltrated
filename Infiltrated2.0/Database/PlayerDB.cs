namespace Infiltrated
{
    public static class PlayerDB
    {
        public static string GetAuth(this Exiled.API.Features.Player p)
        {
            return p.UserId.Split('@')[1];
        }

        public static string GetRawUserId(this Exiled.API.Features.Player p)
        {
            return p.UserId.GetRawUserId();
        }

        public static string GetRawUserId(this string p)
        {
            return p.Split('@')[0];
        }

        public static Player GetPlayerDB(this string player)
        {
            return Exiled.API.Features.Player.Get(player)?.GetPlayerDB() ??
                   Database.LiteDatabase.GetCollection<Player>().FindOne(queryPlayer =>
                       queryPlayer.Id == player.GetRawUserId() || queryPlayer.Name == player);
        }

        public static Player GetPlayerDB(this Exiled.API.Features.Player player)
        {
            if (player == null) return null;
            else if (Database.PlayerData.TryGetValue(player, out var databasePlayer)) return databasePlayer;
            else
                return Database.LiteDatabase.GetCollection<Player>()
                    .FindOne(queryPlayer => queryPlayer.Id == player.GetRawUserId());
        }
    }
}