using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Global {
    public class IDProvider {
        private int nextID = 0;
        public Guid Next() {
            return Guid.NewGuid();
        }
    }
}
