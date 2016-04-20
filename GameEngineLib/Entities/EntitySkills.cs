using GameEngine.Entities.Skills;
using GameEngine.Entities.Stats;
using GameEngine.Global;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Entities {
    [ProtoContract]
    [ProtoInclude(1, typeof(EntitySkills))]
    public interface IEntitySkills {
        /// <summary>
        /// the Useable Skill points that can be assigned by the player
        /// </summary>
        float UseableSkillPoints { get; }

        float GetSkillPointProgress();

        /// <summary>
        /// Gets the current skill level of a given skill
        /// </summary>
        /// <param name="i">Skill in Inquiry</param>
        /// <returns>A whole number indicating the current level</returns>
        float Get(SkillType i);

        /// <summary>
        /// Gets a given stat with a skill bonus calculated into it
        /// </summary>
        /// <param name="i">Skill to be applied</param>
        /// <param name="type">Stat type to be returned</param>
        /// <returns>A number indicating the Modified stat when using the applied skill</returns>
        float GetStatWithAppliedSkill(SkillType i, StatType type, IEntityStats stats);

        /// <summary>
        /// Gets the current progress of a given skill
        /// </summary>
        /// <param name="i">skill in inquiry</param>
        /// <returns>A Number between 0 and 1 indicating the percent progress to the next level</returns>
        float GetSkillProgress(SkillType i);


        /// <summary>
        /// Adds to level of a skill
        /// this may follow the pattern of GetStatWithAppliedSkill an instead of asking the number of points to add
        /// it will be calculated by a function logated in GameGlobal
        /// </summary>
        /// <param name="skill">skill to be added to</param>
        /// <param name="addedSkillPoints">the number of points to be added</param>
        void AddSkillPoints(SkillType skill, float addedSkillPoints);

        /// <summary>
        /// Takes points from the unapplied skill points bucket and applies them (perminently) to the skill supplied
        /// </summary>
        /// <param name="type">The stat to apply the skill points to</param>
        /// <param name="usedSkillPoints">A whole numbered count of skill points <= UseableSkillPoints</param>
        bool DistributePoints(StatType type, IEntityStats stats, float usedSkillPoints = 0);
    }

    [ProtoContract]
    public class EntitySkills : IEntitySkills {
        /// <summary>
        /// These are the skill stats for the current entity
        /// </summary>
        [ProtoMember(1)]
        private float[] skillStats { get; set; }

        /// <summary>
        /// The current skill points for the player
        /// </summary>
        [ProtoMember(2)]
        private float skillPoints { get; set; }


        public float UseableSkillPoints {
            get {
                return (float)Math.Floor(skillPoints);
            }
        }

        public float GetSkillPointProgress() {
            return (float)Math.Round(skillPoints - Math.Floor(skillPoints), 2);
        }

        public EntitySkills() {

        }

        /// <summary>
        /// Entity Stats Constructor
        /// </summary>
        /// <param name="skillTable">A complete table of skill levels placed in the order in which they appear in the SkillType enum</param>
        /// <param name="statTable">A complete table of stat levels placed in the order in which they appear in the StatType enum</param>
        public EntitySkills(float[] skillTable = null) {
            this.skillPoints = 0;
            this.skillStats = new float[GameGlobal.SkillTypeCount];
            if (skillTable != null) {
                Array.Copy(skillTable, this.skillStats, this.skillStats.Length);
            }
        }

        public float Get(SkillType i) {
            return (float)Math.Floor(skillStats[(int)i]);
        }

        public float GetSkillProgress(SkillType i) {
            return (float)Math.Round(skillStats[(int)i] - Math.Floor(skillStats[(int)i]), 2);
        }

        public float GetStatWithAppliedSkill(SkillType i, StatType type, IEntityStats stats) {
            return GameGlobal.CalculateSkillEffect(i, type, (float)Math.Floor(skillStats[(int)i]), stats.Get(type));
        }

        public void AddSkillPoints(SkillType skill, float addedSkillPoints) {
            float currentLevel = this.Get(skill);
            this.skillStats[(int)skill] += addedSkillPoints;
            float newLevel = this.Get(skill);
            if (newLevel > currentLevel) {
                this.skillPoints += GameGlobal.CalculateSkillPointsMadeFromLevelingUpSkill(skill, newLevel);
            }
        }

        public bool DistributePoints(StatType type, IEntityStats stats, float usedSkillPoints = 0) {
            bool success = false;
            if (this.UseableSkillPoints <= usedSkillPoints && usedSkillPoints > 0) {
                // reduce skill points
                this.skillPoints -= usedSkillPoints;
                // increase base line

                stats.Add(type, usedSkillPoints);

                success = true;
            }
            return success;
        }

        public override string ToString() {
            string debugStr = "\"Stats\":{";
            foreach (var value in Enum.GetValues(typeof(SkillType))) {
                debugStr += "\"" + Enum.GetName(typeof(SkillType), value) + "\":" + this.Get((SkillType)value) + ",";
            }
            return debugStr + "}";
        }
    }
}
