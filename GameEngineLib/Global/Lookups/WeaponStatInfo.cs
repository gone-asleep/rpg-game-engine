using GameEngine.Entities.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Global {
    public struct ItemAppliedSkillStatInfo {
        public WeaponStatCalculationFunction Function;
        public SkillType appliedSkill;
        public string name;
        public float attackModifier;
        public float attackDamageBase;
        public float attackDamageRange;
        public float attackSpeedBase;
    }
}
