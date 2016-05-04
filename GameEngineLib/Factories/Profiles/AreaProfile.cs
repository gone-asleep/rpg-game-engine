using GameData;
using GameEngine.Areas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Factories {
    public struct AreaProfile {
        public AreaCode AreaCode;
        public float Size;
        public IArea Source;

        public AreaProfile(AreaCode areaCode, float size, IArea source) {
            Source = source;
            AreaCode = areaCode;
            Size = size;
        }
    }
}
