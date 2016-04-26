using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameEngineLib.Tests {
    [TestClass]
    public class UniqueID {
        [TestMethod]
        public void TestUniqueIDGeneration() {

            Random rnd = new Random();


            string ticks2 = Convert(DateTime.Now.Ticks);
            string ticks3 = Convert(DateTime.Now.Ticks);
            
        }

        public string Convert(long x) {
            char[] bits = new char[64];
            for (int i = 0; i < 63; i++) {
                bits[63 - i] = ((1 << i) & x) > 1 ? '1' : '0'; 
            }
            return new string(bits);

        }
    }
}
