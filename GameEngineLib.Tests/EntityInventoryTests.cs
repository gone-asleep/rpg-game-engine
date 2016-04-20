using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameEngine;
using GameEngine.Items;
using GameEngine.Entities.Skills;
using GameEngine.Entities;
using GameEngine.Effects;
using GameEngine.AI;

namespace GameEngineLib.Tests {
    [TestClass]
    public class EntityInventoryTests {
        [TestMethod]
        public void InventoryCreate() {
            IInventory inventory = new Inventory(60);
        }

        [TestMethod]
        public void InventoryAddItem() {
            IInventory inventory = new Inventory(60);
            IItemInfo info = new WeaponInfo(ItemType.LongSword, ItemEquipType.LeftHand, SkillType.HeavyBlade, 3);
            IItem testItem = new Item(Guid.NewGuid(), info, null, 1);

            bool setSuccessfull = inventory.Set(testItem, 0);
            Assert.IsTrue(setSuccessfull);
        }

        [TestMethod]
        public void InventoryGetItem() {
            IInventory inventory = new Inventory(60);
            IItemInfo info = new WeaponInfo(ItemType.LongSword, ItemEquipType.LeftHand, SkillType.HeavyBlade, 3);
            IItem testItem = new Item(Guid.NewGuid(), info, null, 1);
            inventory.Set(testItem, 0);

            IItem getItem = inventory.Get(0);
            Assert.IsNotNull(getItem);
        }

        [TestMethod]
        public void InventoryEquipItem() {
            IInventory inventory = new Inventory(60);
            IItemInfo info = new WeaponInfo(ItemType.LongSword, ItemEquipType.LeftHand, SkillType.HeavyBlade, 3);
            IItem testItem = new Item(Guid.NewGuid(), info, null, 1);
            IEntityAbility abilities = new EntityAbility(GeneralAbilities.All, ItemAbilities.None, EntityAbilities.ModifyInterationAbilities, EffectAbilities.ModifyMagicAbilities, AIAbilities.None);
                
            IEntityStats stats = new EntityStats();
            inventory.Set(testItem, 0);

            bool equipSuccess = inventory.SetEquiped(0, stats);
            Assert.IsTrue(equipSuccess);

            // ensure we can no longer get the item
            IItem getItem = inventory.Get(0);
            Assert.IsNull(getItem);
        }


        [TestMethod]
        public void InventoryUnequipItem() {
            IInventory inventory = new Inventory(60);
            IWeaponInfo info = new WeaponInfo(ItemType.LongSword, ItemEquipType.LeftHand, SkillType.HeavyBlade, 3);
            IItem testItem = new Item(Guid.NewGuid(), info, null, 1);
            IEntityStats stats = new EntityStats();
            IEntityAbility abilities = new EntityAbility(GeneralAbilities.All, ItemAbilities.None, EntityAbilities.ModifyInterationAbilities, EffectAbilities.ModifyMagicAbilities, AIAbilities.None);
            
            inventory.Set(testItem, 0);
            Assert.AreEqual(inventory.Remaining, 59);
            // equip the item
            bool equipSuccess = inventory.SetEquiped(0, stats);
            Assert.IsTrue(equipSuccess);
            Assert.AreEqual(inventory.Remaining, 60);

            bool unequipSuccess = inventory.SetUnequiped((int)((IWeaponInfo)testItem.Info).EquipType, stats);
            Assert.IsTrue(unequipSuccess);
            Assert.AreEqual(inventory.Remaining, 59);

            // the item should be back in the zero'th position
            IItem getItem = inventory.Get(0);
            Assert.AreEqual(inventory.Remaining, 60);
            Assert.IsNotNull(getItem);
        }

        [TestMethod]
        public void InventoryDestoyItem() {
            IInventory inventory = new Inventory(60);
            IItemInfo info = new ItemInfo(ItemClassCode.Potion, ItemType.HealingPotion, true);
            IItem testItem = new Item(Guid.NewGuid(), info, null, 1);
            
            inventory.Set(testItem, 0);
            Assert.AreEqual(inventory.Remaining, 59);
            bool success = inventory.Destroy(0, 1);
            Assert.AreEqual(inventory.Remaining, 60);
            Assert.IsTrue(success);

            IItem getItem = inventory.Get(0);
            Assert.IsNull(getItem);
            Assert.AreEqual(inventory.Remaining, 60);
        }

        [TestMethod]
        public void InventoryDestoyQuantityOfItem() {
            IInventory inventory = new Inventory(60);
            IItemInfo info = new ItemInfo(ItemClassCode.Potion, ItemType.HealingPotion, true);
            IItem testItem = new Item(Guid.NewGuid(), info, null, 1);
            IItem testItem2 = new Item(Guid.NewGuid(), info, null, 1);
            
            inventory.Set(testItem, 0);
            inventory.Set(testItem2, 0);

            bool success = inventory.Destroy(0, 1);
            Assert.IsTrue(success);
            Assert.AreEqual(inventory.Remaining, 59);
            IItem getItem = inventory.Get(0);
            Assert.IsNotNull(getItem);
            Assert.AreEqual(1, getItem.Count);
            Assert.AreEqual(inventory.Remaining, 60);
        }


        [TestMethod]
        public void InventoryAddStackable() {
            IInventory inventory = new Inventory(60);
            IItemInfo info = new ItemInfo(ItemClassCode.Potion, ItemType.HealingPotion, true);
            IItem testItem = new Item(Guid.NewGuid(), info, null, 1);
            IItem testItem2 = new Item(Guid.NewGuid(), info, null, 1);
            IItem testItem3 = new Item(Guid.NewGuid(), info, null, 1);
            
            inventory.Set(testItem, 0);
            inventory.Set(testItem2, 0);
            inventory.Set(testItem3, 0);
            Assert.AreEqual(inventory.Remaining, 59);
            IItem getItem = inventory.Get(0);
            Assert.IsNotNull(getItem);
            Assert.AreEqual(getItem.Count, 3);
            Assert.AreEqual(inventory.Remaining, 60);
            
        }
    }
}
