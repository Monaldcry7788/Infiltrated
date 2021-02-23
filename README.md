

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
| InfiltratedItems  | int  | The items Infiltrated spawn with |
| ClassDBroadcast  | Broadcast  | The broadcast to all ClassD |
| ClassDSpawnTime  | float  | How soon to spawn Infiltrated after round start  |
| InfiltratedBroadcast  | Broadcast  | The broadcast to Infiltrated ClassD  |
| HealthAmount  | int | Healt amount of Infiltrated ClassD  |
| InfiltratedBroadcastDeath  | Broadcast | The broadcast when Infiltrated death |

**Commands**

| Commands  | Args | Permission | Description | 
| ------------- | ------------- | ------------- | ------------- |
| Infiltrated_spawn / ISpawn  | Player name or ID  | infiltrated.spawn | Spawn Infiltrated |
| Infiltrated_list / IList  | none  | infiltrated.list | Show the list of alive infiltrated |
| Infiltrated_kill / IKill  | Player name or ID | infiltrated.kill | Kill alive infiltrated |

If you found bug please contact me on discord: **Twitch.tv/Monaldcry7788#9248** .<br /><br />
