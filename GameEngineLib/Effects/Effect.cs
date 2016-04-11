using GameEngine.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Effects {
    public class Effect {
        public int Id { get; private set; }
        public EffectType TypeCode { get; private set; }
        public EffectClass ClassCode { get; private set; }
        public float EndTime { get; private set; }
        public float StartTime { get; private set; }
        public StatModifier Modifier { get; private set; }

        public Effect(EffectType typeCode, EffectClass classCode, float startTime, float endTime) {
            this.Id = GlobalLookup.IDs.Next();
            this.TypeCode = typeCode;
            this.ClassCode = classCode;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.Modifier = new StatModifier();
        }
    }
}
