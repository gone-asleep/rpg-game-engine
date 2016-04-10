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
    public class ActionMove : ActionBase {
        public Vector2 UpdateDelta;
        public Vector2 InitialPosition;
        public Vector2 DestinationPosition;
       
        public ActionMove(Entity entity, Vector2 toLocation, float nextAvailableTime) : base() {
            this.InitialPosition = entity.FinalPosition;
            this.DestinationPosition = toLocation;
            Vector2 delta = this.DestinationPosition - this.InitialPosition;
            float timeTillEnd = entity.Stats.Get(StatType.Speed) / delta.Length();
            this.UpdateDelta = timeTillEnd * delta;
            this.StartTime = nextAvailableTime;
            this.EndTime = nextAvailableTime + (1.0f / timeTillEnd);
            this.TargetEntity = entity;
        }

        protected override void Do(float elapsedTime) {
            this.TargetEntity.Move(this.InitialPosition + (UpdateDelta * elapsedTime));
        }

        protected override void Finish() {
            this.TargetEntity.Move(this.InitialPosition + (UpdateDelta * this.TotalTime));
        }
    }
}
