﻿using GameEngine;
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
using System.Diagnostics;
using GameEngine.Entities.Stats;

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

        public float NextAvailableActionTime {
            get {
                return (FinalActionTime > GlobalLookup.Time.Current) ? GlobalLookup.Time.Current : FinalActionTime;
            }
        }

        /// <summary>
        /// Current stats of this entity
        /// </summary>
        public EntityStats Stats { get; private set; }


        /// <summary>
        /// Equipment Applied to this entity
        /// </summary>
        private Item[] Equiped { get; set; }

        /// <summary>
        /// Effects Applied to this Entity
        /// </summary>
        private List<Effect> Effects { get; set; }

        /// <summary>
        /// Entities Inventory
        /// </summary>
        private EntityInventory Inventory { get; set; }

        /// <summary>
        /// Entities currently Queued Actions
        /// </summary>
        private Queue<EntityActionBase> Actions { get; set; }


        public Entity(string name) {
            this.ID = GlobalLookup.IDs.Next();
            this.Name = name;
            this.Position = new Vector2(0, 0);
            this.FinalPosition = new Vector2(0, 0);
            this.Stats = new EntityStats();
            this.Equiped = new Item[GlobalLookup.EquipTypeCount];
            this.Effects = new List<Effect>();
            this.Inventory = new EntityInventory(60);
            this.Actions = new Queue<EntityActionBase>();


        }

        /// <summary>
        /// Adds a Move command to the entities Action Queue
        /// </summary>
        /// <param name="position">The new position of the entity</param>
        public void Move(Vector2 position, bool immediate) {
            if (immediate) {
                this.Position = position;
            } else {
                var action = new EntityActionMove(this, position, NextAvailableActionTime);
                this.FinalActionTime = action.EndTime;
                this.FinalPosition = position;
                this.Actions.Enqueue(action);
            }
        }

        /// <summary>
        /// Adds a Equip command to the Entities Action Queue
        /// </summary>
        /// <param name="item">The item that will be added</param>
        public void Equip(Item item, bool immediate) {
            if (immediate) {
                // check that the entity has the item in inventory 
                if (!this.Inventory.Contains(item)) {
                    Debug.WriteLine("Failed: Unequiped Item {0}, Not Present in inventory", item.Name);
                    return;
                }
                // check that the item is currently equiped
                if (this.Equiped[(int)item.EquipType] != null) {
                    Debug.WriteLine("Failed: Unequiped Item {0}, Item Currently Equiped to Slot", item.Name);
                    return;
                }

                this.Stats.AddModifier(item.Modifier);
                if (item.EnchantmentModifier != null) {
                    Stats.AddModifier(item.EnchantmentModifier);
                }
                this.Equiped[(int)item.EquipType] = item;
            } else {
                var action = new EntityActionUnequip(this, item, NextAvailableActionTime);
                this.FinalActionTime = action.EndTime;
                this.Actions.Enqueue(action);
            }
        }

        /// <summary>
        /// Adds a Unequip command to the Entities Action Queue
        /// </summary>
        /// <param name="item"></param>
        public void Unequip(Item item, bool immediate) {
            if (immediate) {
                // check that the entity has the item in inventory 
                if (!this.Inventory.Contains(item)) {
                    Debug.WriteLine("Failed: Unequiped Item {0}, Not Present in inventory", item.Name);
                    return;
                }
                // check that the item is currently equiped
                if (this.Equiped[(int)item.EquipType] != item) {
                    Debug.WriteLine("Failed: Unequiped Item {0}, Not Currently Equiped", item.Name);
                    return;
                }

                this.Stats.RemoveModifier(item.Modifier);
                if (item.EnchantmentModifier != null) {
                    Stats.RemoveModifier(item.EnchantmentModifier);
                }
                this.Equiped[(int)item.EquipType] = null;
            } else {
                var action = new EntityActionUnequip(this, item, NextAvailableActionTime);
                this.FinalActionTime = action.EndTime;
                this.Actions.Enqueue(action);
            }
        }


        /// <summary>
        /// Adds an Item to the players inventory
        /// </summary>
        /// <param name="item">The Item to be recieved</param>
        /// <param name="immediate"></param>
        public void Receive(Item item, bool immediate) {
            if (immediate) {
                this.Inventory.Add(item);
            } else {
                
            }
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

            foreach (var item in this.Equiped) {
                if (item != null) {
                    debugStr += item.EquipType + ":" + item + ",  ";
                }
            }
            
            return debugStr + "]";
        }
    }
}
