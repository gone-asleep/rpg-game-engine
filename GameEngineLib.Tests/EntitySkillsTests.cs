using GameEngine;
using GameEngine.Entities;
using GameEngine.Entities.Skills;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GameEngineLib.Tests {

    [TestClass]
    public class EntitySkillTests {
        private float[] statValues = new float[] { /*strength*/1.0f, /*Stamina*/2.0f, /*Wisdom*/3.0f, /*Inteligence*/4.0f, /*Charisma*/5.0f, /*Agility*/6.0f, /*Luck*/7.0f, /*Speed*/8.0f };
        private float[] skillValues = new float[] { 
            /*LightArmor*/1.0f, /*HeavyArmor*/2.0f, /*LightBlade*/3.0f, 
            /*HeavyBlade*/4.0f, /*BluntWeapon*/5.0f, /*Sneak*/6.0f, 
            /*Pickpocket*/7.0f, /*LockPicking*/8.0f, /*RangeWeapon*/9.0f,
            /*PolearmWeapon*/7.0f, /*AxeWeapon*/8.0f, /*FlailWeapon*/9.0f,
            /*StaffWeapon*/7.0f, /*HammerWeapon*/8.0f
        };
               

        [TestMethod]
        public void SkillsCreate() {
            IEntitySkills skills = new EntitySkills();
        }

        [TestMethod]
        public void SkillsCreateWithValues() {
            try {
                IEntitySkills skills = new EntitySkills(skillValues);

                Assert.AreEqual(skills.Get(SkillType.LightArmor), 1.0f);
                Assert.AreEqual(skills.Get(SkillType.HeavyArmor), 2.0f);
                Assert.AreEqual(skills.Get(SkillType.LightBlade), 3.0f);
                Assert.AreEqual(skills.Get(SkillType.HeavyBlade), 4.0f);
                Assert.AreEqual(skills.Get(SkillType.BluntWeapon), 5.0f);
                Assert.AreEqual(skills.Get(SkillType.Sneak), 6.0f);
                Assert.AreEqual(skills.Get(SkillType.Pickpocket), 7.0f);
                Assert.AreEqual(skills.Get(SkillType.LockPicking), 8.0f);
            } catch {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void SkillsLeveling() {
            try {
                IEntitySkills skills = new EntitySkills(skillValues);

                Assert.AreEqual(skills.Get(SkillType.Pickpocket), 7.0f);

                // the player uses the pickpocket skill and a small amount of experience is deposited
                skills.AddSkillPoints(SkillType.Pickpocket, 0.05f);
                Assert.AreEqual(skills.Get(SkillType.Pickpocket), 7.0f);
                Assert.AreEqual(skills.GetSkillProgress(SkillType.Pickpocket), 0.05f);

                // this happens continuously and here we add the sum of all the skill uses
                skills.AddSkillPoints(SkillType.Pickpocket, 0.95f);
                Assert.AreEqual(skills.Get(SkillType.Pickpocket), 8.0f, "The Skill did not reach level 8");
                Assert.AreEqual(skills.GetSkillProgress(SkillType.Pickpocket), 0.0f, "The Skill progress did not reach 0% of level 8");
                Assert.IsTrue(skills.GetSkillPointProgress() > 0, "Skill Point Progress was not added into stats after leveling");
            } catch {
                Assert.Fail();
            }
        }
    }

}
