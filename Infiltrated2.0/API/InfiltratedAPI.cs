using System.Collections.Generic;

namespace Infiltrated.API
{
    public class InfiltratedAPI
    {
        public List<Exiled.API.Features.Player> GetInfiltratedList = Infiltrated.Singleton.TrackedPlayers;

        public static void SpawnInfiltrated(Exiled.API.Features.Player player)
        {
            if (player.GameObject.TryGetComponent(out InfiltratedComponent component)) component.Destroy();

            player.GameObject.AddComponent<InfiltratedComponent>();
        }

        public void KillInfiltrated(Exiled.API.Features.Player player)
        {
            if (player.GameObject.TryGetComponent(out InfiltratedComponent component)) component.Destroy();
        }
    }
}