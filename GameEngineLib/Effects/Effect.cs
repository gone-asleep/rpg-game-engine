using GameEngine.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Effects {
    public class Effect {
        public int Id { get; private set; }
        public EffectTypeCode TypeCode { get; private set; }
        public EffectClassCode ClassCode { get; private set; }
        public float EndTime { get; private set; }
        public float StartTime { get; private set; }
        public StatModifier Modifier { get; private set; }

        public Effect(EffectTypeCode typeCode, EffectClassCode classCode, float startTime, float endTime) {
            this.Id = GlobalLookup.GetNextID();
            this.TypeCode = typeCode;
            this.ClassCode = classCode;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.Modifier = new StatModifier();
        }
    }
}
