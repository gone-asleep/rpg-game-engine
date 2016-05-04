using GameData;
using ProtoBuf;
using System;
using System.Collections.Generic;

namespace GameEngine {

    [ProtoContract]
    [ProtoInclude(100, typeof(EntityStats))]
    public interface IEntityStats {

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


        void Add(StatType i, float value);
    }

    [ProtoContract]
    public class EntityStats : IEntityStats {
        /// <summary>
        /// A list of Stat Modifiers currently applied to the entity
        /// </summary>
        [ProtoMember(1)]
        private List<StatModifier> modifiers;

        /// <summary>
        /// The base stats for the entity
        /// base stats are the initial granted stats + any distributed skill points
        /// </summary>
        [ProtoMember(2)]
        private float[] baseLineStats;

        /// <summary>
        /// The computed stats for the entity.
        /// These include the baseLine stats modified by any currently active stat modifiers
        /// </summary>
        private float[] computedStats;


        /// <summary>
        /// Indicates that the computed stats will be recalculated on the next stat read
        /// </summary>
        private bool requiresRefresh;

        public EntityStats() {

        }

        /// <summary>
        /// Entity Stats Constructor
        /// </summary>
        /// <param name="skillTable">A complete table of skill levels placed in the order in which they appear in the SkillType enum</param>
        /// <param name="statTable">A complete table of stat levels placed in the order in which they appear in the StatType enum</param>
        public EntityStats(float[] statTable = null) {
            this.requiresRefresh = false;
            this.modifiers = new List<StatModifier>();
            this.computedStats = new float[Globals.StatTypeCount];
            this.baseLineStats = new float[Globals.StatTypeCount];
            if (statTable != null) {
                Array.Copy(statTable, this.baseLineStats, this.baseLineStats.Length);
                this.requiresRefresh = true;
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
            for (int i = 0; i < modifiers.Count; i++) { // columns
                if (modifiers[i].Applied == false) {
                    modifiers.RemoveAt(i); // remove the unapplied modifier
                    i--; // decrease the count so that we don't skip the next modifier
                    continue;
                } else {
                    for (int j = 0; j < computedStats.Length; j++) { // rows
                        switch (modifiers[i][j].Operation) {
                            case StatModifierOperation.Add:
                                computedStats[j] += modifiers[i][j].Value;
                                break;
                            case StatModifierOperation.Multiply:
                                computedStats[j] *= modifiers[i][j].Value;
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

        public void Add(StatType i, float value) {
            computedStats[(int)i] += value;
            this.requiresRefresh = true;
        }

        public void ApplyModifier(StatModifier stats) {
            this.requiresRefresh = true;
            this.modifiers.Add(stats);
        }

        public override string ToString() {
            string debugStr = "\"Stats\":{";
            foreach (var value in Enum.GetValues(typeof(StatType))) {
                debugStr += "\"" + Enum.GetName(typeof(StatType), value) + "\":" + this.Get((StatType)value) +",";
            }
            return debugStr + "}";
        }

        [ProtoAfterDeserialization]
        private void OnDeserialize() {
            // do not pass computed stats, instead recalculate them on first use
            this.computedStats = new float[Globals.StatTypeCount];
            this.requiresRefresh = true;

            if (this.modifiers == null)
                modifiers = new List<StatModifier>();
        }
    }
}