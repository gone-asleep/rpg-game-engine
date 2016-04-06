using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Entities.Actions {
    public class EntityActionAttack : EntityActionBase {
        Entity target;

        public EntityActionAttack(Entity entity, Entity target) {
            this.target = target;
            this.StartTime = entity.FinalActionTime;
            this.EndTime = entity.FinalActionTime + 1;
        }
    }
}
