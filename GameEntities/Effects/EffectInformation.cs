using GameEngine;
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
    //public static class EffectInformation {
    //    private static readonly IEffectInfo[] BaseInfo = new IEffectInfo[] { 
    //        new EffectInfo(EffectType.Strengthen, EffectClass.EntityStatusEffect, "Strengthen"),
    //        new EffectInfo(EffectType.Strengthen, EffectClass.EntityStatusEffect, "Weaken"),
    //    };

    //    private static bool isLoaded = false;

    //    public static readonly Func<EffectProfile, Effect> StrengthenEffectConstructor = (profile) => {
    //        float seconds = 30.0f;
    //        IStatModifier statModifier = new StatModifier();
    //        Effect effect = new Effect(BaseInfo[0], statModifier, seconds);
    //        return effect;
    //    };

    //    public static readonly Func<EffectProfile, Effect> WeakenEffectConstructor = (profile) => {
    //        float seconds = 30.0f;
    //        IStatModifier statModifier = new StatModifier();
    //        Effect effect = new Effect(BaseInfo[1], statModifier, seconds);
    //        return effect;
    //    };

    //    public static void Load() {
    //        if (!isLoaded) {
    //            GameGlobal.Factories.Effects.Add(EffectType.Strengthen, StrengthenEffectConstructor);
    //            GameGlobal.Factories.Effects.Add(EffectType.Weaken, WeakenEffectConstructor);
    //            isLoaded = true;
    //        }
    //    }
    //}
}
