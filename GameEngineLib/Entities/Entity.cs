using GameData;
using GameData.Info;
using GameEngine.Effects;
using GameEngine.Entities;
using GameEngine.Global;
using GameEngine.Items;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameEngine {
    [ProtoContract]
    [ProtoInclude(100, typeof(Entity))]
    public interface IEntity {
        Guid ID { get; }

        IEntityStats Stats { get; }

        IEntityInfo Info { get; }

        float Health { get; }

        /// <summary>
        /// Entities Inventory
        /// </summary>
        IInventory Inventory { get; }

        IEntityAbility Abilities { get; }

        // Note: this could be moved up into global
        // entities could be treated just like data elements
        bool PerformAction(IGlobal global, ActionParameters parameter);

        void AddEffect(Effect effect);

        void Refresh(IGlobal global);
    }

    [ProtoContract]
    public class Entity : IEntity {
        /// <summary>
        /// The ID of this Entity
        /// </summary>
        [ProtoMember(1)]
        public Guid ID { get; private set; }

        [ProtoMember(2)]
        public float Health { get; private set; }

        [ProtoMember(3)]
        public float MaxHealth { get; private set; }

        [ProtoMember(4)]
        public string Name { get; private set; }



        /// <summary>
        /// Current stats of this entity
        /// </summary>
        [ProtoMember(5)]
        public IEntityStats Stats { get; private set; }

        /// <summary>
        /// Current Skills of this entity (skill stat calculations)
        /// </summary>
        [ProtoMember(6)]
        public IEntitySkills Skills { get; private set; }

        /// <summary>
        /// Basic information about this entity
        /// </summary>
        [ProtoMember(7)]
        public IEntityInfo Info { get; private set; }

        /// <summary>
        /// Entities Inventory
        /// </summary>
        [ProtoMember(8)]
        public IInventory Inventory { get; private set; }

        /// <summary>
        /// Entity Abilities used to check if a entity can perform an action/skill
        /// </summary>
        [ProtoMember(9)]
        public IEntityAbility Abilities { get; private set; }


        #region Toying with the idea of a system where the player appears a certain way which then determines how entities may act in return

        [ProtoMember(10)]
        public float AppearanceOfWealth { get; private set; }

        [ProtoMember(11)]
        public float AppearanceOfIntellect { get; private set; }

        [ProtoMember(12)]
        public float AppearanceOfHonesty { get; private set; }

        [ProtoMember(13)]
        public float AppearanceOfHostility { get; private set; }

        [ProtoMember(14)]
        public float AppearanceOfPower { get; private set; }

        #endregion

        /// <summary>
        /// Effects Applied to this Entity
        /// </summary>
        private List<Effect> Effects { get; set; }

        public Entity() {
        }

        public Entity(Guid id, IEntityInfo info, IEntitySkills skills, IEntityStats stats, IInventory inventory, IEntityAbility abilities) {
            Debug.Assert(info != null, "Info cannot be null");
            Debug.Assert(stats != null, "Stats cannot be null");
            Debug.Assert(inventory != null, "inventory cannot be null");
            Debug.Assert(id != null && id != Guid.Empty, "invalid ID");

            this.ID = id;
            this.Stats = stats;
            this.Skills = skills;
            this.Info = info;
            this.Inventory = inventory;
            this.Abilities = abilities;
            this.MaxHealth = this.Stats.Get(StatType.Constitution) * 10;
            this.Health = this.MaxHealth;
            this.Effects = new List<Effect>();
        }

        public bool PerformAction(IGlobal global, ActionParameters parameter) {
            if (!this.Abilities.Check(parameter.action)) {
                Debug.WriteLine("Failed: Entity does not have ability to Receive");
                return false;
            } else {
                bool success = false;
                // retrieve the item
                if (parameter.action == GeneralAbilities.Receive) {
                    var item = global.World.GetItem(parameter.itemID);
                    if (item != null) {
                        success = (this.Inventory.Set(item, (InventorySlot)parameter.targetIndex, this.Stats) || this.Inventory.Set(item, (InventorySlot)(-1), this.Stats));
                    }
                } else if (parameter.action == GeneralAbilities.Destroy) {
                    var item = this.Inventory.Get((InventorySlot)parameter.targetIndex, this.Stats);
                    if (item != null) {
                        if (parameter.targetQuantity <= item.Count) {
                            item.Count -= (int)parameter.targetQuantity;
                            if (item.Count != 0) {
                                this.Inventory.Set(item, (InventorySlot)parameter.targetIndex, this.Stats);
                            }
                            success = true;
                        }
                    }
                } else if (parameter.action == GeneralAbilities.Give) {
                    var item = this.Inventory.Get((InventorySlot)parameter.targetIndex, this.Stats);
                    if (item != null) {
                        success = true;
                    }
                }else if (parameter.action == GeneralAbilities.DistributeSkillPoints) {
                    success = this.Skills.DistributePoints((StatType)parameter.targetIndex, this.Stats, parameter.targetQuantity);
                } else if (parameter.action == GeneralAbilities.MoveInventory) {
                    //this.Position = parameter.position;
                    success = this.Inventory.Swap((InventorySlot)parameter.targetIndex, (InventorySlot)parameter.targetIndex2, this.Stats);
                } else if (parameter.action == GeneralAbilities.TakeDamage) {
                    success = this.AdjustHealth(parameter.targetQuantity);
                } else if (parameter.action == GeneralAbilities.GiveDamage) {
                    success = true; // do nothing? not sure
                } else if (parameter.action == GeneralAbilities.Use) {
                    IItem item = this.Inventory.Get((InventorySlot)parameter.targetIndex, this.Stats);
                    // somehow an item is used
                    if (item != null) {
                        success = true; // for now
                    }
                }
                if (!success){
                    Debug.WriteLine("Failed to perform action {0}", parameter.action); 
                }
                return success;
            }
        }

        public bool AdjustHealth(float amount) {
            this.Health += amount;
            if (this.Health > MaxHealth) {
                this.Health = MaxHealth;
            } else if (this.Health < 0) {
                this.Health = 0;
            }
            return true;
        }

        /// <summary>
        /// Refreshes the Entities state, Performs the current action in the action queue and 
        /// updates stats
        /// </summary>
        public void Refresh(IGlobal global) {
            foreach (var effect in this.Effects) {
                if (effect.EndTime <= global.Time.Current) {
                    effect.Modifier.Unapply(this.Stats);
                    this.Effects.Remove(effect);
                }
            }
        }

        public void AddEffect(Effect effect) {
            this.Effects.Add(effect);
            effect.Modifier.Unapply(this.Stats);
        }

        public override string ToString() {
            string debugStr = this.Info.Name + "[" + this.Stats.ToString();

            
            return debugStr + "]";
        }
    }
}
