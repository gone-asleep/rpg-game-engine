using GameEngine.Factories;
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
        public ItemFactory Items { get; private set; }

        /// <summary>
        /// Factory to produce Entities
        /// </summary>
        public EntityFactory Entities { get; private set; }

        /// <summary>
        /// Factory to produce Maps
        /// </summary>
        public MapFactory Maps { get; private set; }

        /// <summary>
        /// Factory to produce Entity Effects
        /// </summary>
        public EffectFactory Effects { get; private set; }

        public FactoriesProvider() {
            this.Items = new ItemFactory();
            this.Entities = new EntityFactory();
            this.Maps = new MapFactory();
            this.Effects = new EffectFactory();
        }
    }
}
