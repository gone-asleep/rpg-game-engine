using GameEngine.Items;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Actions {
    public class EntityActionUnequip : EntityActionBase {
        Item equipedItem;

        public EntityActionUnequip(Entity entity, Item item, float nextAvailableTime) {
            this.equipedItem = item;
            this.StartTime = nextAvailableTime;
            this.EndTime = nextAvailableTime + 1;
        }

        public override void Do(Entity entity, float time) {
        }

        public override void Finish(Entity entity) {
            entity.Unequip(equipedItem, true);
        }
    }
}
