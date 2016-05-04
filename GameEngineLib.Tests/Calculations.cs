using GameData;
using GameEngine.Global;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace GameEngineLib.Tests {
    [TestClass]
    public class Calculations {
        public float[] GenerateStats(float points, float skew, StatType[] preferrence, StatType[] neglected) {
            float baseVal = 1.0f / 3.0f;
            float baseMax = 2.0f / 3.0f;

            float diff = baseMax - baseVal;

            float prefValue = (points * (baseVal + skew * diff)) / preferrence.Length;
            float passValue = (points * (baseVal - (skew * (1.0f - 0.3333333f)) * diff)) / (Globals.StatTypeCount - (neglected.Length + preferrence.Length));
            float neglValue = (points * (baseVal - (skew * 0.3333333f) * diff)) / neglected.Length;

            float[] stats = new float[Globals.StatTypeCount];
            for (int i = 0; i < Globals.StatTypeCount; i++) {
                if (preferrence.Contains((StatType)i)) {
                    stats[i] = prefValue;
                } else if (neglected.Contains((StatType)i)) {
                    stats[i] = neglValue;
                } else {
                    stats[i] = passValue;
                }
            }
            return stats;
        }

        [TestMethod]
        public void BaseStatDistributions() {
            // currently needs work to distribute based on the number of pref pass and negl, currently allows the distribution to be more influenced by these settings than the actual skew
            float[] test = GenerateStats(100, 1, new[] { StatType.Inteligence, StatType.Wisdom }, new[] { StatType.Strength, StatType.Dexterity });

            // this formula is intented to be used to generate NPCs with stats grouped into 3 catagories (Preferred, Passive, Neglected)
            // this mimics how a player would distribute skill points with an occupation goal in mind
            float prefBase = 1.0f / 3.0f;
            float prefMax  = 2.0f / 3.0f; // 4/5ths distribution of points as the most skewed
            float passBase = 1.0f / 3.0f;
            float NeglBase = 1.0f / 3.0f;

            float NeglSkew = 2.0f / 3.0f; // neglected falls twice as fast as passive
            float PassSkew = 1 - NeglSkew;

            float p1Mult = (prefMax - prefBase);
            float p2Mult = -PassSkew * p1Mult;
            float p3Mult = -NeglSkew * p1Mult;

            for (float i = 0; i <= 1.0f; i += 0.2f) {
                float a1 = prefBase + i * p1Mult;
                float a2 = passBase + i * p2Mult;
                float a3 = NeglBase + i * p3Mult;
                this.AreSimiliar(a1 + a2 + a3, 1.0f);
            }

        }
        [TestMethod]
        public void TestRandomInt() {
            // I like this method because of it's ability to be deterministic random
            // the server can reseed it at any point and keep both -random- generators synced accross
            // computers
            GameGlobal.SeedRandomInt(1212, 3434, 42);
            int rnd1 = GameGlobal.NextRandomInt(100);
            int rnd2 = GameGlobal.NextRandomInt(100);
            int rnd3 = GameGlobal.NextRandomInt(100);
            int rnd4 = GameGlobal.NextRandomInt(100);
            int rnd5 = GameGlobal.NextRandomInt(100);

        }

        private void AreSimiliar(float a, float b) {
            if (Math.Abs(a - b) > float.Epsilon) {
                Assert.Fail("Float are not approx equal");
                // Values are within specified tolerance of each other....
            }
        }



    }
}
