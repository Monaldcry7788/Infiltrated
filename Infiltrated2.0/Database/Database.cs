using Exiled.API.Features;
using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;

namespace Infiltrated
{
    public class Database
    {
        public static LiteDatabase LiteDatabase { get; private set; }
        public static string Folder => Path.Combine(Exiled.API.Features.Paths.Plugins, Infiltrated.Singleton.Config.DatabaseName);
        public static string FullDir => Path.Combine(Folder, $"{Infiltrated.Singleton.Config.DatabaseName}.db");

        public static Dictionary<Exiled.API.Features.Player, Player> PlayerData = new Dictionary<Exiled.API.Features.Player, Player>();
        private readonly Infiltrated plugin;
        public Database(Infiltrated plugin) => this.plugin = plugin;

        public void CreateDatabase()
        {
            if (Directory.Exists(Folder)) return;
            try
            {
                Directory.CreateDirectory(Folder);
                Log.Warn("I cannot found database, I'm creating one");
            }
            catch (Exception e)
            {
                Log.Error($"Error creating database: {e}");
            }
        }

        public void Open()
        {
            try
            {
                LiteDatabase = new LiteDatabase(FullDir);
                LiteDatabase.GetCollection<Player>().EnsureIndex(p => p.Id);
                LiteDatabase.GetCollection<Player>().EnsureIndex(p => p.Name);
                LiteDatabase.GetCollection<Player>().EnsureIndex(p => p.TotalRoundPlayed);
            }
            catch (Exception e)
            {
                Log.Error($"Failed to open the database: {e}");
            }
        }

        public void CreatePlayer(Exiled.API.Features.Player player)
        {
            try
            {
                if (LiteDatabase.GetCollection<Player>().Exists(p => p.Id == PlayerDB.GetRawUserId(player))) return;
                LiteDatabase.GetCollection<Player>().Insert(new Player()
                {
                    Id = PlayerDB.GetRawUserId(player),
                    Name = player.Nickname,
                    Auth = PlayerDB.GetAuth(player),
                    TotalRoundPlayed = 0
                });
            }
            catch (Exception e)
            {
                Log.Error($"I can't add player {player.Nickname} into database:\n {e}");
            }
        }
    }
}
