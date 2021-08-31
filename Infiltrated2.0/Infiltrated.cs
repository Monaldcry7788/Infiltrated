using Exiled.API.Features;
using Exiled.Loader;
using System;
using System.Collections.Generic;
using Infiltrated.API;
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
        public InfiltratedAPI api;
        public static Infiltrated Singleton;
        //public static bool isSH = false;
        //public static bool is035 = false;
        public override string Author { get; } = "Twitch.tv/Monaldcry7788#9248";
        public override string Name { get; } = "Infiltrated";
        public override Version Version { get; } = new Version(5, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(3, 0, 0);
        public Player Player { get; private set; }
        public Database PlayerDataDB { get; private set; }

        public override void OnEnabled()
        {
            Singleton = this;
            Events = new Events(this);
            Logic = new Logic(this);
            api = new InfiltratedAPI();
            ServerEvent.RoundStarted += Events.OnRoundStart;
            PlayerEvent.Verified += Events.OnPlayerVerify;
            //Log.Debug("Checking for SCP035 or SerpentsHand plugins", Config.IsDebugEnabled);
            //Check035();
            //CheckSH();
            db = new Database(this);
            PlayerDataDB = new Database(this);
            PlayerDataDB.CreateDatabase();
            PlayerDataDB.Open();
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            ServerEvent.RoundStarted -= Events.OnRoundStart;
            PlayerEvent.Verified -= Events.OnPlayerVerify;
            Events = null;
            Singleton = null;
            Logic = null;
            api = null;
            //is035 = false;
            //isSH = false;
            db = null;
            PlayerDataDB = null;
            Database.LiteDatabase.Dispose();
            base.OnDisabled();
        }

        /*internal void Check035()
        {
            foreach (var plugin in Loader.Plugins)
                if (plugin.Name == "scp035")
                {
                    is035 = true;
                    Log.Debug("SCP035 found!", Config.IsDebugEnabled);
                    return;
                }

            Log.Debug("SCP035 plugin not found, skipping process", Config.IsDebugEnabled);
        }

        internal void CheckSH()
        {
            foreach (var plugin in Loader.Plugins)
                if (plugin.Name == "SerpentsHand")
                {
                    isSH = true;
                    Log.Debug("SH found!", Config.IsDebugEnabled);
                    return;
                }

            Log.Debug("SerpentsHand plugin not found, skipping process", Config.IsDebugEnabled);
        }
        */
    }
}