using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Areas {
    public interface IArea {
        AreaCode Code { get; }
    }

    public class Area : IArea {
        public Guid ID { get; private set; }
        public AreaCode Code { get; private set; }
        public List<IArea> SubAreas { get; private set; }

        public Area(AreaCode code) {
            this.Code = code;
            this.SubAreas = new List<IArea>();
        }
    }
}
