using GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Global {
    public struct SkillStatInfo {
        public SkillStatInfoFunction Function;
        public float BaseValue;
        public float ModifierValue;
        public SkillStatInfo(SkillStatInfoFunction function, float baseValue, float modifierValue) {
            Function = function;
            BaseValue = baseValue;
            ModifierValue = modifierValue;
        }
    }
}
