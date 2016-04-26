using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameEngine;
using ProtoBuf.Serializers;
using ProtoBuf;
using GameEngine.Items;
using GameEngine.Entities.Stats;
using GameEngine.Entities;
using GameEngine.Entities.Skills;
using GameEngine.AI;
using GameEngine.Effects;
using System.IO;
using System.Diagnostics;
using GameEngine.Global;
using GameEntities.Items;
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
            
            skillValues = new float[GameGlobal.SkillTypeCount];
            for (int i = 0; i < skillValues.Length; i++) {
                skillValues[i] = (float)i;
            }
        }

        [TestMethod]
        public void SerializeItemInfo() {
            IItemInfo info = new ItemInfo(ItemClassCode.Potion, ItemType.HealingPotion, true, "Healing Potion");
            IItemInfo infoClone = Serializer.DeepClone(info);

            Assert.AreEqual(info.ClassCode, infoClone.ClassCode);
            Assert.AreEqual(info.TypeCode, infoClone.TypeCode);
            Assert.AreEqual(info.Stackable, infoClone.Stackable);
            Assert.AreEqual(info.Name, infoClone.Name);

        }

        [TestMethod]
        public void SerializeItem() {
            
            IItemInfo info = new ItemInfo(ItemClassCode.Potion, ItemType.HealingPotion, true, "Healing Potion");
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
            IItemInfo info = new ItemInfo(ItemClassCode.Potion, ItemType.HealingPotion, true);
            IItem testItem = new Item(Guid.NewGuid(), info, null, ItemQualityCode.Superior, 1);
            inventory.Set(testItem, 5);
            
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
            Assert.AreEqual(statsClone.Get(StatType.Agility), statsClone.Get(StatType.Agility));
            Assert.AreEqual(statsClone.Get(StatType.Luck), statsClone.Get(StatType.Luck));
            Assert.AreEqual(statsClone.Get(StatType.Dexterity), statsClone.Get(StatType.Dexterity));
        }

        [TestMethod]
        public void SerializeNPCStats() {
            float[] statDistributionValues = new float[] { /*strength*/0.1f, /*Stamina*/0.1f, /*Wisdom*/0.3f, /*Inteligence*/0.1f, /*Charisma*/0.2f, /*Agility*/0.1f, /*Luck*/0.05f, /*Speed*/0.05f };
            IEntityStats stats = new NPCStats(100, statDistributionValues);
            IEntityStats statsClone = Serializer.DeepClone(stats);
            Assert.AreEqual(statsClone.Get(StatType.Strength), Math.Floor(statDistributionValues[(int)StatType.Strength] * 100));
            Assert.AreEqual(statsClone.Get(StatType.Stamina), Math.Floor(statDistributionValues[(int)StatType.Stamina] * 100));
            Assert.AreEqual(statsClone.Get(StatType.Wisdom), Math.Floor(statDistributionValues[(int)StatType.Wisdom] * 100));
            Assert.AreEqual(statsClone.Get(StatType.Inteligence), Math.Floor(statDistributionValues[(int)StatType.Inteligence] * 100));
            Assert.AreEqual(statsClone.Get(StatType.Charisma), Math.Floor(statDistributionValues[(int)StatType.Charisma] * 100));
            Assert.AreEqual(statsClone.Get(StatType.Agility), Math.Floor(statDistributionValues[(int)StatType.Agility] * 100));
            Assert.AreEqual(statsClone.Get(StatType.Luck), Math.Floor(statDistributionValues[(int)StatType.Luck] * 100));
            Assert.AreEqual(statsClone.Get(StatType.Dexterity), Math.Floor(statDistributionValues[(int)StatType.Dexterity] * 100));
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
            IEntityInfo info = new EntityInfo(EntityRace.Human, EntityOccupation.Barbarian, "Grok");
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
            IEntityInfo info = new EntityInfo(EntityRace.Human, EntityOccupation.Barbarian, "Grok");
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
                action = GeneralAbilities.Equip,
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
            WeaponsFactory factory = new WeaponsFactory();
            WeaponsFactory clone = Serializer.DeepClone(factory);
            Assert.IsNotNull(clone); // fails due to jagged arrays :( figure out new strategy
        }

    }
}

