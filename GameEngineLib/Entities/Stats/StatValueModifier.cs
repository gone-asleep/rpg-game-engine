using GameData;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Entities.Stats {
    [ProtoContract]
    public struct StatValueModifier {
        private static StatValueModifier None {
            get { return new StatValueModifier(StatModifierOperation.None, 0); }
        }
        [ProtoMember(1)]
        public float Value;
        [ProtoMember(2)]
        public StatModifierOperation Operation;

   

        public StatValueModifier(StatModifierOperation op, float value) {
            Value = value;
            Operation = op;
        }
        public override string ToString() {
            if (this.Operation == StatModifierOperation.Add) {
                return "+" + Value;
            } else {
                return Value * 100 + "%";
            }

        }
    }
}
