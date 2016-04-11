using GameEngine.Effects;
using GameEngine.Entities;
using GameEngine.Factories;
using GameEngine.Items;
using GameEngine.Worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Global {
    public class FactoriesProvider {
        /// <summary>
        /// Factory to produce Items
        /// </summary>
        public Factory<ItemType, Item, ItemProfile> Items { get; private set; }

        /// <summary>
        /// Factory to produce Entities
        /// </summary>
        public Factory<EntityType, Entity, EntityProfile> Entities { get; private set; }

        /// <summary>
        /// Factory to produce Maps
        /// </summary>
        public Factory<MapType, Map, MapProfile> Maps { get; private set; }

        /// <summary>
        /// Factory to produce Entity Effects
        /// </summary>
        public Factory<EffectType, Effect, EffectProfile> Effects { get; private set; }

        public FactoriesProvider() {
            this.Items = new Factory<ItemType, Item, ItemProfile>();
            this.Entities = new Factory<EntityType, Entity, EntityProfile>();
            this.Maps = new Factory<MapType, Map, MapProfile>();
            this.Effects = new Factory<EffectType, Effect, EffectProfile>();
        }
    }
}
