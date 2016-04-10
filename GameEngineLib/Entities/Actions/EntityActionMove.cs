using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Entities.Stats;
using GameEngine.Global;

namespace GameEngine.Actions {
    public class EntityActionMove : EntityActionBase {
        public Vector2 UpdateDelta;
        public Vector2 InitialPosition;
        public Vector2 DestinationPosition;
       
        public EntityActionMove(Entity entity, Vector2 toLocation, float nextAvailableTime) : base() {
            this.InitialPosition = entity.FinalPosition;
            this.DestinationPosition = toLocation;
            Vector2 delta = this.DestinationPosition - this.InitialPosition;
            float timeTillEnd = entity.Stats.Get(StatType.Speed) / delta.Length();
            this.UpdateDelta = timeTillEnd * delta;
            this.StartTime = nextAvailableTime;
            this.EndTime = nextAvailableTime + (1.0f / timeTillEnd);
        }

        public override void Do(Entity entity, float elapsedTime) {
            entity.Move(this.InitialPosition + (UpdateDelta * elapsedTime), true);
        }

        public override void Finish(Entity entity) {
            entity.Move(this.InitialPosition + (UpdateDelta * this.TotalTime), true);
        }
    }
}
