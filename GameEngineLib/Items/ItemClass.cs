using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Items {
    public enum ItemClassCode : long {
        Potion = 1,
        Weapon = 2,
        Food = 4,
        Bait = 8,
        Tool = 16,
        Armor = 32,
        Clothing = 64,
        Currency = 128
    }
}
