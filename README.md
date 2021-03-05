

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
- **RandomSpawn Command**: You can spawn a random Infiltrated.
- **PlayerInfo Command**: You can view info of player.
- **Help Command**: You can view list and description of commands

### Required: 
- Exiled 2.1.35
- LiteDB 5.0.9.0

### To do:

Add support for: Scp035 and SerpentHand
Add spawn item change

### Config

You can see settings and edit them inside your Exiled config.

**Config**

| Name  | Type | Description | 
| ------------- | ------------- | ------------- |
| IsEnabled  | bool  | Enable or Disable the plugin |
| InfiltratedItems  | List  | The items Infiltrated spawn with |
| ClassDBroadcast  | Broadcast  | The broadcast to all ClassD |
| ClassDSpawnTime  | float  | How soon to spawn Infiltrated after round start  |
| InfiltratedBroadcast  | Broadcast  | The broadcast to Infiltrated ClassD  |
| HealthAmount  | int | Health amount of Infiltrated ClassD  |
| InfiltratedBroadcastDeath  | Broadcast | The broadcast when Infiltrated death |
| IsDatabaseEnaled  | bool | Enable or disable the Database |
| DatabaseName | string | The name of the database |

**Commands**

| Commands  | Args | Permission | Description | 
| ------------- | ------------- | ------------- | ------------- |
| Infiltrated / if  | none  | none | show sub-command |
| Spawn / s  | player name or id  | infiltrated.spawn | Spawn an Infiltrated |
| Kill / k  | Player name or ID | infiltrated.kill | Kill alive infiltrated |
| List / l | none | infiltrated.list | List of alive infiltrated |
| RandomSpawn / RSpawn | none | infiltrated.randomspawn | Spawn a random infiltrated |
| PlayerInfo / pi | Player name or id | infiltrated.playerinfo | Show player info (avariable only if IsDatabaseEnaled is enabled) |
| Help / h | none | infiltrated.help | Show help command |

If you found bug please contact me on discord: **Twitch.tv/Monaldcry7788#9248** .<br /><br />

### Downloads
![img](https://img.shields.io/github/downloads/Monaldcry7788/Infiltrated/total?style=for-the-badge)
