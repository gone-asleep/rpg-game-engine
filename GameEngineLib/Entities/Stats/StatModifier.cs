using GameEngine.Entities.Stats;
using GameEngine.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine {
    public class StatModifier {
        public struct StatValue {
            public float Value;
            public StatModifierType Operation;
            public StatValue(float value, StatModifierType op) {
                Value = value;
                Operation = op;
            }
            public override string ToString() {
                if (this.Operation == StatModifierType.Add) {
                    return "+" + Value;
                } else {
                    return Value * 100 + "%";
                }

            }
        }

        private StatValue[] Stats { get; set; }
        
        public int Id { get; private set; }

        public StatValue this[int i] {
            get { return Stats[i]; }
            set { Stats[i] = value; }
        }
        
        public StatValue this[StatType i] {
            get { return Stats[(int)i]; }
            set { Stats[(int)i] = value; }
        }

        public void Define(StatType type, StatModifierType op, float value) {
            this[(int)type] = new StatValue(value, op);
        }

        public StatModifier() {
            this.Id = GlobalLookup.IDs.Next();
            this.Stats = new StatValue[Enum.GetValues(typeof(StatType)).Length];
        }

        public override string ToString() {
            string debugStr = "";
            foreach (var name in Enum.GetNames(typeof(StatType))) {
                StatType type = (StatType)Enum.Parse(typeof(StatType), name);
                if (this[type].Operation != StatModifierType.None) {
                    debugStr += name + ":" + this[type].ToString() + ",  ";
                }
            }
            return debugStr;
        }
    }    
}
