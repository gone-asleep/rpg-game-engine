using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Entities.Stats {
    public struct StatValueModifier {
        private static StatValueModifier None {
            get { return new StatValueModifier(StatModifierOperation.None, 0); }
        }

        public float Value;
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
