using Exiled.API.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;
namespace Infiltrated
{
    public sealed class Config : IConfig
    {
        [Description("Enable or Disable the plugin")]
        public bool IsEnabled { get; set; } = true;

        [Description("The items Infiltrated spawn with.")]
        public List<ItemType> InfiltratedItems { get; private set; } = new List<ItemType>() { ItemType.KeycardScientistMajor, ItemType.GrenadeFlash, ItemType.Adrenaline, ItemType.Disarmer };

        [Description("The broadcast to all ClassD")]
        public Exiled.API.Features.Broadcast ClassDBroadcast { get; set; } = new Exiled.API.Features.Broadcast("<size=30><color=aqua>In {seconds} seconds a classD will be chosen to become an</color> <color=red>Infiltrated ClassD</color></size>", 10);

        [Description("How soon to spawn Infiltrated ClassD")]
        public float ClassDSpawnTime { get; set; } = 30.0f;

        [Description("The broadcast to Infiltrated ClassD")]
        public Exiled.API.Features.Broadcast InfiltratedBroadcast { get; private set; } = new Exiled.API.Features.Broadcast("<size=30><color=aqua>You have become a</color> <color=red>classD Infiltrated</color>\n <color=aqua>Help the other ClassDs escape the facility!</color></size>", 10);

        [Description("Healt amount of Infiltrated ClassD")]
        public int HealthAmount { get; private set; } = 150;

        [Description("The broadcast when Infiltrated death")]
        public Exiled.API.Features.Broadcast InfiltratedBroadcastDeath { get; private set; } = new Exiled.API.Features.Broadcast("<color=aqua>The infiltrated <color=red>{player}</color> has been killed!</color>", 10);

        [Description("Enable or disable the Database")]
        public bool IsDatabaseEnaled { get; private set; } = true;

        [Description("The name of the database")]
        public string DatabaseName { get; private set; } = "Infiltrated";

    }
}
