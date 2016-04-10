using GameEngine.Items;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Actions {
    public class ActionUnequip : ActionBase {
        Item equipedItem;

        public ActionUnequip(Entity entity, Item item, float nextAvailableTime) {
            this.equipedItem = item;
            this.StartTime = nextAvailableTime;
            this.EndTime = nextAvailableTime + 1;
            this.TargetEntity = entity;
        }

        protected override void Do(float time) {
        }

        protected override void Finish() {
            this.TargetEntity.Unequip(equipedItem);
        }
    }
}
