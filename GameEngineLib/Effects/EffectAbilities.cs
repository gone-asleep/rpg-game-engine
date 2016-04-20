using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Effects {
    [Flags]
    public enum EffectAbilities : long {
        ModifyMagicAbilities = 1,
        All = Int64.MaxValue,
        None = 0
    }
}
