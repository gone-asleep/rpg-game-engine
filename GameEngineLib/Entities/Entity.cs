using GameEngine;
using GameEngine.Actions;
using GameEngine.Effects;
using System.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Items;
using GameEngine.Global;

namespace GameEngine {
    public class Entity {
        /// <summary>
        /// The ID of this Entity
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// This is the name of the entity 
        /// Note: not all entities have names, and it is not a
        /// usefull piece of data for most entity opperations
        /// this could move to another location as a look up
        /// </summary>
        public string Name { get; private set; }

        public Vector2 Position { get; set; }
        public Vector2 FinalPosition { get; set; }
        // currently used for queued actions
        public float FinalActionTime { get; set; }

        /// <summary>
        /// Current stats of this entity
        /// </summary>
        public EntityStats Stats { get; private set; }

        /// <summary>
        /// Equipment Applied to this entity
        /// </summary>
        public Dictionary<ItemEquipType, Item> Equiped { get; private set; }

        /// <summary>
        /// Effects Applied to this Entity
        /// </summary>
        public List<Effect> Effects { get; private set; }

        /// <summary>
        /// Entities Inventory
        /// </summary>
        public EntityInventory Inventory { get; private set; }

        /// <summary>
        /// Entities currently Queued Actions
        /// </summary>
        public Queue<EntityActionBase> Actions { get; private set; }


        public Entity(string name) {
            this.ID = GlobalLookup.IDs.Next();
            this.Name = name;
            this.Position = new Vector2(0, 0);
            this.FinalPosition = new Vector2(0, 0);
            this.Stats = new EntityStats();
            this.Equiped = new Dictionary<ItemEquipType, Item>();
            this.Effects = new List<Effect>();
            this.Inventory = new EntityInventory(60);
            this.Actions = new Queue<EntityActionBase>();
        }

        /// <summary>
        /// Adds a Move command to the entities Action Queue
        /// </summary>
        /// <param name="position">The new position of the entity</param>
        public void Move(Vector2 position) {
            if (FinalActionTime < GlobalLookup.Time.Current) {
                FinalActionTime = GlobalLookup.Time.Current;
            }
            var action = new EntityActionMove(this, position);
            this.FinalActionTime = action.EndTime;
            this.FinalPosition = action.DestinationPosition;
            this.Actions.Enqueue(action);
        }

        /// <summary>
        /// Adds a Equip command to the Entities Action Queue
        /// </summary>
        /// <param name="item">The item that will be added</param>
        public void Equip(Item item) {
            if (FinalActionTime < GlobalLookup.Time.Current) {
                FinalActionTime = GlobalLookup.Time.Current;
            }
            var action = new EntityActionUnequip(this, item);
            this.FinalActionTime = action.EndTime;
            this.Actions.Enqueue(action);
        }

        /// <summary>
        /// Adds a Unequip command to the Entities Action Queue
        /// </summary>
        /// <param name="item"></param>
        public void Unequip(Item item) {
            if (FinalActionTime < GlobalLookup.Time.Current) {
                FinalActionTime = GlobalLookup.Time.Current;
            }
            var action = new EntityActionUnequip(this, item);
            this.FinalActionTime = action.EndTime;
            this.Actions.Enqueue(action);
        }

        /// <summary>
        /// Refreshes the Entities state, Performs the current action in the action queue and 
        /// updates stats
        /// </summary>
        public void Refresh() {
            foreach (var effect in this.Effects) {
                if (effect.EndTime <= GlobalLookup.Time.Current) {
                    this.Stats.RemoveModifier(effect.Modifier);
                    this.Effects.Remove(effect);
                }
            }
            this.Stats.Refresh();
            if (this.Actions.Count > 0) {
                var currentAction = this.Actions.Peek();
                currentAction.Update(this);
                if (currentAction.IsFinished) {
                    this.Actions.Dequeue();
                }
            }
        }

        public void AddEffect(Effect effect) {
            this.Effects.Add(effect);
            this.Stats.AddModifier(effect.Modifier);
        }



        /// <summary>
        /// Draws the entity (Currently implemented as a square)
        /// </summary>
        public void Draw() {
        }

        public override string ToString() {
            string debugStr = this.Name + "[" + this.Stats.ToString();
            
            foreach (var name in Enum.GetNames(typeof(ItemEquipType))) {
                ItemEquipType equipType = (ItemEquipType)Enum.Parse(typeof(ItemEquipType), name);
                if (this.Equiped.ContainsKey(equipType)) {
                    debugStr += name + ":" + this.Equiped[equipType] + ",  ";

                }
            }
            return debugStr + "]";
        }
    }
}
