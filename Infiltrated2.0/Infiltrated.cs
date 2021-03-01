using Exiled.API.Features;
using System;
using Exiled.Loader;
using System.Collections.Generic;
using PlayerEvent = Exiled.Events.Handlers.Player;
using ServerEvent = Exiled.Events.Handlers.Server;
namespace Infiltrated
{
    public class Infiltrated : Plugin<Config>
    {
        public List<Exiled.API.Features.Player> TrackedPlayers = new List<Exiled.API.Features.Player>();
        public Events Events;
        public Logic Logic;
        public Database db;
        public Functions function;
        public static Infiltrated Singleton;
        public override string Author { get; } = "Twitch.tv/Monaldcry7788#9248";
        public override string Name { get; } = "Infiltrated";
        public override Version Version { get; } = new Version(4, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(2, 1, 35);
        public Player Player { get; private set; }
        public Database PlayerDataDB { get; private set; }

        public override void OnEnabled()
        {
            Singleton = this;
            Events = new Events(this);
            Logic = new Logic(this);
            ServerEvent.RoundStarted += Events.OnRoundStart;
            PlayerEvent.Died += Events.OnDied;
            PlayerEvent.Verified += Events.OnPlayerVerify;
            if (Config.IsDatabaseEnaled)
            {
                db = new Database(this);
                function = new Functions(this);
                PlayerDataDB = new Database(this);
                PlayerDataDB.CreateDatabase();
                PlayerDataDB.Open();
            }
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            ServerEvent.RoundStarted -= Events.OnRoundStart;
            PlayerEvent.Died -= Events.OnDied;
            PlayerEvent.Verified -= Events.OnPlayerVerify;
            Events = null;
            Singleton = null;
            Logic = null;
            if (Config.IsDatabaseEnaled)
            {
                db = null;
                function = null;
                PlayerDataDB = null;
                Database.LiteDatabase.Dispose();
            }
            base.OnDisabled();
        }
    }
}
