using GameEngine.Global;
using GameEngine.Items;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Actions {
    public class EntityActionEquip : EntityActionBase{
        
        Item equipedItem;

        public EntityActionEquip(Entity entity, Item item, float nextAvailableTime) {
            this.equipedItem = item;
            this.StartTime = nextAvailableTime;
            this.EndTime = nextAvailableTime + 1;
        }

        public override void Do(Entity entity, float elapsedTime) {
        }

        public override void Finish(Entity entity) {
            entity.Equip(equipedItem, true);
        }
    }
}
