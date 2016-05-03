using GameEngine.Entities;
using GameEngine.Global;
using GameEngine.Items;
using GameEngine.Items.Info;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine {
    [ProtoContract]
    [ProtoInclude(100, typeof(Inventory))]
    public interface IInventory {
        /// <summary>
        /// Checks to see if the iniventory contains an item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool Contains(IItem item);

        /// <summary>
        /// Sets an item at index, if the item is placed on 
        /// A stackable item of the same sort then it is stacked
        /// Use index -1 to find the first available stacking then non-stacking spot
        /// </summary>
        /// <param name="item"></param>
        /// <param name="index"></param>
        bool Set(IItem item, InventorySlot index, IEntityStats stats);

        /// <summary>
        /// Gets an Item at Index and removes it from the inventory
        /// </summary>
        /// <param name="index">Index to place item</param>
        /// <returns>Item</returns>
        IItem Get(InventorySlot index, IEntityStats stats);

        /// <summary>
        /// Swaps whatever exists between two inventory locations
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        /// <returns></returns>
        bool Swap(InventorySlot index1, InventorySlot index2, IEntityStats stats);

        int Size { get; }

        int Remaining { get; }
    }

    [ProtoContract]
    public class Inventory : IInventory {
        [ProtoMember(1)]
        public int Size { get; private set; }

        [ProtoMember(2)]
        public int Remaining { get; private set; }

        /// <summary>
        /// The size of just the actual inventory 
        /// not the equip or wielded item spaces
        /// </summary>
        private int inventoryOffset;

        [ProtoMember(3)]
        private IItem[] innerList;

        public bool Contains(IItem item) {
            return this.innerList.Contains(item);
        }

        public bool Swap(InventorySlot primaryIndex, InventorySlot secondaryIndex, IEntityStats stats) {
            bool success = true;
            IItem primaryItem = this.Get(primaryIndex, stats);
            IItem secondaryItem = this.Get(secondaryIndex, stats);
            if (primaryItem != null) {
                success = this.Set(primaryItem, secondaryIndex, stats);
                if (success && secondaryItem != null) {
                    success = this.Set(secondaryItem, primaryIndex, stats);
                    if (!success) {
                        this.Get(secondaryIndex, stats); // take primary back out
                    }
                }
                if (!success) {
                    if (primaryItem != null) {
                        this.Set(primaryItem, primaryIndex, stats);
                    }
                    if (secondaryItem != null) {
                        this.Set(secondaryItem, secondaryIndex, stats);
                    }
                }
            } else {
                success = false;
            }
            // if we did not succeed
            
            return success;
        }

        private int GetEmptyIndex(IItem item, InventorySlot etype) {
            if (etype != InventorySlot.Any && etype != InventorySlot.AnyNonEquiped) {
                 if (innerList[(int)etype] != null) {
                     if (!(item.Info is IItemConsumableInfo) || !innerList[(int)etype].Same(item)) {
                         return -1;
                     }
                 }  
                return (int)etype;
            } else {
                if (etype == InventorySlot.Any) {
                    // try and equip it
                    if (item.Info is IEquipableItemInfo) {
                        InventorySlot equipSlot = ((IEquipableItemInfo)item.Info).EquipType;
                        if (innerList[(int)equipSlot] != null) {
                            return (int)equipSlot;
                        }
                    }
                    // wielding items are more complex so, I'm leaving it out for now...
                }
                if (item.Info is IItemConsumableInfo) {
                    for (int i = (int)InventorySlot.Inventory1; i < this.innerList.Length; i++) {
                        if (innerList[i] != null && innerList[i].Same(item)) {
                            return i;
                        }
                    }
                }
                for (int i = (int)InventorySlot.Inventory1; i < this.innerList.Length; i++) {
                    if (innerList[i] == null) {
                        return i;
                    }
                }
            }
            return -1;
        }

        public bool Set(IItem item, InventorySlot etype, IEntityStats stats) {
            bool success = false;
            int index = this.GetEmptyIndex(item, etype);
            // if we have a valid index and valid item
            if (item == null && index >= this.innerList.Length) {
                return false;
            }
            // if we are setting it to a unequiped location
            if (index > 11) {
                IItem existingItem = innerList[index];
                if (existingItem == null) {
                    innerList[index] = item;
                    success = true;
                    Remaining--;
                } else {
                    existingItem.Count += item.Count;
                    item.Count = 0;
                }
            } else if (etype == InventorySlot.WieldPrimary || etype == InventorySlot.WieldSecondary || etype == InventorySlot.WieldTwoHanded) {
                if (item.Info is IWieldableItemInfo) {
                    ItemWieldType itemWieldType = ((IWieldableItemInfo)item.Info).WieldType;
                    // unequip any dual wield item
                    var existingTwoHandedItem = this.Get(InventorySlot.WieldTwoHanded, stats);
                    if (existingTwoHandedItem != null) {
                        this.Set(existingTwoHandedItem, InventorySlot.AnyNonEquiped, stats);
                    }

                    if (itemWieldType == ItemWieldType.OneHand && etype != InventorySlot.WieldTwoHanded) {
                        // if the left hand is empty or we are able to unwield whats in it
                        var existing = this.Get(etype, stats);
                        if (existing != null) {
                            this.Set(existing, InventorySlot.AnyNonEquiped, stats);
                        }
                        innerList[index] = item;
                        success = true;
                    } else if (itemWieldType == ItemWieldType.BothHands && etype != InventorySlot.WieldTwoHanded) {
                        var existingPrimary = this.Get(InventorySlot.WieldPrimary, stats);
                        if (existingPrimary != null) {
                            this.Set(existingPrimary, InventorySlot.AnyNonEquiped, stats);
                        }
                        var existingSecondary = this.Get(InventorySlot.WieldSecondary, stats);
                        if (existingSecondary != null) {
                            this.Set(existingSecondary, InventorySlot.AnyNonEquiped, stats);
                        }
                        innerList[index] = item;
                        success = true;
                    }
                }
            } else {
                //equip code
                if (item.Info is IEquipableItemInfo) {
                    innerList[index] = item;
                    success = true;
                }
            }
            

            // if success apply the new modifiers
            if (success) {
                // apply new modifiers
                if (item.Modifier != null) {
                    item.Modifier.Apply(stats);
                }
            }

            return success;
        }


        public IItem Get(InventorySlot etype, IEntityStats stats) {
            int index = (int)etype;
            IItem item = innerList[index];
            if (index < this.innerList.Length && item != null) {
                if (index > 11) {
                    Remaining++;
                } else { // equiped item so remove modifier
                    if (innerList[index].Modifier != null) {
                        innerList[index].Modifier.Unapply(stats);
                    }
                }
                innerList[index] = null;
            }
            return item;
        }

        public Inventory() {

        }
        public Inventory(int inventorySize) {
            Debug.Assert(inventoryOffset <= 60);
            this.inventoryOffset = 61 - inventorySize;
            this.innerList = new IItem[GameGlobal.EquipTypeCount - (61 - inventorySize)];
            this.Size = inventorySize;
            this.Remaining = inventorySize;
        }

        [ProtoAfterDeserialization]
        private void OnDeserialize() {
            if (this.innerList == null)
                this.innerList = new IItem[this.Size];
        }

        public override string ToString() {
            string rtn = "{\"Equiped\":{";
            bool first = true;
            rtn += "},\"Inventory\":{";
            for (int i = 0; i < this.innerList.Length; i++) {
                if (this.innerList[i] != null) {
                    if (!first) {
                        rtn += ",";
                    }
                    rtn += "\"" + i + "\":" + this.innerList[i].ToString();
                    first = false;
                }
            }
            rtn += "}}";
            return rtn;
        }
    }
}