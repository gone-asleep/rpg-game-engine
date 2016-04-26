using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameEntities.Items;
using GameEngine.Factories;
using GameEngine.Items;
using GameEntities.Entities;
using GameEngine;

namespace GameEngineLib.Tests {
    [TestClass]
    public class FactoryTests {
        [TestMethod]
        public void WeaponsFactoryCreateRandomWeapon() {
            WeaponsFactory factory = new WeaponsFactory();
            var creationProfile = new ItemProfile(1, ItemProfileType.InventoryWarrior);
            IItem item1 = factory.Create(creationProfile);
            Assert.IsNotNull(item1);
            Assert.AreEqual(item1.Quality, item1.Quality);
        }

        [TestMethod]
        public void WeaponsFactoryCreateSpecifiedWeapon() {
            WeaponsFactory factory = new WeaponsFactory();
            var creationProfile = new ItemProfile(1, ItemType.Crossbow, ItemQualityCode.Flawless);
            IItem item1 = factory.Create(creationProfile);
            Assert.IsNotNull(item1);
            Assert.AreEqual(creationProfile.qualityCode, item1.Quality);
            Assert.AreEqual(creationProfile.Type, item1.Info.TypeCode);
        }

        [TestMethod]
        public void NPCFactoryCreate() {
            NPCFactory factory = new NPCFactory();
            var creationProfile = new EntityProfile(10, NameCatagoryCode.HumanFemale, GameEngine.Entities.EntityOccupation.Aristocrat, GameEngine.Entities.EntityRace.Dwarf, 10);
            IEntity entity = factory.Create(creationProfile);
        }
    }
}
