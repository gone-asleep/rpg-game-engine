using GameData;
using GameData.Info;
using GameEngine;
using GameEngine.Entities;
using GameEngine.Global;
using GameEngine.Items;
using GameEntities.Items;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProtoBuf;
using System;
using System.Diagnostics;
using System.IO;

namespace GameEngineLib.Tests {
    [TestClass]
    public class SerializationTests {
        private float[] statValues = new float[] { /*strength*/1.0f, /*Stamina*/2.0f, /*Wisdom*/3.0f, /*Inteligence*/4.0f, /*Charisma*/5.0f, /*Agility*/6.0f, /*Luck*/7.0f, /*Speed*/8.0f };
        private float[] skillValues;


        [TestInitialize]
        public void Initialize() {
            Serializer.PrepareSerializer<IEntityInfo>();
            Serializer.PrepareSerializer<IEntityAbility>();
            Serializer.PrepareSerializer<IItem>();
            Serializer.PrepareSerializer<IItemInfo>();
            Serializer.PrepareSerializer<IInventory>();
            Serializer.PrepareSerializer<IEntityStats>();
            Serializer.PrepareSerializer<IEntitySkills>();

            skillValues = new float[Globals.SkillTypeCount];
            for (int i = 0; i < skillValues.Length; i++) {
                skillValues[i] = (float)i;
            }
        }

        [TestMethod]
        public void SerializeItemInfo() {
            IItemInfo info = new ItemConsumableInfo(ItemClassCode.Potion, ItemType.HealingPotion, 5, "Healing Potion");
            IItemInfo infoClone = Serializer.DeepClone(info);

            Assert.AreEqual(info.ClassCode, infoClone.ClassCode);
            Assert.AreEqual(info.TypeCode, infoClone.TypeCode);
            Assert.IsTrue(info is IItemConsumableInfo);
            Assert.AreEqual(info.Name, infoClone.Name);

        }

        [TestMethod]
        public void SerializeItem() {
            
            IItemInfo info = new ItemInfo(ItemClassCode.Potion, ItemType.HealingPotion, "Healing Potion");
            IItem testItem = new Item(Guid.NewGuid(), info, null, ItemQualityCode.Superior, 1);
            IItem testItemClone = Serializer.DeepClone(testItem);

            Assert.AreEqual(testItem.Count, testItemClone.Count);
            Assert.AreEqual(testItem.ID, testItemClone.ID);
            Assert.AreEqual(testItem.Quality, testItemClone.Quality);
            Assert.AreEqual(testItem.Info, testItemClone.Info);
        }

        [TestMethod]
        public void SerializeInventory() {
            IInventory inventory = new Inventory(60);
            IItemInfo info = new ItemInfo(ItemClassCode.Potion, ItemType.HealingPotion);
            IItem testItem = new Item(Guid.NewGuid(), info, null, ItemQualityCode.Superior, 1);
            inventory.Set(testItem, InventorySlot.Inventory13, null);
            
            IInventory inventoryClone = Serializer.DeepClone(inventory);

            Assert.AreEqual(inventory.Remaining, inventoryClone.Remaining);
            Assert.AreEqual(inventory.Size, inventoryClone.Size);

        }

        [TestMethod]
        public void SerializeStats() {
            
            float[] statValues = new float[] { /*strength*/1.0f, /*Stamina*/2.0f, /*Wisdom*/3.0f, /*Inteligence*/4.0f, /*Charisma*/5.0f, /*Agility*/6.0f, /*Luck*/7.0f, /*Speed*/8.0f };
            IEntityStats stats = new EntityStats(statValues);
            IEntityStats statsClone = Serializer.DeepClone(stats);
            Assert.AreEqual(statsClone.Get(StatType.Strength), statsClone.Get(StatType.Strength));
            Assert.AreEqual(statsClone.Get(StatType.Stamina), statsClone.Get(StatType.Stamina));
            Assert.AreEqual(statsClone.Get(StatType.Wisdom), statsClone.Get(StatType.Wisdom));
            Assert.AreEqual(statsClone.Get(StatType.Inteligence), statsClone.Get(StatType.Inteligence));
            Assert.AreEqual(statsClone.Get(StatType.Charisma), statsClone.Get(StatType.Charisma));
            Assert.AreEqual(statsClone.Get(StatType.Constitution), statsClone.Get(StatType.Constitution));
            Assert.AreEqual(statsClone.Get(StatType.Luck), statsClone.Get(StatType.Luck));
            Assert.AreEqual(statsClone.Get(StatType.Dexterity), statsClone.Get(StatType.Dexterity));
        }

