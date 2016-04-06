using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine {
    interface IConstruct {
        string Name { get; }
        ConstructType ConstructType { get; }
        void Referesh(int time);
    }
}
