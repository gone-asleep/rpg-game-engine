using GameEngine.Effects;
using GameEngine.Entities;
using GameEngine.Entities.Stats;
using GameEngine.Global;
using GameEngine.Items;
using GameEngine.Worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine {
    class Program {

        static void Main(string[] args) {
            GameEntities.Effects.EffectInformation.Load();
            GameEntities.Entities.Human.Load();
            GameEntities.Items.Weapons.Load();
            GameEntities.Maps.TestMaps.Load();
            GameEntities.Entities.Player.Load();

            for (float i = 0.001f; i < 1.0; i+= 0.01f) {

                Console.WriteLine(new string('*', (int)(10 * Extensions.NextGaussian(i, .5, 1))));
            }
            Console.ReadKey();
        }
    }
}
