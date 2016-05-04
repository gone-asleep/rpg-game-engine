using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Global {

    public class IDProvider {
        public Guid Next() {
            return Guid.NewGuid();
        }

        //public long Next() {
          // long ticks =  DateTime.Now.Ticks;
//            byte  dayByte = (byte)now.DayOfYear;
//            byte  year    = (byte)(2266 - now.Year);
//            byte  
//            day  9 bit (12*31=372 d)
//year 8 bit (2266-2010 = 256 y)
//seconds  17 bit (24*60*60=86400 s)
//hostname 12 bit (2^12=4096)
//random 18 bit (2^18=262144)
        //}
    }
}
