using Exiled.API.Enums;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using PlayerEvent = Exiled.Events.Handlers.Player;
using ServerEvent = Exiled.Events.Handlers.Server;

namespace Infiltrated
{
    public class Infiltrated : Plugin<Config>
    {
        private static readonly Lazy<Infiltrated> LazInstance = new Lazy<Infiltrated>(() => new Infiltrated());
        public static Infiltrated Instance => LazInstance.Value;
        public Random random = new Random();
        public List<int> infiltrated = new List<int>();
        public Events Events;
        public override string Author { get; } = "Twitch.tv/Monaldcry7788#9248";
        public override string Name { get; } = "Infiltrated";
        public override Version Version { get; } = new Version(1, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(2, 1, 29);
        public override PluginPriority Priority { get; } = PluginPriority.Low;

        private Infiltrated()
        {
        }

        public override void OnEnabled()
        {
            if (Config.IsEnabled)
            {

                try
                {
                    base.OnEnabled();
                    Log.Warn("The Infiltrated plugin started successfully");
                }
                catch (Exception e)
                {
                    Log.Error($"Startup failed: {e}");
                }
                Events = new Events(this);
                ServerEvent.RoundStarted += Events.OnRoundStart;
                PlayerEvent.Died += Events.onDied;
                ServerEvent.EndingRound += Events.onRoundEnd;
            }
            else
            {
                Log.Warn("Infiltrated plugin is disabled by server config!");
            }


        }

        public override void OnDisabled()
        {
            base.OnDisabled();
            ServerEvent.RoundStarted -= Events.OnRoundStart;
            PlayerEvent.Died -= Events.onDied;
            ServerEvent.EndingRound -= Events.onRoundEnd;
            Events = null;
        }



    }
}
