﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameEngine;
using GameEngine.Entities.Skills;
using GameEngine.Entities.Stats;
using GameEngine.Entities;

namespace GameEngineLib.Tests {
    [TestClass]
    public class EntityStatTests {

        private float[] statValues = new float[] { /*strength*/1.0f, /*Stamina*/2.0f, /*Wisdom*/3.0f, /*Inteligence*/4.0f, /*Charisma*/5.0f, /*Agility*/6.0f, /*Luck*/7.0f, /*Speed*/8.0f };
        private float[] statDistributionValues = new float[] { /*strength*/0.1f, /*Stamina*/0.1f, /*Wisdom*/0.3f, /*Inteligence*/0.1f, /*Charisma*/0.2f, /*Agility*/0.1f, /*Luck*/0.05f, /*Speed*/0.05f };
               

        [TestMethod]
        public void StatsCreate() {
            try {
                IEntityStats stats = new EntityStats();
            } catch {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void EntityStatsCreateWithValues() {
            try {
                IEntityStats stats = new EntityStats(statValues);
                Assert.AreEqual(stats.Get(StatType.Strength), 1.0f);
                Assert.AreEqual(stats.Get(StatType.Stamina), 2.0f);
                Assert.AreEqual(stats.Get(StatType.Wisdom), 3.0f);
                Assert.AreEqual(stats.Get(StatType.Inteligence), 4.0f);
                Assert.AreEqual(stats.Get(StatType.Charisma), 5.0f);
                Assert.AreEqual(stats.Get(StatType.Constitution), 6.0f);
                Assert.AreEqual(stats.Get(StatType.Luck), 7.0f);
                Assert.AreEqual(stats.Get(StatType.Dexterity), 8.0f);
            } catch {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void NPCStatsCreate() {
            try {
                IEntityStats stats = new NPCStats();
            } catch {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void NPCStatsCreateWithValues() {
            try {
                IEntityStats stats = new NPCStats(100, statDistributionValues);
                Assert.AreEqual(stats.Get(StatType.Strength), Math.Floor(statDistributionValues[(int)StatType.Strength]*100));
                Assert.AreEqual(stats.Get(StatType.Stamina), Math.Floor(statDistributionValues[(int)StatType.Stamina] * 100));
                Assert.AreEqual(stats.Get(StatType.Wisdom), Math.Floor(statDistributionValues[(int)StatType.Wisdom] * 100));
                Assert.AreEqual(stats.Get(StatType.Inteligence), Math.Floor(statDistributionValues[(int)StatType.Inteligence] * 100));
                Assert.AreEqual(stats.Get(StatType.Charisma), Math.Floor(statDistributionValues[(int)StatType.Charisma] * 100));
                Assert.AreEqual(stats.Get(StatType.Constitution), Math.Floor(statDistributionValues[(int)StatType.Constitution] * 100));
                Assert.AreEqual(stats.Get(StatType.Luck), Math.Floor(statDistributionValues[(int)StatType.Luck] * 100));
                Assert.AreEqual(stats.Get(StatType.Dexterity), Math.Floor(statDistributionValues[(int)StatType.Dexterity] * 100));
            } catch {
                Assert.Fail();
            }
        }

    }
}
