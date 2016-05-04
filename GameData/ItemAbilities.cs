using System;

namespace GameData {
    [Flags]
    public enum ItemAbilities : int {
        ModifyItemAbilities = 1,
        All = Int32.MaxValue,
        None = 0
    }
}
