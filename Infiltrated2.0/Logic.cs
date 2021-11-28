using Exiled.API.Enums;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infiltrated
{
    public class Logic
    {
        public Random Rand = new Random();
        private int random = new Random().Next(1, 100);
        private readonly Infiltrated plugin;
        //private List<Exiled.API.Features.Player> sh = null;
        //private IEnumerable<Exiled.API.Features.Player> scp035 = null;

        public Logic(Infiltrated plugin)
        {
            this.plugin = plugin;
        }

        public void ChooseClassD()
        {
            if (Exiled.API.Features.Player.List.Count() >= plugin.Config.SpawnMinium)
            {
                if (random <= plugin.Config.SpawnChance)
                {
                    var classd = Exiled.API.Features.Player.List.Where(p => p.Role == RoleType.ClassD && !plugin.TrackedPlayers.Contains(p)).ToList();
                    //var ClassD = Exiled.API.Features.Player.List.Where(classD => classD.Role == RoleType.ClassD && !plugin.TrackedPlayers.Contains(classD) && classD != scp035 && !sh.Contains(classD)).ToList();
                    Log.Debug(classd.Count());
                    if (classd.IsEmpty())
                        Log.Info("I don't found any player to spawn!");

                    var infiltrated = classd[Rand.Next(classd.Count())];
                    plugin.TrackedPlayers.Add(infiltrated);
                    infiltrated.GameObject.AddComponent<InfiltratedComponent>();
                }
                /*if (Infiltrated.is035)
                    scp035 = TryGet035();
                
                if (Infiltrated.isSH)
                    sh = TrySH();
                */
                
            }
        }

        /*private IEnumerable<Exiled.API.Features.Player> TryGet035()
        {
            return Scp035.API.AllScp035;
        }

        private List<Exiled.API.Features.Player> TrySH()
        {
            return SerpentsHand.API.GetSHPlayers();
        }
        */
    }
}
