using GameData.Info;
using GameEngine.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Effects {
    public class Effect {
        public IEffectInfo Info { get; private set; }
        public float EndTime { get; private set; }
        public float StartTime { get; private set; }
        public StatModifier Modifier { get; private set; }
        public float Duration { get; private set; }
        public Effect(IEffectInfo info, IStatModifier modifier, float duration) {
            this.Info = info;
            this.Modifier = new StatModifier();
            this.Duration = duration;
        }
    }
}
