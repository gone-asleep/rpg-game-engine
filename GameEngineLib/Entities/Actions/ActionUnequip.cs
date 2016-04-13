using GameEngine.Items;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Actions {
    public class ActionUnequip : ActionBase {
        int EquipedIndex;

        public ActionUnequip(Entity entity, int equipedIndex, float nextAvailableTime) {
            this.EquipedIndex = equipedIndex;
            this.StartTime = nextAvailableTime;
            this.EndTime = nextAvailableTime + 1;
            this.TargetEntity = entity;
        }

        protected override void Do(float time) {
        }

        protected override void Finish() {
            this.TargetEntity.Unequip(EquipedIndex);
        }
    }
}
