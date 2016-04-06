using GameEngine.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Factories {
    public class EffectFactory {
        Dictionary<EffectTypeCode, Func<EffectFactoryTypeProfile, Effect>> Factories { get; set; }

        public EffectFactory() {
            this.Factories = new Dictionary<EffectTypeCode, Func<EffectFactoryTypeProfile, Effect>>();
        }

        public void AddFactoryConstructor(EffectTypeCode code, Func<EffectFactoryTypeProfile, Effect> func) {
            this.Factories.Add(code, func);
        }

        public Effect Generate(EffectTypeCode typeCode, EffectFactoryTypeProfile profile = new EffectFactoryTypeProfile()) {
            if (this.Factories.ContainsKey(typeCode)) {
                var entity = Factories[typeCode](profile);
                return entity;
            }
            return null;
        }
    }

    public struct EffectFactoryTypeProfile {
        public int Level;

        public EffectFactoryTypeProfile(int level) {
            Level = level;
        }
    }
}
