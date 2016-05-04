using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameEntities.Items;
using GameEngine.Factories;
using GameEngine.Items;
using GameEntities.Entities;
using GameEngine;
using GameEngine.Entities;
using GameData;

namespace GameEngineLib.Tests {
    [TestClass]
    public class FactoryTests {
        [TestMethod]
        public void WeaponsFactoryCreateRandomWeapon() {
            ItemsFactory factory = new ItemsFactory();
            var creationProfile = new ItemProfile(1, EntityOccupation.Warrior);
            IItem item1 = factory.Create(creationProfile);
            Assert.IsNotNull(item1);
            Assert.AreEqual(item1.Quality, item1.Quality);
        }

        [TestMethod]
        public void WeaponsFactoryCreateSpecifiedWeapon() {
            ItemsFactory factory = new ItemsFactory();
            var creationProfile = new ItemProfile(1, ItemType.Crossbow, ItemQualityCode.Flawless);
            IItem item1 = factory.Create(creationProfile);
            Assert.IsNotNull(item1);
            Assert.AreEqual(creationProfile.qualityCode, item1.Quality);
            Assert.AreEqual(creationProfile.Type, item1.Info.TypeCode);
        }

        [TestMethod]
        public void NPCFactoryCreate() {
            ItemsFactory wfactory = new ItemsFactory();
            NPCFactory factory = new NPCFactory(wfactory);
            var creationProfile = new EntityProfile(10, EntityOccupation.Fisherman, EntityRace.None, 10);
            IEntity entity = factory.Create(creationProfile);
        }
    }
}
