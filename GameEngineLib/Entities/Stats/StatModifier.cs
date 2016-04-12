using GameEngine.Entities.Stats;
using GameEngine.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine {
    public interface IStatModifier {
        /// <summary>
        /// Indicates if this stat modification is currently being applied to the 'Calculated' EntityStat Collection
        /// Note that this could be out of date if the Stats need to be recalculated.
        /// </summary>
        bool Applied { get; }

        /// <summary>
        /// Unapplies the modifier to a entity status container
        /// and marks the container as dirty
        /// </summary>
        /// <param name="appliedStatContainer">The Entity Stats to remove Modification</param>
        void Unapply(IEntityStats appliedStatContainer);

        /// <summary>
        /// Unapply the modifier to a entity status container
        /// and marks the container as dirty
        /// </summary>
        /// <param name="appliedStatContainer">The Entity Stats to Modify</param>
        void Apply(IEntityStats appliedStatContainer);

        /// <summary>
        /// Returns the Stat Modifier Value and Operation for a given stat type
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        StatValueModifier this[StatType i] { get; }

        /// <summary>
        /// Returns the Stat Modifier Value and Operation for a given stat type
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        StatValueModifier this[int i] { get; }
    }

    public class StatModifier : IStatModifier {
        private StatValueModifier[] Stats { get; set; }

        public bool Applied { get; private set; }

        public void Apply(IEntityStats appliedStatContainer) {
            if (this.Applied == false) {
                this.Applied = true;
                appliedStatContainer.ApplyModifier(this);
            }
        }

        public void Unapply(IEntityStats appliedStatContainer) {
            if (this.Applied == true) {
                this.Applied = false;
                appliedStatContainer.MarkAsDirty();
            }
        }

        public StatValueModifier this[int i] {
            get { return Stats[i]; }
        }
        
        public StatValueModifier this[StatType i] {
            get { return Stats[(int)i]; }
        }

        public StatModifier(StatValueModifier[] statModificationTable = null) {
            this.Stats = new StatValueModifier[GameGlobal.StatTypeCount];
            if (statModificationTable != null) {
                Array.Copy(statModificationTable, this.Stats, this.Stats.Length);
            }
        }

        public override string ToString() {
            string debugStr = "";
            foreach (var name in Enum.GetNames(typeof(StatType))) {
                StatType type = (StatType)Enum.Parse(typeof(StatType), name);
                if (this[type].Operation != StatModifierOperation.None) {
                    debugStr += name + ":" + this[type].ToString() + ",  ";
                }
            }
            return debugStr;
        }
    }
}
