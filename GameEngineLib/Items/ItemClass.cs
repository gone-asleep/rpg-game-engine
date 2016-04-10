using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Items {
    public enum ItemClassCode : long {
        Stackable = 1 >> 63,
        Potion = 1 | Stackable,
        Weapon = 2,
    }
}