        [TestMethod]
        public void SerializeSkills() {
            IEntitySkills skills = new EntitySkills(skillValues);
            IEntitySkills skillsClone = Serializer.DeepClone(skills);

            Assert.AreEqual(skillsClone.Get(SkillType.LightArmor), skills.Get(SkillType.LightArmor));
            Assert.AreEqual(skillsClone.Get(SkillType.HeavyArmor), skills.Get(SkillType.HeavyArmor));
            Assert.AreEqual(skillsClone.Get(SkillType.LightBlade), skills.Get(SkillType.LightBlade));
            Assert.AreEqual(skillsClone.Get(SkillType.HeavyBlade), skills.Get(SkillType.HeavyBlade));
            Assert.AreEqual(skillsClone.Get(SkillType.BluntWeapon), skills.Get(SkillType.BluntWeapon));
            Assert.AreEqual(skillsClone.Get(SkillType.Sneak), skills.Get(SkillType.Sneak));
            Assert.AreEqual(skillsClone.Get(SkillType.Pickpocket), skills.Get(SkillType.Pickpocket));
            Assert.AreEqual(skillsClone.Get(SkillType.LockPicking), skills.Get(SkillType.LockPicking));
        }

        [TestMethod]
        public void SerializeInfo() {
            IEntityInfo info = new EntityInfo(EntityRace.Human, EntityOccupation.Barbarian, 365 * 25, "Grok");
            IEntityInfo infoClone = Serializer.DeepClone(info);
            Assert.AreEqual(info.Name, infoClone.Name);
            Assert.AreEqual(info.Occupation, infoClone.Occupation);
            Assert.AreEqual(info.Race, infoClone.Race);
        }

        [TestMethod]
        public void SerializeAbilities() {
            IEntityAbility abilities = new EntityAbility(GeneralAbilities.All, ItemAbilities.None, EntityAbilities.ModifyInterationAbilities, EffectAbilities.ModifyMagicAbilities, AIAbilities.None);
            IEntityAbility abilitiesClone = Serializer.DeepClone(abilities);

            Assert.AreEqual(abilities, abilitiesClone);

        }

        [TestMethod]
        public void SerializeEntity() {
            IEntityInfo info = new EntityInfo(EntityRace.Human, EntityOccupation.Barbarian, 25 * 365, "Grok");
            IEntityStats stats = new EntityStats(statValues);
            IEntitySkills skills = new EntitySkills(skillValues);
            IInventory inventory = new Inventory(60);
            IEntityAbility abilities = new EntityAbility(GeneralAbilities.All, ItemAbilities.None, EntityAbilities.ModifyInterationAbilities, EffectAbilities.ModifyMagicAbilities, AIAbilities.None);
            Guid id = Guid.NewGuid();
            IEntity entity = new Entity(id, info, skills, stats, inventory, abilities);
            IEntity entityClone = Serializer.DeepClone(entity);

            // to do add equality tests here
            //Assert.Fail();
        }

        [TestMethod]
        public void SerializeActionParameters() {
            ActionParameters parameters = new ActionParameters() {
                action = GeneralAbilities.Give,
                entityID = Guid.NewGuid(),
                itemID = Guid.NewGuid(),
                targetIndex = 1,
                targetQuantity = 2
            };
            ActionParameters clone = Serializer.DeepClone(parameters);

            using (var stream = new MemoryStream()) {
                Serializer.Serialize(stream, parameters);
                byte[] buf = stream.GetBuffer();
                for (int i = 0; i < stream.Length; i++)
                    Debug.WriteLine("{0:X2} ", buf[i]);
            }
        }

        [TestMethod]
        public void SerializeWeaponsFactory() {
            ItemsFactory factory = new ItemsFactory();
            ItemsFactory clone = Serializer.DeepClone(factory);
            Assert.IsNotNull(clone); // fails due to jagged arrays :( figure out new strategy
        }

    }
}

