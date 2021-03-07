using System.Collections.Generic;

namespace Infiltrated.API
{
    public static class InfiltratedAPI
    {
        public static List<Exiled.API.Features.Player> GetInfiltratedList = Infiltrated.Singleton.TrackedPlayers;
        public static void SpawnInfiltrated(Exiled.API.Features.Player player)
        {
            Infiltrated.Singleton.Logic.ClassDSpawn(player);
        }
        public static void KillInfiltrated(Exiled.API.Features.Player player)
        {
            Infiltrated.Singleton.Logic.Kill(player);
        }
    }
}