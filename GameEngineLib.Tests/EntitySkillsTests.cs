using GameEngine;
using GameEngine.Entities;
using GameEngine.Entities.Skills;
using GameEngine.Global;
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
        private float[] skillValues;

        [TestInitialize]
        public void Setup() {
            skillValues = new float[GameGlobal.SkillTypeCount];
            for (int i = 0; i < skillValues.Length; i++) {
                skillValues[i] = (float)i;
            }
        }

        [TestMethod]
        public void SkillsCreate() {
            IEntitySkills skills = new EntitySkills();
        }

        [TestMethod]
        public void SkillsCreateWithValues() {
            try {
                IEntitySkills skills = new EntitySkills(skillValues);

                Assert.AreEqual(skills.Get(SkillType.LightArmor), skillValues[(int)SkillType.LightArmor]);
                Assert.AreEqual(skills.Get(SkillType.HeavyArmor), skillValues[(int)SkillType.HeavyArmor]);
                Assert.AreEqual(skills.Get(SkillType.LightBlade), skillValues[(int)SkillType.LightBlade]);
                Assert.AreEqual(skills.Get(SkillType.HeavyBlade), skillValues[(int)SkillType.HeavyBlade]);
                Assert.AreEqual(skills.Get(SkillType.BluntWeapon), skillValues[(int)SkillType.BluntWeapon]);
                Assert.AreEqual(skills.Get(SkillType.Sneak), skillValues[(int)SkillType.Sneak]);
                Assert.AreEqual(skills.Get(SkillType.Pickpocket), skillValues[(int)SkillType.Pickpocket]);
                Assert.AreEqual(skills.Get(SkillType.LockPicking), skillValues[(int)SkillType.LockPicking]);
            } catch {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void SkillsLeveling() {
            try {
                IEntitySkills skills = new EntitySkills(skillValues);

                Assert.AreEqual(skills.Get(SkillType.Pickpocket), skillValues[(int)SkillType.Pickpocket]);

                // the player uses the pickpocket skill and a small amount of experience is deposited
                skills.AddSkillPoints(SkillType.Pickpocket, 0.05f);
                Assert.AreEqual(skills.Get(SkillType.Pickpocket), skillValues[(int)SkillType.Pickpocket]);
                Assert.AreEqual(skills.GetSkillProgress(SkillType.Pickpocket), 0.05f);

                // this happens continuously and here we add the sum of all the skill uses
                skills.AddSkillPoints(SkillType.Pickpocket, 0.95f);
                Assert.AreEqual(skills.Get(SkillType.Pickpocket), skillValues[(int)SkillType.Pickpocket]+1, "The Skill did not reach level 8");
                Assert.AreEqual(skills.GetSkillProgress(SkillType.Pickpocket), 0.0f, "The Skill progress did not reach 0% of level 8");
                Assert.IsTrue(skills.GetSkillPointProgress() > 0, "Skill Point Progress was not added into stats after leveling");
            } catch {
                Assert.Fail();
            }
        }
    }

}
