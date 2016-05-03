using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Areas {
    public interface IArea {
        AreaCode Code { get; }
        List<IAreaConnector> Connections { get; }
    }

    public class Area : IArea {
        public Guid ID { get; private set; }
        public AreaCode Code { get; private set; }
        public List<IAreaConnector> Connections { get; private set; }
        public string Name {get; private set; }

        public Area(AreaCode code, string name = null) {
            this.Code = code;
            this.Connections = new List<IAreaConnector>();
            this.Name = name;
        }
    } 
}
