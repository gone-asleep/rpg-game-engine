using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Factories {
    public struct ItemFactoryTypeProfile {
        public int Level;

        public ItemFactoryTypeProfile(int level) {
            Level = level;
        }
    }
}
