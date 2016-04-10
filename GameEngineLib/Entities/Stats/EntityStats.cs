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
    public class EntityStats {
        private List<StatModifier> stats;
        private float[] baseLineStats;
        private float[] computedStats;
        private float[] skillLevelStats;
        private bool requiresRefresh = false;

        public EntityStats() {
            this.stats = new List<StatModifier>();
            this.computedStats = new float[GlobalLookup.StatCount];
            this.baseLineStats = new float[GlobalLookup.StatCount];
            this.skillLevelStats = new float[GlobalLookup.SkillCount];
        }

        public void Refresh() {
            if (this.requiresRefresh) {
                Array.Copy(baseLineStats, computedStats, baseLineStats.Length);
                for (int i = 0; i < stats.Count; i++) { // columns
                    for (int j = 0; j < computedStats.Length; j++) { // rows
                        switch (stats[i][j].Operation) {
                            case StatValueOp.Add:
                                computedStats[j] += stats[i][j].Value;
                                break;
                            case StatValueOp.Multiply:
                                computedStats[j] *= stats[i][j].Value;
                                break;
                            case StatValueOp.None:
                                break;
                        }
                    }
                }
                this.requiresRefresh = false;
            }
        }

        public float Get(StatType i) {
            if (requiresRefresh) {
                this.Refresh();
            }
            return computedStats[(int)i];
        }
        public float Get(SkillType i) {
            return skillLevelStats[(int)i];
        }

        public float Get(SkillType i, StatType type) {
            if (requiresRefresh) {
                this.Refresh();
            }
            return GlobalLookup.CalculateSkillEffect(i, type, skillLevelStats[(int)i], computedStats[(int)type]);
        }

        public void Set(StatType type, float amount) {
            this.requiresRefresh = true;
            this.baseLineStats[(int)type] = amount;
        }

        public void Set(SkillType type, float amount) {
            this.skillLevelStats[(int)type] = amount;
        }

        public void Increase(StatType type, float amount) {
            this.requiresRefresh = true;
            this.baseLineStats[(int)type] += amount;
        }

        public void Increase(SkillType type, float amount) {
            this.skillLevelStats[(int)type] += amount;
        }

        public void AddModifier(StatModifier stats) {
            this.requiresRefresh = true;
            this.stats.Add(stats);
        }

        public void RemoveModifier(StatModifier stats) {
            this.requiresRefresh = true;
            this.stats = this.stats.Where(i => i.Id != stats.Id).ToList();
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