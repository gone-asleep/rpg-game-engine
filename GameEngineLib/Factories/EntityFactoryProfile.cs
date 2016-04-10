using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Factories {
    public struct EntityFactoryTypeProfile {
        public int Level;

        public EntityFactoryTypeProfile(int level) {
            Level = level;
        }
    }
}
