using GameEngine.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Factories {
    public struct ItemProfile {
        public int Level;
        public ItemType Type;

        public ItemProfile(int level, ItemType type) {
            Level = level;
            Type = type;
        }
    }
}
