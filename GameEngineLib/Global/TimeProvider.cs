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

        public TimeProvider() {
            this.Current = 0;
        }
    }
}
