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
        int inventoryIndex;

        public ActionEquip(Entity entity, int inventoryIndex, float nextAvailableTime) {
            this.inventoryIndex = inventoryIndex;
            this.StartTime = nextAvailableTime;
            this.EndTime = nextAvailableTime + 1;
            this.TargetEntity = entity;
        }

        protected override void Do( float elapsedTime) {
        }

        protected override void Finish() {
            this.TargetEntity.Equip(inventoryIndex);
        }
    }
}
