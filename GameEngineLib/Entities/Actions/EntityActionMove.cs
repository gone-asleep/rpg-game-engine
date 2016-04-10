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
       
        public EntityActionMove(Entity entity, Vector2 toLocation) : base() {
            this.InitialPosition = entity.FinalPosition;
            this.DestinationPosition = toLocation;
            Vector2 delta = this.DestinationPosition - this.InitialPosition;
            float timeTillEnd = entity.Stats.Get(StatType.Speed) / delta.Length();
            this.UpdateDelta = timeTillEnd * delta;
            this.StartTime = entity.FinalActionTime;
            this.EndTime = entity.FinalActionTime + (1.0f / timeTillEnd);
        }

        public override void Update(Entity entity) {
            if (this.IsCurrent()) {
                float timeFromStart = 0.0f;
                if (GlobalLookup.CurrentTick >= this.EndTime) {
                    this.IsFinished = true;
                    timeFromStart = this.EndTime - this.StartTime;
                    //Debug.WriteLine("Finished Moving To {0}", entity.Position);
                } else {
                    timeFromStart = GlobalLookup.CurrentTick - this.StartTime;
                    //Debug.WriteLine("Finished Moving To {0}", entity.Position);
                }
                entity.Position = this.InitialPosition + (UpdateDelta * timeFromStart);
                
            } 
        }
    }
}
