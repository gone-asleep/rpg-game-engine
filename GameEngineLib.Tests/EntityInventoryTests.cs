using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameEngine;
using GameEngine.Items;
using GameEngine.Entities.Skills;

namespace GameEngineLib.Tests {
    [TestClass]
    public class EntityInventoryTests {
        [TestMethod]
        public void Create() {
            IEntityInventory inventory = new EntityInventory(60);
        }

        [TestMethod]
        public void AddItem() {
            IEntityInventory inventory = new EntityInventory(60);
            IItemInfo info = new ItemInfo(ItemClassCode.Weapon, ItemType.LongSword, ItemEquipType.LeftHand, SkillType.OneHanded, false, true, 3);
            IItem testItem = new Item(Guid.NewGuid(), info, null);

            bool setSuccessfull = inventory.Set(testItem, 0);
            Assert.IsTrue(setSuccessfull);
        }

        [TestMethod]
        public void GetItem() {
            IEntityInventory inventory = new EntityInventory(60);
            IItemInfo info = new ItemInfo(ItemClassCode.Weapon, ItemType.LongSword, ItemEquipType.LeftHand, SkillType.OneHanded, false, true, 3);
            IItem testItem = new Item(Guid.NewGuid(), info, null);
            inventory.Set(testItem, 0);

            IItem getItem = inventory.Get(0);
            Assert.IsNotNull(getItem);
        }

        [TestMethod]
        public void EquipItem() {
            IEntityInventory inventory = new EntityInventory(60);
            IItemInfo info = new ItemInfo(ItemClassCode.Weapon, ItemType.LongSword, ItemEquipType.LeftHand, SkillType.OneHanded, false, true, 3);
            IItem testItem = new Item(Guid.NewGuid(), info, null);
            IEntityStats stats = new EntityStats();
            inventory.Set(testItem, 0);

            // equip the item
            bool equipSuccess = inventory.SetEquiped(0, stats);
            Assert.IsTrue(equipSuccess);

            // ensure we can no longer get the item
            IItem getItem = inventory.Get(0);
            Assert.IsNull(getItem);
        }


        [TestMethod]
        public void UnequipItem() {
            IEntityInventory inventory = new EntityInventory(60);
            IItemInfo info = new ItemInfo(ItemClassCode.Weapon, ItemType.LongSword, ItemEquipType.LeftHand, SkillType.OneHanded, false, true, 3);
            IItem testItem = new Item(Guid.NewGuid(), info, null);
            IEntityStats stats = new EntityStats();
            inventory.Set(testItem, 0);

            // equip the item
            bool equipSuccess = inventory.SetEquiped(0, stats);
            Assert.IsTrue(equipSuccess);

            bool unequipSuccess = inventory.SetUnequiped((int)testItem.Info.EquipType, stats);
            Assert.IsTrue(unequipSuccess);

            // the item should be back in the zero'th position
            IItem getItem = inventory.Get(0);
            Assert.IsNotNull(getItem);
        }

        [TestMethod]
        public void AddStackable() {
            IEntityInventory inventory = new EntityInventory(60);
            IItemInfo info = new ItemInfo(ItemClassCode.Potion, ItemType.HealingPotion, ItemEquipType.LeftHand, SkillType.OneHanded, true, false, 0);
            IItem testItem = new Item(Guid.NewGuid(), info, null);
            IItem testItem2 = new Item(Guid.NewGuid(), info, null);
            IItem testItem3 = new Item(Guid.NewGuid(), info, null);
            
            inventory.Set(testItem, 0);
            inventory.Set(testItem2, 0);
            inventory.Set(testItem3, 0);
            
            IItem getItem = inventory.Get(0);
            Assert.IsNotNull(getItem);
            Assert.AreEqual(getItem.Count, 3);
        }
    }
}
