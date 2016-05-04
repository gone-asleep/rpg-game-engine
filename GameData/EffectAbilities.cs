using System;

namespace GameData {
    [Flags]
    public enum EffectAbilities : long {
        ModifyMagicAbilities = 1,
        All = Int64.MaxValue,
        None = 0
    }
}
