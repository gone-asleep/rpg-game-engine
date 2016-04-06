﻿using GameEngine.Worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Factories {

    public class MapFactory {
        Dictionary<MapTypeCode, Func<MapFactoryTypeProfile, Map>> Factories { get; set; }

        public MapFactory() {
            this.Factories = new Dictionary<MapTypeCode, Func<MapFactoryTypeProfile, Map>>();
        }

        public void AddFactoryConstructor(MapTypeCode code, Func<MapFactoryTypeProfile, Map> func) {
            this.Factories.Add(code, func);
        }

        public Map Generate(MapTypeCode typeCode, MapFactoryTypeProfile profile = new MapFactoryTypeProfile()) {
            if (this.Factories.ContainsKey(typeCode)) {
                var entity = Factories[typeCode](profile);
                return entity;
            }
            return null;
        }
    }

    public struct MapFactoryTypeProfile {
        public int Level;

        public MapFactoryTypeProfile(int level) {
            Level = level;
        }
    }
}
