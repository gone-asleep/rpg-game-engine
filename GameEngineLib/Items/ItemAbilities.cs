using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Items {
    [Flags]
    public enum ItemAbilities : int {
        ModifyItemAbilities = 1,
        All = Int32.MaxValue,
        None = 0
    }
}
