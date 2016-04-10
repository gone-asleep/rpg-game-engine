using GameEngine.Global;
using GameEngine.Items;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Actions {
    public class ActionEquip : ActionBase{
        Item equipedItem;

        public ActionEquip(Entity entity, Item item, float nextAvailableTime) {
            this.equipedItem = item;
            this.StartTime = nextAvailableTime;
            this.EndTime = nextAvailableTime + 1;
            this.TargetEntity = entity;
        }

        protected override void Do( float elapsedTime) {
        }

        protected override void Finish() {
            this.TargetEntity.Equip(equipedItem);
        }
    }
}
