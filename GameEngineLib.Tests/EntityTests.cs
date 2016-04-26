using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameEngine.Entities;
using GameEngine;
using GameEngine.Items;
using GameEngine.Effects;
using GameEngine.AI;
using GameEngine.Entities.Skills;
using GameEngine.Global;

namespace GameEngineLib.Tests {
    [TestClass]
    public class EntityTests {
        float[] statValues = new float[] { /*strength*/1.0f, /*Stamina*/2.0f, /*Wisdom*/3.0f, /*Inteligence*/4.0f, /*Charisma*/5.0f, /*Agility*/6.0f, /*Luck*/7.0f, /*Speed*/8.0f };
        private float[] skillValues;


        [TestInitialize]
        public void Setup() {
            skillValues = new float[GameGlobal.SkillTypeCount];
            for (int i = 0; i < skillValues.Length; i++) {
                skillValues[i] = (float)i;
            }
        }

        private IGlobal GetDummyGlobal() {
            INetworkAdapter networkAdapter = new LoopbackNetworkAdapter();
            IGlobal global = new Global(networkAdapter);
            return global;
        }

        private IEntity GetDummyEntity() {
            IEntityInfo info = new EntityInfo(EntityRace.Human, EntityOccupation.Barbarian, "Grok");
            IEntityStats stats = new EntityStats(statValues);
            IEntitySkills skills = new EntitySkills(skillValues);
            IInventory inventory = new Inventory(60);
            IEntityAbility abilities = new EntityAbility(GeneralAbilities.All, ItemAbilities.None, EntityAbilities.ModifyInterationAbilities, EffectAbilities.ModifyMagicAbilities, AIAbilities.None);
            Guid id = Guid.NewGuid();
            IEntity entity = new Entity(id, info, skills, stats, inventory, abilities);
            return entity;
        }

        private IItem GetDummyWeapon() {
            IWeaponInfo info = new WeaponInfo(ItemType.LongSword, ItemEquipType.LeftHand, SkillType.HeavyBlade, 3, "Long Sword");
            Guid id = Guid.NewGuid();
            Item item = new Item(id, info, null, ItemQualityCode.Superior, 1);
            return item;
        }

        [TestMethod]
        public void EntityCreate() {
            try {
                IEntityInfo info = new EntityInfo(EntityRace.Human, EntityOccupation.Barbarian, "Grok");
                IEntityStats stats = new EntityStats(statValues);
                IEntitySkills skills = new EntitySkills(skillValues);
                IInventory inventory = new Inventory(60);
                IEntityAbility abilities = new EntityAbility(GeneralAbilities.All, ItemAbilities.None, EntityAbilities.ModifyInterationAbilities, EffectAbilities.ModifyMagicAbilities, AIAbilities.None);
                Guid id = Guid.NewGuid();

                // based on occupation generate some inventory items
                IEntity entity = new Entity(id, info, skills, stats, inventory, abilities);

                // check that we have an inventory of size 60
                Assert.AreEqual(inventory, entity.Inventory);
                Assert.AreEqual(abilities, entity.Abilities);
                Assert.AreEqual(stats, entity.Stats);
                Assert.AreEqual(info, entity.Info);
                Assert.AreEqual(id, entity.ID);
            } catch {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void EntityEquipItem() {
            IEntity entity = GetDummyEntity();
            IItem weapon = GetDummyWeapon();
            IGlobal global = GetDummyGlobal();
            
            entity.Inventory.Set(weapon, 0);

            bool success = entity.PerformAction(global, new ActionParameters() {
                 action = GeneralAbilities.Equip,
                 entityID = entity.ID,
                 targetIndex = 0
            });

            Assert.AreEqual(success, true);
        }


        [TestMethod]
        public void EntityUnequipItem() {
            IEntity entity = GetDummyEntity();
            IItem weapon = GetDummyWeapon();
            IGlobal global = GetDummyGlobal();

            entity.Inventory.Set(weapon, 0);
            entity.Inventory.SetEquiped(0, entity.Stats);

            bool success = entity.PerformAction(global, new ActionParameters() {
                action = GeneralAbilities.Unequip,
                entityID = entity.ID,
                targetIndex = (int)((IEquipableItemInfo)weapon.Info).EquipType
            });

            Assert.AreEqual(success, true);
        }


        [TestMethod]
        public void EntityUnequipMissingItem() {
            IEntity entity = GetDummyEntity();
            IGlobal global = GetDummyGlobal();

            bool success = entity.PerformAction(global, new ActionParameters() {
                action = GeneralAbilities.Unequip,
                entityID = entity.ID,
                targetIndex = (int)ItemEquipType.LeftHand
            });

            Assert.AreEqual(success, false);
        }

        [TestMethod]
        public void EntityEquipMissingItem() {
            IEntity entity = GetDummyEntity();
            IGlobal global = GetDummyGlobal();

            bool success = entity.PerformAction(global, new ActionParameters() {
                action = GeneralAbilities.Equip,
                entityID = entity.ID,
                targetIndex = 0
            });

            Assert.AreEqual(success, false);
        }

    }
}
