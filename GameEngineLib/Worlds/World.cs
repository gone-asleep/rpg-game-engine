using GameEngine.Actions;
using GameEngine.Items;
using GameEngine.Worlds;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine {
    public class World {

        public List<Entity> Entities { get; set; }

        public List<Item> Items { get; set; }
        
        public SortedList Actions { get; set; }
        
        public Map Map { get; private set; }
        
        public World() {
            this.Entities = new List<Entity>();
            this.Items = new List<Item>();
            this.Actions = new SortedList();
        }



        public void AddMoveAction(Entity targetEntity, Vector2 toPosition, float time) {
            if (time == 0) {
                time = targetEntity.NextAvailableActionTime;
            } 

            var action = new ActionMove(targetEntity, toPosition, time);
            
            targetEntity.FinalPosition = toPosition;

            if (action.EndTime > targetEntity.NextAvailableActionTime) {
                targetEntity.NextAvailableActionTime = action.EndTime;
            }
        }
        
        public void AddUnequipAction(Entity targetEntity, Item item, float time) {
            if (time == 0) {
                time = targetEntity.NextAvailableActionTime;
            }
            var action = new ActionUnequip(targetEntity, item, time);

            this.Actions.Add(action.StartTime, action);

            if (action.EndTime > targetEntity.NextAvailableActionTime) {
                targetEntity.NextAvailableActionTime = action.EndTime;
            }
        }

        public void AddEquipAction(Entity targetEntity, Item item, float time) {
            if (time == 0) {
                time = targetEntity.NextAvailableActionTime;
            }
            var action = new ActionUnequip(targetEntity, item, time);

            this.Actions.Add(action.StartTime, action);

            if (action.EndTime > targetEntity.NextAvailableActionTime) {
                targetEntity.NextAvailableActionTime = action.EndTime;
            }
            
        }

        public void SetMap(Map map) {
            this.Map = map;
        }

        public void Refresh(float currentTime) {
            foreach (var entity in Entities) {
                entity.Refresh();
            }
            foreach (var action in this.Actions) {
                var c_action = (ActionBase)action;
                if (c_action.IsFinished) {
                    continue;
                } else if (c_action.EndTime < currentTime) {
                    c_action.FinishAction(currentTime);
                    continue;
                } else if (c_action.StartTime > currentTime) {
                    break;
                } else {
                    c_action.UpdateAction(currentTime);
                } 
            }
        }

        public void Draw() {
            this.Map.Draw();
            foreach (var entity in Entities) {
                entity.Draw();
            }
        }
    }
}
