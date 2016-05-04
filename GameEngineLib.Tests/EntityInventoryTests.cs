using GameData;
using GameData.Info;
using GameEngine;
using GameEngine.Entities;
using GameEngine.Items;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
            IItemInfo info = new ItemWeaponInfo(ItemType.LongSword, ItemWieldType.OneHand, SkillType.HeavyBlade, 10, 3);
            IItem testItem = new Item(Guid.NewGuid(), info, null, ItemQualityCode.Superior, 1);

            bool setSuccessfull = inventory.Set(testItem, 0, null);
            Assert.IsTrue(setSuccessfull);
        }

        [TestMethod]
        public void InventoryGetItem() {
            IInventory inventory = new Inventory(60);
            IItemInfo info = new ItemWeaponInfo(ItemType.LongSword, ItemWieldType.OneHand, SkillType.HeavyBlade, 10, 3);
            IItem testItem = new Item(Guid.NewGuid(), info, null, ItemQualityCode.Superior, 1);
            inventory.Set(testItem, 0, null);

            IItem getItem = inventory.Get(0, null);
            Assert.IsNotNull(getItem);
        }

        [TestMethod]
        public void InventoryEquipItem() {
            IInventory inventory = new Inventory(60);
            IItemInfo info = new ItemArmorInfo(ItemType.ClothCap, InventorySlot.Head, 10, 5);
            IItem testItem = new Item(Guid.NewGuid(), info, null, ItemQualityCode.Superior, 1);
            IEntityAbility abilities = new EntityAbility(GeneralAbilities.All, ItemAbilities.None, EntityAbilities.ModifyInterationAbilities, EffectAbilities.ModifyMagicAbilities, AIAbilities.None);
                
            IEntityStats stats = new EntityStats();
            inventory.Set(testItem, InventorySlot.Inventory1, null);

            bool equipSuccess = inventory.Swap(InventorySlot.Inventory1, InventorySlot.Head, stats);
            Assert.IsTrue(equipSuccess);

            // ensure we can no longer get the item
            IItem getItem = inventory.Get(0, null);
            Assert.IsNull(getItem);
        }

        [TestMethod]
        public void InventoryWieldItem() {
            IInventory inventory = new Inventory(60);
            IItemInfo info = new ItemWeaponInfo(ItemType.LongSword, ItemWieldType.OneHand, SkillType.HeavyBlade, 10, 3);
            IItem testItem = new Item(Guid.NewGuid(), info, null, ItemQualityCode.Superior, 1);
            IEntityAbility abilities = new EntityAbility(GeneralAbilities.All, ItemAbilities.None, EntityAbilities.ModifyInterationAbilities, EffectAbilities.ModifyMagicAbilities, AIAbilities.None);

            IEntityStats stats = new EntityStats();
            bool wieldSuccess = inventory.Set(testItem, InventorySlot.WieldPrimary, null);
            Assert.IsTrue(wieldSuccess);

            // ensure we can no longer get the item
            IItem getItem = inventory.Get(InventorySlot.Inventory1, stats);
            Assert.IsNull(getItem);
        }

        [TestMethod]
        public void InventoryUnequipItem() {
            IInventory inventory = new Inventory(60);
            IItemInfo info = new ItemArmorInfo(ItemType.ClothCap, InventorySlot.Head, 10, 5);
            IItem testItem = new Item(Guid.NewGuid(), info, null, ItemQualityCode.Superior, 1);
            IEntityStats stats = new EntityStats();
            IEntityAbility abilities = new EntityAbility(GeneralAbilities.All, ItemAbilities.None, EntityAbilities.ModifyInterationAbilities, EffectAbilities.ModifyMagicAbilities, AIAbilities.None);

            inventory.Set(testItem, InventorySlot.Inventory1, stats);
            Assert.AreEqual(inventory.Remaining, 59);
            // equip the item
            bool equipSuccess = inventory.Swap(InventorySlot.Inventory1, InventorySlot.Head, stats);
            Assert.IsTrue(equipSuccess);
            Assert.AreEqual(inventory.Remaining, 60);

            bool unequipSuccess = inventory.Swap(InventorySlot.Head, InventorySlot.Inventory1, stats);
            Assert.IsTrue(unequipSuccess);
            Assert.AreEqual(inventory.Remaining, 59);

            // the item should be back in the zero'th position
            IItem getItem = inventory.Get(InventorySlot.Inventory1, stats);
            Assert.AreEqual(inventory.Remaining, 60);
            Assert.IsNotNull(getItem);
        }

        [TestMethod]
        public void InventoryUnwieldItem() {
            IInventory inventory = new Inventory(60);
            IItemWeaponInfo info = new ItemWeaponInfo(ItemType.LongSword, ItemWieldType.OneHand, SkillType.HeavyBlade, 10, 3);
            IItem testItem = new Item(Guid.NewGuid(), info, null, ItemQualityCode.Superior, 1);
            IEntityStats stats = new EntityStats();
            IEntityAbility abilities = new EntityAbility(GeneralAbilities.All, ItemAbilities.None, EntityAbilities.ModifyInterationAbilities, EffectAbilities.ModifyMagicAbilities, AIAbilities.None);

            inventory.Set(testItem, InventorySlot.Inventory1, stats);
            Assert.AreEqual(inventory.Remaining, 59);
            // equip the item
            bool wieldSuccess = inventory.Swap(InventorySlot.Inventory1, InventorySlot.WieldPrimary, stats);
            Assert.IsTrue(wieldSuccess);
            Assert.AreEqual(inventory.Remaining, 60);

            bool unwieldSuccess = inventory.Swap(InventorySlot.WieldPrimary, InventorySlot.Inventory1, stats);
            Assert.IsTrue(unwieldSuccess);
            Assert.AreEqual(inventory.Remaining, 59);

            // the item should be back in the zero'th position
            IItem getItem = inventory.Get(InventorySlot.Inventory1, stats);
            Assert.AreEqual(inventory.Remaining, 60);
            Assert.IsNotNull(getItem);
        }


        [TestMethod]
        public void InventoryDestoyItem() {
            IInventory inventory = new Inventory(60);
            IItemInfo info = new ItemInfo(ItemClassCode.Potion, ItemType.HealingPotion);
            IItem testItem = new Item(Guid.NewGuid(), info, null, ItemQualityCode.Superior, 1);
            
            inventory.Set(testItem, InventorySlot.Inventory1, null);
            Assert.AreEqual(inventory.Remaining, 59);
            inventory.Get(InventorySlot.Inventory1, null);
            Assert.AreEqual(inventory.Remaining, 60);
        }

        [TestMethod]
        public void InventoryDestoyQuantityOfItem() {
            IInventory inventory = new Inventory(60);
            IItemInfo info = new ItemConsumableInfo(ItemClassCode.Potion, ItemType.HealingPotion, 10);
            IItem testItem = new Item(Guid.NewGuid(), info, null, ItemQualityCode.Superior, 1);
            IItem testItem2 = new Item(Guid.NewGuid(), info, null, ItemQualityCode.Superior, 1);
            
            inventory.Set(testItem, InventorySlot.Inventory1, null);
            inventory.Set(testItem2, InventorySlot.Inventory1, null);
            Assert.AreEqual(inventory.Remaining, 59);
            var item = inventory.Get(InventorySlot.Inventory1, null);
            item.Count--;
            inventory.Set(item, InventorySlot.Inventory1, null);

            IItem getItem = inventory.Get(InventorySlot.Inventory1, null);
            Assert.IsNotNull(getItem);
            Assert.AreEqual(1, getItem.Count);
            Assert.AreEqual(inventory.Remaining, 60);
        }


        [TestMethod]
        public void InventoryAddStackable() {
            IInventory inventory = new Inventory(60);
            IItemInfo info = new ItemConsumableInfo(ItemClassCode.Potion, ItemType.HealingPotion, 10);
            IItem testItem = new Item(Guid.NewGuid(), info, null, ItemQualityCode.Superior, 1);
            IItem testItem2 = new Item(Guid.NewGuid(), info, null, ItemQualityCode.Superior, 1);
            IItem testItem3 = new Item(Guid.NewGuid(), info, null, ItemQualityCode.Superior, 1);

            inventory.Set(testItem, InventorySlot.Inventory1, null);
            inventory.Set(testItem2, InventorySlot.Inventory1, null);
            inventory.Set(testItem3, InventorySlot.Inventory1, null);
            Assert.AreEqual(inventory.Remaining, 59);
            IItem getItem = inventory.Get(InventorySlot.Inventory1, null);
            Assert.IsNotNull(getItem);
            Assert.AreEqual(3, getItem.Count);
            Assert.AreEqual(inventory.Remaining, 60);
            
        }
    }
}
