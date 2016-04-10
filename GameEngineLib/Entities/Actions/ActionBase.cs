using GameEngine.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine {
    public class ActionBase {
        /// <summary>
        /// The target entity who is performing the action/ or is being acted upon by the action
        /// </summary>
        public Entity TargetEntity { get; protected set; }

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
        

        public void UpdateAction(float currentTime) {
            // the action is current, do the action
            this.Do(currentTime - this.StartTime);
        }

        public void FinishAction(float currentTime) {
            this.Finish();
            this.IsFinished = true;
        }


        protected virtual void Do(float elapsedTime) {

        }

        protected virtual void Finish() {

        }
    }
}
