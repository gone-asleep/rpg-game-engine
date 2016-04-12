using GameEngine.Effects;
using GameEngine.Entities.Stats;
using GameEngine.Factories;
using GameEngine.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEntities.Effects {
    public static class EffectInformation {
        private static bool isLoaded = false;

        public static readonly Func<EffectProfile, Effect> StrengthenEffectConstructor = (profile) => {
            float seconds = 30.0f;
            Effect effect = new Effect(EffectType.Strengthen, EffectClass.EntityStatusEffect ,GlobalLookup.Time.Current ,GlobalLookup.Time.Current + seconds);
            effect.Modifier.Define(StatType.Strength, StatModifierType.Multiply, 1.5f); // +50%
            return effect;
        };

        public static readonly Func<EffectProfile, Effect> WeakenEffectConstructor = (profile) => {
            float seconds = 30.0f;
            Effect effect = new Effect(EffectType.Weaken, EffectClass.EntityStatusEffect, GlobalLookup.Time.Current, GlobalLookup.Time.Current + seconds);
            effect.Modifier.Define(StatType.Strength, StatModifierType.Multiply, 0.5f); //-50%
            return effect;
        };

        public static void Load() {
            if (!isLoaded) {
                GlobalLookup.Factories.Effects.Add(EffectType.Strengthen, StrengthenEffectConstructor);
                GlobalLookup.Factories.Effects.Add(EffectType.Weaken, WeakenEffectConstructor);
                isLoaded = true;
            }
        }
    }
}
