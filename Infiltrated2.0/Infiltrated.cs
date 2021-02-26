using Exiled.API.Features;
using System;
using System.Collections.Generic;
using PlayerEvent = Exiled.Events.Handlers.Player;
using ServerEvent = Exiled.Events.Handlers.Server;

namespace Infiltrated
{
    public class Infiltrated : Plugin<Config>
    {
        public List<Player> TrackedPlayers = new List<Player>();
        public Events Events;
        public static Infiltrated Singleton;
        public override string Author { get; } = "Twitch.tv/Monaldcry7788#9248";
        public override string Name { get; } = "Infiltrated";
        public override Version Version { get; } = new Version(2, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(2, 1, 35);

        public override void OnEnabled()
        {
            Singleton = this;
            Events = new Events(this);
            ServerEvent.RoundStarted += Events.OnRoundStart;
            PlayerEvent.Died += Events.OnDied;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            ServerEvent.RoundStarted -= Events.OnRoundStart;
            PlayerEvent.Died -= Events.OnDied;
            Events = null;
            Singleton = null;
            base.OnDisabled();
        }
    }
}
