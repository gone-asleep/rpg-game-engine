using GameEngine.Entities.Stats;
using GameEngine.Factories;
using GameEngine.Global;
using GameEngine.Items;
using GameEngine.Worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEntities.Maps {
    public static class TestMaps {
        private static bool isLoaded = false;

        public static readonly Func<MapProfile, Map> TestMapConstructor = (profile) => {
            var map = new Map(20, 20, 40);
            map.Tiles[5][5] = 3;
            map.SetTiles(0, 0, 3, 3, 1);
            map.SetTiles(3, 2, 1, 6, 1);
            return map;
        };

        public static void Load() {
            if (!isLoaded) {
                GlobalLookup.Factories.Maps.Add(MapType.Test, TestMaps.TestMapConstructor);
                isLoaded = true;
            }
        }
    }
}
