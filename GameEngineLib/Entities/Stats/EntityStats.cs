using GameEngine.Entities.Skills;
using GameEngine.Entities.Stats;
using GameEngine.Global;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine {
    public interface IEntityStats {
        /// <summary>
        /// the Useable Skill points that can be assigned by the player
        /// </summary>
        float UseableSkillPoints { get; }

        /// <summary>
        /// Use this function to force a refresh of the stats when next accessed
        /// </summary>
        void MarkAsDirty();

        /// <summary>
        /// Adds a Modifier to the EntityStats collection
        /// </summary>
        /// <param name="stats">The Modifier to be applied</param>
        void ApplyModifier(StatModifier stats);

        /// <summary>
        /// Gets the current computed stat level
        /// </summary>
        /// <param name="i"></param>
        /// <returns>Computed Stat Level</returns>
        float Get(StatType i);

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
        float GetStatWithAppliedSkill(SkillType i, StatType type);

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
        bool DistributePoints(StatType type, float usedSkillPoints = 0);
    }

    public class EntityStats : IEntityStats {
        /// <summary>
        /// A list of Stat Modifiers currently applied to the entity
        /// </summary>
        private List<StatModifier> stats;

        /// <summary>
        /// The base stats for the entity
        /// base stats are the initial granted stats + any distributed skill points
        /// </summary>
        private float[] baseLineStats;

        /// <summary>
        /// The computed stats for the entity.
        /// These include the baseLine stats modified by any currently active stat modifiers
        /// </summary>
        private float[] computedStats;

        /// <summary>
        /// These are the skill stats for the current entity
        /// </summary>
        private float[] skillStats;

        /// <summary>
        /// The current skill points for the player
        /// </summary>
        private float skillPoints;

        /// <summary>
        /// Indicates that the computed stats will be recalculated on the next stat read
        /// </summary>
        private bool requiresRefresh;

        public float UseableSkillPoints {
            get {
                return (float)Math.Floor(skillPoints);
            }
        }

        /// <summary>
        /// Entity Stats Constructor
        /// </summary>
        /// <param name="skillTable">A complete table of skill levels placed in the order in which they appear in the SkillType enum</param>
        /// <param name="statTable">A complete table of stat levels placed in the order in which they appear in the StatType enum</param>
        public EntityStats(float[] skillTable = null, float[] statTable = null) {
            this.skillPoints = 0;
            this.requiresRefresh = false;
            this.stats = new List<StatModifier>();
            this.computedStats = new float[GameGlobal.StatTypeCount];
            this.baseLineStats = new float[GameGlobal.StatTypeCount];
            this.skillStats = new float[GameGlobal.SkillTypeCount];
            if (statTable != null) {
                Array.Copy(statTable, this.baseLineStats, this.baseLineStats.Length);
            }
            if (skillTable != null) {
                Array.Copy(skillTable, this.skillStats, this.skillStats.Length);    
            }
        }

        /// <summary>
        /// this is called whenever Stats are accessed by an outside process
        /// Stats will be recomputed if a change has occured since the last time
        /// accessed. 
        /// Note this should be wrapped in an if statement to determine if stats need to be refreshed before calling
        /// Optimize this by grouping things that modify stats, and things that read stats
        /// so that all modifications are done before a read
        /// </summary>
        private void Refresh() {
            Array.Copy(baseLineStats, computedStats, baseLineStats.Length);
            for (int i = 0; i < stats.Count; i++) { // columns
                if (stats[i].Applied == false) {
                    stats.RemoveAt(i); // remove the unapplied modifier
                    i--; // decrease the count so that we don't skip the next modifier
                    continue;
                } else {
                    for (int j = 0; j < computedStats.Length; j++) { // rows
                        switch (stats[i][j].Operation) {
                            case StatModifierOperation.Add:
                                computedStats[j] += stats[i][j].Value;
                                break;
                            case StatModifierOperation.Multiply:
                                computedStats[j] *= stats[i][j].Value;
                                break;
                            case StatModifierOperation.None:
                                break;
                        }
                    }
                }
            }
            this.requiresRefresh = false;
        }

        public void MarkAsDirty() {
            this.requiresRefresh = true;
        }

        public float Get(StatType i) {
            if (requiresRefresh) {
                this.Refresh();
            }
            return computedStats[(int)i];
        }

        public float Get(SkillType i) {
            return (float)Math.Floor(skillStats[(int)i]);
        }

        public float GetSkillProgress(SkillType i) {
            return skillStats[(int)i] - (float)Math.Floor(skillStats[(int)i]);
        }

        public float GetStatWithAppliedSkill(SkillType i, StatType type) {
            if (requiresRefresh) {
                this.Refresh();
            }
            return GameGlobal.CalculateSkillEffect(i, type, (float)Math.Floor(skillStats[(int)i]), computedStats[(int)type]);
        }

        public void AddSkillPoints(SkillType skill, float addedSkillPoints) {
            this.skillStats[(int)skill] += addedSkillPoints;
        }

        public bool DistributePoints(StatType type, float usedSkillPoints = 0) {
            bool success = false;
            if (this.UseableSkillPoints <= usedSkillPoints && usedSkillPoints > 0) {
                this.requiresRefresh = true;
                // reduce skill points
                this.skillPoints -= usedSkillPoints;
                // increase base line
                this.baseLineStats[(int)type] += usedSkillPoints;

                success = true;
            }
            return success;
        }

        public void ApplyModifier(StatModifier stats) {
            this.requiresRefresh = true;
            this.stats.Add(stats);
        }

        public override string ToString() {
            string debugStr = "\"Stats\":{";
            foreach (var value in Enum.GetValues(typeof(StatType))) {
                debugStr += "\"" + Enum.GetName(typeof(StatType), value) + "\":" + this.Get((StatType)value) +",";
            }
            foreach (var value in Enum.GetValues(typeof(SkillType))) {
                debugStr += "\"" + Enum.GetName(typeof(SkillType), value) + "\":" + this.Get((SkillType)value) + ",";
            }
            return debugStr + "}";
        }
    }
}