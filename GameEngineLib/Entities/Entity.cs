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
using System.Diagnostics;
using GameEngine.Entities.Stats;
using GameEngine.Entities;

namespace GameEngine {
    public interface IEntity {
        Guid ID { get; }
        float NextAvailableActionTime { get; }
        IEntityStats Stats { get; }
        IEntityInfo Info { get; }

        /// <summary>
        /// Adds a Move command to the entities Action Queue
        /// </summary>
        /// <param name="position">The new position of the entity</param>
        bool Move(Vector2 position);

        /// <summary>
        /// Adds a Equip command to the Entities Action Queue
        /// </summary>
        /// <param name="item">The item that will be added</param>
        bool Equip(IItem item);

        /// <summary>
        /// Adds a Unequip command to the Entities Action Queue
        /// </summary>
        /// <param name="item"></param>
        bool Unequip(IItem item);


        /// <summary>
        /// Adds an Item to the players inventory
        /// </summary>
        /// <param name="item">The Item to be recieved</param>
        /// <param name="immediate"></param>
        bool Receive(int inventoryIndex, IItem item);

        /// <summary>
        /// Distributes a quantity of skill points to a stat
        /// </summary>
        /// <param name="statType"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        bool DistributeSkillPoints(StatType statType, float amount);

        /// <summary>
        /// Removes an item from the players inventory, and adds it to another
        /// </summary>
        /// <param name="item">The item to be moved</param>
        /// <param name="toEntity">The entity recieving the item</param>
        /// <param name="immediate">Indicates if this is added to the action stack or is current</param>
        bool Give(int inventoryIndex, IEntity toEntity);
        void AddEffect(Effect effect);
    }

    public class Entity {
        /// <summary>
        /// The ID of this Entity
        /// </summary>
        public Guid ID { get; private set; }

        public Vector2 Position { get; set; }

        public Vector2 FinalPosition { get; set; }
        // currently used for queued actions
        public float FinalActionTime { get; set; }

        public float NextAvailableActionTime {
            get {
                return (FinalActionTime > GameGlobal.Time.Current) ? GameGlobal.Time.Current : FinalActionTime;
            }
            set {
                this.FinalActionTime = value;
            }
        }

        /// <summary>
        /// Current stats of this entity
        /// </summary>
        public IEntityStats Stats { get; private set; }

        /// <summary>
        /// Basic information about this entity
        /// </summary>
        public IEntityInfo Info { get; private set; }



        /// <summary>
        /// Effects Applied to this Entity
        /// </summary>
        private List<Effect> Effects { get; set; }

        /// <summary>
        /// Entities Inventory
        /// </summary>
        private EntityInventory Inventory { get; set; }

        public Entity(Guid id, IEntityInfo info, IEntityStats stats) {
            this.ID = id;
            this.Info = info;
            this.Position = new Vector2(0, 0);
            this.FinalPosition = new Vector2(0, 0);
            this.Stats = new EntityStats();
            
            this.Effects = new List<Effect>();
            this.Inventory = new EntityInventory(60);
        }

        /// <summary>
        /// Adds a Move command to the entities Action Queue
        /// </summary>
        /// <param name="position">The new position of the entity</param>
        public void Move(Vector2 position) {
            this.Position = position;
        }

        public bool DistributeSkillPoints(StatType statType, float amount) {
            if (!this.Stats.DistributePoints(statType, amount)) {
                Debug.WriteLine("Failed: Distributing {0} points to Stat {1},", amount, statType);
                return false;
            }
            return true;
        }


        public bool Equip(int inventoryIndex) {
            if (!this.Inventory.SetEquiped(inventoryIndex, this.Stats)) {
                Debug.WriteLine("Failed: Equiped Item at Inventory index {0},", inventoryIndex);
                return false;
            }
            return true;
        }


        public bool Unequip(int equipedIndex) {
            if (!this.Inventory.SetUnequiped(equipedIndex, this.Stats)) {
                Debug.WriteLine("Failed: Unequiped Item at Equipment index {0},", equipedIndex);
                return false;
            }
            return true;
        }


        public bool Receive(int inventoryIndex, IItem item) {
            if (!this.Inventory.Set(item, inventoryIndex)) {
                Debug.WriteLine("Failed: Unequiped Item at Equipment index {0},", inventoryIndex);
                return false;
            }
            return true;
        }


        public bool Give(int inventoryIndex, IEntity toEntity) {
            // check if the item is equiped, if so unequip
            IItem item = this.Inventory.Get(inventoryIndex);
            if (item != null) {
                // try to push to the reciever, if it fails add it back to our inventory
                if (!toEntity.Receive(-1, item)) {
                    Debug.WriteLine("Failed: Give Item at Equipment index {0}, Entity could not recieve", inventoryIndex);
                    this.Inventory.Set(item, inventoryIndex);
                    return false;
                }
            } else {
                Debug.WriteLine("Failed: Give Item at Equipment index {0}, item not in inventory", inventoryIndex);
                return false;
            }
            return true;
        }


        /// <summary>
        /// Refreshes the Entities state, Performs the current action in the action queue and 
        /// updates stats
        /// </summary>
        public void Refresh() {
            foreach (var effect in this.Effects) {
                if (effect.EndTime <= GameGlobal.Time.Current) {
                    effect.Modifier.Unapply(this.Stats);
                    this.Effects.Remove(effect);
                }
            }
        }

        public void AddEffect(Effect effect) {
            this.Effects.Add(effect);
            effect.Modifier.Unapply(this.Stats);
        }



        /// <summary>
        /// Draws the entity (Currently implemented as a square)
        /// </summary>
        public void Draw() {
        }

        public override string ToString() {
            string debugStr = this.Info.Name + "[" + this.Stats.ToString();

            
            return debugStr + "]";
        }
    }
}
