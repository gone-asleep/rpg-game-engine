using GameEngine.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine {
    public class EntityActionBase {

        /// <summary>
        /// The Starting Time for this Action
        /// </summary>
        public float StartTime { get; protected set; }

        /// <summary>
        /// The Ending Time for this Action
        /// </summary>
        public float EndTime { get; protected set; }

        public float TotalTime {
            get {
                return EndTime - StartTime;
            }
        }

        /// <summary>
        /// Boolean Value Indicating if this action has completed
        /// </summary>
        public bool IsFinished { get; protected set; }
        

        public void Update(Entity entity) {
            if (GlobalLookup.Time.CheckCurrent(this)) {
                this.Do(entity, GlobalLookup.Time.Current - this.StartTime);
            } 
            if (GlobalLookup.Time.CheckOccured(this)) {
                this.Finish(entity);
                this.IsFinished = true;
            }
        }

        public virtual void Do(Entity entity, float elapsedTime) {

        }

        public virtual void Finish(Entity entity) {

        }
    }
}
