using Exiled.API.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace Infiltrated
{
    public class Config : IConfig
    {
        [Description("Enable or Disable the plugin")]
        public bool IsEnabled { get; set; } = true;


        [Description("The items Infiltrated spawn with.")]
        public List<int> InfiltratedItem { get; private set; } = new List<int>() { 21, 26, 12, 14, 10 };


        [Description("The duration of the classd spawn broadcast")]
        public ushort ClassDAnnounceDuration { get; private set; } = 10;


        [Description("The message to broadcast to all ClassD")]
        public string ClassDAnnounceMessage { get; private set; } = "<size=30><color=aqua>In {seconds} seconds a classD will be chosen to become an</color> <color=red>Infiltrated ClassD</color></size>";


        [Description("How soon to spawn Infiltrated ClassD")]
        public float ClassDSpawnTime { get; private set; } = 30.0f;


        [Description("The message to broadcast to Infiltrated ClassD")]
        public string InfiltratedBroadcastMessage { get; private set; } = "<size=30><color=aqua>You have become a</color> <color=red>classD Infiltrated</color>\n <color=aqua>Help the other ClassDs escape the facility!</color></size>";


        [Description("The duration of the Infiltrated ClassD broadcast")]
        public ushort InfiltratedBroadCastDuration { get; private set; } = 10;


        [Description("Healt amount of Infiltrated ClassD")]
        public int HealtAmount { get; private set; } = 150;

        [Description("Enable or disable death broadcast of Infiltrated")]
        public bool IsEnabledDeathAnnounceInfiltrated { get; private set; } = true;


        [Description("The message to broadcast when Infiltrated death")]
        public string InfiltratedBroadcastDeath { get; private set; } = "<color=aqua>The infiltrated <color=red>{player}</color> was killed!</color>";


        [Description("The duration of the Infiltrated death broadcast")]
        public ushort InfiltratedDeathAnnounceDuration { get; private set; } = 10;
    }
}
