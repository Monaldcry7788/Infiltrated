using System;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using Scp035;
using UnityEngine;

namespace Infiltrated
{
    public class InfiltratedComponent : MonoBehaviour
    {
        private Exiled.API.Features.Player player;

        private void Awake()
        {
            RegisterEvents();
            player = Exiled.API.Features.Player.Get(gameObject);
        }

        private void Start()
        {
            ClassDSpawn();
        }

        public void OnDied(DiedEventArgs ev)
        {
            if (ev.Target != player || ev.Killer != player)
                return;
            if (Infiltrated.Singleton.Config.InfiltratedBroadcastDeath.Show &&
                Infiltrated.Singleton.TrackedPlayers.Contains(ev.Target))
            {
                Map.Broadcast(Infiltrated.Singleton.Config.InfiltratedBroadcastDeath.Duration,
                    Infiltrated.Singleton.Config.InfiltratedBroadcastDeath.Content.Replace("{player}",
                        ev.Target.Nickname));
                Infiltrated.Singleton.TrackedPlayers.Remove(ev.Target);
                ev.Target.GetPlayerDB().TotalDeath++;
                Database.LiteDatabase.GetCollection<Player>().Update(Database.PlayerData[ev.Target]);
                Destroy();
                return;
            }

            if (Infiltrated.Singleton.TrackedPlayers.Contains(ev.Killer))
            {
                ev.Killer.GetPlayerDB().TotalKill++;
                Database.LiteDatabase.GetCollection<Player>().Update(Database.PlayerData[ev.Killer]);
            }
        }

        public void OnShoot(ShootingEventArgs ev)
        {
            if (ev.Shooter != player) return;
            if (Infiltrated.Singleton.TrackedPlayers.Contains(ev.Shooter))
            {
                ev.Shooter.GetPlayerDB().TotalShotsFired++;
                Database.LiteDatabase.GetCollection<Player>().Update(Database.PlayerData[ev.Shooter]);
            }
        }

        public void ClassDSpawn()
        {
            player.Ammo[ItemType.Ammo9x19] = 250;
            player.Ammo[ItemType.Ammo12gauge] = 250;
            player.Ammo[ItemType.Ammo44cal] = 250;
            player.Ammo[ItemType.Ammo556x45] = 250;
            player.Ammo[ItemType.Ammo762x39] = 250;

            player.Health = player.MaxHealth = Infiltrated.Singleton.Config.HealthAmount;
            player.ResetInventory(Infiltrated.Singleton.Config.InfiltratedItems);
            player.ClearBroadcasts();
            player.Broadcast(Infiltrated.Singleton.Config.InfiltratedBroadcast);
            player.GetPlayerDB().TotalRoundPlayed++;
            Database.LiteDatabase.GetCollection<Player>().Update(Database.PlayerData[player]);
        }

        public void Kill()
        {
            player.SetRole(RoleType.Spectator);
            Infiltrated.Singleton.TrackedPlayers.Remove(player);
        }

        private void Update()
        {
            if (player == null)
            {
                Destroy();
                return;
            }
        }

        private void OnDestroy()
        {
            PartiallyDestroy();
        }

        public void PartiallyDestroy()
        {
            UnRegisterEvents();
            if (player == null)
                return;
        }

        public void Destroy()
        {
            try
            {
                Destroy(this);
            }
            catch (Exception exception)
            {
                Log.Error($"Error, cannot destroy: {exception}");
            }
        }

        public void RegisterEvents()
        {
            Exiled.Events.Handlers.Player.Died += OnDied;
            Exiled.Events.Handlers.Player.Shooting += OnShoot;
        }

        public void UnRegisterEvents()
        {
            Exiled.Events.Handlers.Player.Died -= OnDied;
            Exiled.Events.Handlers.Player.Shooting -= OnShoot;
        }
    }
}