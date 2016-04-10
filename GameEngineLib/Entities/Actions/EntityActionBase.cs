using GameEngine.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine {
    public class EntityActionBase {
        public float StartTime { get; protected set; }
        public float EndTime { get; protected set; }
        public bool IsFinished { get; protected set; }
        public bool IsCurrent() {
            return (GlobalLookup.CurrentTick >= StartTime && GlobalLookup.CurrentTick < EndTime) 
                || (GlobalLookup.CurrentTick >= StartTime && !this.IsFinished);
        }


        public virtual void Update(Entity entity) {

        }

        public EntityActionBase() {

        }
    }

    
}
