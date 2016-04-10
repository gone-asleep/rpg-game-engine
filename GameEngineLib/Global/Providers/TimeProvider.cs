using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Global.Providers {
    //note if this doesn't become any more complicated, consider merging into GameLookup

    public class TimeProvider {
        /// <summary>
        /// The Games Current Tick Value
        /// </summary>
        public float Current { get; set; }

        public bool CheckOccured(EntityActionBase action) {
            return action.EndTime <= Current;
        }

        public bool CheckCurrent(EntityActionBase action) {
            return Current >= action.StartTime && Current < action.EndTime;
        }


        public TimeProvider() {
            this.Current = 0;
        }
    }
}
