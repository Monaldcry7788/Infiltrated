

**Infiltrated Plugin**<br />

Welcome to Infiltrated plugin!

This is the list of Infiltrated features::

- **Broadcast to all ClassDs:** You can set in server config the duration and message of the broadcast, and you can use variable **{seconds}** for wow soon to spawn Infiltrated ClassD
- **Broadcast to Infiltrated:** You can set in server config the message and duration of the broadcast to display only to Infiltrated.
- **Items:** You can set in server config the items Infiltrated spawn with.
- **Health amount:** You can set in server config the health amount of Infiltrated.
- **Death broadcast:** You can set in server config the broadcast to display when Infiltrated dead.
- **Spawn Command:** You can spawn infiltrated using command.
- **Kill Command:** You can kill infiltrated using command.
- **List command:** You can view list of alive infiltrated.


### To do:

Add Multi language support

### Config

You can see settings and edit them inside your Exiled config.

**Config**

| Name  | Type | Description | 
| ------------- | ------------- | ------------- |
| IsEnabled  | bool  | Enable or Disable the plugin |
| InfiltratedItem  | int  | The items Infiltrated spawn with |
| ClassDAnnounceDuration  | ushort | The duration of the ClassD spawn broadcast |
| ClassDAnnounceMessage  | string  | The message to broadcast to all ClassD |
| ClassDSpawnTime  | float  | How soon to spawn Infiltrated after round start  |
| InfiltratedBroadcastMessage  | string  | The message to broadcast to Infiltrated ClassD  |
| InfiltratedBroadCastDuration  | ushort  | The duration of the Infiltrated ClassD broadcast  |
| HealtAmount  | int | Healt amount of Infiltrated ClassD  |
| IsEnabledDeathAnnounceInfiltrated  | bool | Enable or disable death broadcast of Infiltrated Enable or disable death broadcast of Infiltrated |
| InfiltratedBroadcastDeath  | string | The message to broadcast when Infiltrated death |
| InfiltratedDeathAnnounceDuration | ushort | The duration of the Infiltrated death broadcast |

**Commands**

| Commands  | Args | Permission | Description | 
| ------------- | ------------- | ------------- | ------------- |
| Infiltrated_spawn / ISpawn  | <player name>/<player id>  | infiltrated.spawn | Spawn Infiltrated |
| Infiltrated_list / IList  | none  | infiltrated.list | Show the list of alive infiltrated |
| Infiltrated_kill / IKill  | <player name>/<player id> | infiltrated.kill | Kill alive infiltrated |

If you found bug please contact me on discord: **Twitch.tv/Monaldcry7788#9248** .<br /><br />
