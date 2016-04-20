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
namespace GameEngineLib.Tests {
    [TestClass]
    public class SerializationTests {

        [TestInitialize]
        public void Initialize() {
            Serializer.PrepareSerializer<IItem>();
            Serializer.PrepareSerializer<IItemInfo>();
            Serializer.PrepareSerializer<IInventory>();
            Serializer.PrepareSerializer<IEntityStats>();
            Serializer.PrepareSerializer<IEntitySkills>();
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
            IItem testItem = new Item(Guid.NewGuid(), info, null, 1);
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
            IItem testItem = new Item(Guid.NewGuid(), info, null, 1);
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
            Assert.AreEqual(statsClone.Get(StatType.Speed), statsClone.Get(StatType.Speed));
        }

        [TestMethod]
        public void SerializeSkills() {
            float[] skillValues = new float[] { 
            /*LightArmor*/1.0f, /*HeavyArmor*/2.0f, /*LightBlade*/3.0f, 
            /*HeavyBlade*/4.0f, /*BluntWeapon*/5.0f, /*Sneak*/6.0f, 
            /*Pickpocket*/7.0f, /*LockPicking*/8.0f, /*RangeWeapon*/9.0f,
            /*PolearmWeapon*/7.0f, /*AxeWeapon*/8.0f, /*FlailWeapon*/9.0f,
            /*StaffWeapon*/7.0f, /*HammerWeapon*/8.0f
            };

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
            IEntityInfo info = new EntityInfo(EntityRace.Human, EntityOccupation.Barbarian);
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
            float[] skillValues = new float[] { 
            /*LightArmor*/1.0f, /*HeavyArmor*/2.0f, /*LightBlade*/3.0f, 
            /*HeavyBlade*/4.0f, /*BluntWeapon*/5.0f, /*Sneak*/6.0f, 
            /*Pickpocket*/7.0f, /*LockPicking*/8.0f, /*RangeWeapon*/9.0f,
            /*PolearmWeapon*/7.0f, /*AxeWeapon*/8.0f, /*FlailWeapon*/9.0f,
            /*StaffWeapon*/7.0f, /*HammerWeapon*/8.0f
            };
            float[] statValues = new float[] { /*strength*/1.0f, /*Stamina*/2.0f, /*Wisdom*/3.0f, /*Inteligence*/4.0f, /*Charisma*/5.0f, /*Agility*/6.0f, /*Luck*/7.0f, /*Speed*/8.0f };

            
            IEntityInfo info = new EntityInfo(EntityRace.Human, EntityOccupation.Barbarian);
            IEntityStats stats = new EntityStats(statValues);
            IEntitySkills skills = new EntitySkills(skillValues);
            IInventory inventory = new Inventory(60);
            IEntityAbility abilities = new EntityAbility(GeneralAbilities.All, ItemAbilities.None, EntityAbilities.ModifyInterationAbilities, EffectAbilities.ModifyMagicAbilities, AIAbilities.None);
            Guid id = Guid.NewGuid();
            IEntity entity = new Entity(id, info, skills, stats, inventory, abilities);
            IEntity entityClone = Serializer.DeepClone(entity);

            // to do add equality tests here
            Assert.Fail();
        }

    }
}

