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
    public class NPCStats : IEntityStats {
        /// <summary>
        /// A list of Stat Modifiers currently applied to the entity
        /// </summary>
        [ProtoMember(1)]
        private List<StatModifier> modifiers;

        /// <summary>
        /// This is the distribution of stat points
        /// the sum of this should be
        /// </summary>
        [ProtoMember(2)]
        public float[] baseStatsDistribution;

        /// <summary>
        /// The number of stat points to distribute over baseStatsDistribution
        /// </summary>
        [ProtoMember(3)]
        public int distributedStatPoints;

        /// <summary>
        /// The computed stats for the entity.
        /// These include the baseLine stats modified by any currently active stat modifiers
        /// </summary>
        private float[] computedStats;

        /// <summary>
        /// Indicates that the computed stats will be recalculated on the next stat read
        /// </summary>
        private bool requiresRefresh;



        public NPCStats() {

        }

        public NPCStats(int pointsToDistribute, float[] baseStatsDistribution) {
            this.baseStatsDistribution = new float[GameGlobal.StatTypeCount];
            this.computedStats = new float[GameGlobal.StatTypeCount];
            Array.Copy(baseStatsDistribution, this.baseStatsDistribution, this.baseStatsDistribution.Length);
            this.requiresRefresh = true;
            this.distributedStatPoints = pointsToDistribute;
            this.modifiers = new List<StatModifier>();
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
            // this part is different that the entityStats class. base values are calculated from a distribution 
            // instead of explicit value
            for (int j = 0; j < computedStats.Length; j++) {
                computedStats[j] = (float)Math.Floor(this.baseStatsDistribution[j] * this.distributedStatPoints);
            }
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

        public void ApplyModifier(StatModifier stats) {
            this.requiresRefresh = true;
            this.modifiers.Add(stats);
        }

        public float Get(Stats.StatType i) {
            if (requiresRefresh) {
                this.Refresh();
            }
            return computedStats[(int)i];
        }

        public void Add(Stats.StatType i, float value) {
            throw new NotImplementedException();
        }

        [ProtoAfterDeserialization]
        private void OnDeserialize() {
            // do not pass computed stats, instead recalculate them on first use
            this.computedStats = new float[GameGlobal.StatTypeCount];
            this.requiresRefresh = true;

            if (this.modifiers == null)
                modifiers = new List<StatModifier>();
        }
    }
}
