using GameEngine.Entities;
using GameEngine.Global;
using GameEngine.Items;
using GameEngine.Items.Info;
using ProtoBuf;
using System;
using System.Collections.Generic;
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
        bool Set(IItem item, int index);

        /// <summary>
        /// Gets an Item at Index and removes it from the inventory
        /// </summary>
        /// <param name="index">Index to place item</param>
        /// <returns>Item</returns>
        IItem Get(int index);

        /// <summary>
        /// Swaps whatever exists between two inventory locations
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        /// <returns></returns>
        bool Swap(int index1, int index2);

        /// <summary>
        /// Sets the 
        /// </summary>
        /// <param name="inventoryIndex"></param>
        /// <param name="stats"></param>
        /// <returns></returns>
        bool SetEquiped(int inventoryIndex, IEntityStats stats);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="equipIndex"></param>
        /// <param name="stats"></param>
        /// <returns></returns>
        bool SetUnequiped(int equipIndex, IEntityStats stats);

        /// <summary>
        /// Sets the 
        /// </summary>
        /// <param name="inventoryIndex"></param>
        /// <param name="stats"></param>
        /// <returns></returns>
        bool SetWielded(ItemWieldType wieldType, int inventoryIndex, IEntityStats stats);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="equipIndex"></param>
        /// <param name="stats"></param>
        /// <returns></returns>
        bool SetUnwielded(ItemWieldType wieldType, IEntityStats stats);


        int Size { get; }

        int Remaining { get; }

        bool Destroy(int inventoryIndex, int quantity);

    }

    [ProtoContract]
    public class Inventory : IInventory {
        [ProtoMember(1)]
        public int Size { get; private set; }
        
        [ProtoMember(2)]
        public int Remaining { get; private set; }
        
        [ProtoMember(3)]
        private IItem[] innerList;

        /// <summary>
        /// Equipment Applied to this entity
        /// </summary>
        [ProtoMember(4)]
        private IItem[] equiped;

        [ProtoMember(5)]
        public IItem wieldedLeftHand { get; private set; }

        [ProtoMember(6)]
        public IItem wieldedRightHand { get; private set; }

        public bool Contains(IItem item) {
            return this.innerList.Contains(item);
        }


        public bool SetUnwielded(ItemWieldType wieldType, IEntityStats stats) {
            bool success = false;

            if (Remaining > 0) {
                if (wieldType.HasFlag(ItemWieldType.LeftHand)) {
                    if (this.wieldedLeftHand != null && this.Set(this.wieldedLeftHand, -1)) {
                        // unapply left hand modifiers
                        if (wieldedLeftHand.Modifier != null) {
                            wieldedLeftHand.Modifier.Unapply(stats);
                        }
                        // remove right hand also if this is a dual wield item that is occupying both spaces
                        if (wieldedRightHand == wieldedLeftHand) {
                            wieldedRightHand = null;
                        }
                        // set left hand to empty
                        wieldedLeftHand = null;
                        success = true;
                    }
                } else if (wieldType.HasFlag(ItemWieldType.RightHand)) {
                    if (this.wieldedRightHand != null && this.Set(this.wieldedRightHand, -1)) {
                        // unapply right hand modifiers
                        if (wieldedRightHand.Modifier != null) {
                            wieldedRightHand.Modifier.Unapply(stats);
                        }
                        // remove left hand also if this is a dual wield item that is occupying both spaces
                        if (wieldedLeftHand == wieldedRightHand) {
                            wieldedLeftHand = null;
                        }
                        // set right hand to empty
                        wieldedRightHand = null;
                        success = true;
                    }
                }
            }

            return success;
        }

        public bool SetWielded(ItemWieldType wieldType, int inventoryIndex, IEntityStats stats) {
            bool success = false;
            IItem item = this.Get(inventoryIndex);
            if (item != null && item.Info is IWieldableItemInfo) {

                ItemWieldType itemWieldType = ((IWieldableItemInfo)item.Info).WieldType;

                // if an item is currently equiped in that spot
                // unequip it and add it to the inventory
                if (itemWieldType == ItemWieldType.OneHand) {
                    if (wieldType == ItemWieldType.LeftHand) {
                        // if the left hand is empty or we are able to unwield whats in it
                        if (wieldedLeftHand == null || this.SetUnwielded(ItemWieldType.LeftHand, stats)) {
                            wieldedLeftHand = item;
                            success = true;
                        }
                    } else if (wieldType == ItemWieldType.RightHand) {
                        if (wieldedRightHand == null || this.SetUnwielded(ItemWieldType.RightHand, stats)) {
                            wieldedRightHand = item;
                            success = true;
                        }
                    }
                   
                } else if (itemWieldType == ItemWieldType.BothHands) {
                    // if hands are empty or we are able to remove whats in them
                    if ((wieldedLeftHand == null && wieldedRightHand == null) || this.SetUnwielded(ItemWieldType.BothHands, stats)) {
                        wieldedLeftHand = item;
                        wieldedRightHand = item;
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
            }
            return success;
        }


        public bool SetUnequiped(int equipIndex, IEntityStats stats) {
            bool success = false;
             
            if (Remaining > 0 && equiped[equipIndex] != null) {
                IItem item = equiped[equipIndex];
                if (this.Set(item, -1)) {
                    if (equiped[equipIndex].Modifier != null) {
                        equiped[equipIndex].Modifier.Unapply(stats);
                    }
                    equiped[equipIndex] = null;
                    success = true;
                }
            }
   
            return success;
        }

        public bool SetEquiped(int inventoryIndex, IEntityStats stats) {
            bool success = false;
            IItem item = this.Get(inventoryIndex);
            if (item != null && item.Info is IEquipableItemInfo) {
                int equipIndex = (int)((IEquipableItemInfo)item.Info).EquipType;

                // if an item is currently equiped in that spot
                // unequip it and add it to the inventory
                if (equiped[equipIndex] != null) {

                    if (this.Set(equiped[equipIndex], -1)) {
                        // unapply an modifiers 
                        if (equiped[equipIndex].Modifier != null) {
                            equiped[equipIndex].Modifier.Unapply(stats);
                        }
                        // apply new modifiers
                        if (item.Modifier != null) {
                            item.Modifier.Apply(stats);
                        }
                        equiped[equipIndex] = item;
                        success = true;
                    }
                } else {
                    // apply modifier
                    if (item.Modifier != null) {
                        item.Modifier.Apply(stats);
                    }
                    equiped[equipIndex] = item;
                    success = true;
                }
            }
            return success;
        }

        public bool Swap(int index1, int index2) {
            IItem item1 = this.Get(index1);
            if (item1 != null) {
                IItem item2 = this.Get(index2);
                if (item2 != null) {
                    this.Set(item1, index2);
                    this.Set(item2, index1);
                } else {
                    this.Set(item1, index1);
                }
            } else {
                
            }
            return true;
        }

        public bool Set(IItem item, int index) {
            bool success = false;
            if (item != null) {
                if (index >= 0) {
                    IItem existingItem = innerList[index];
                
                    if (existingItem != null) {
                       
                        if ((item.Info is IItemConsumableInfo) && item.Same(existingItem)) {
                            existingItem.Count++;
                            item.Count--;
                            success = true;
                        }
                    } else {
                        innerList[index] = item;
                        success = true;
                        Remaining--;
                    }
                } else {
                    int inventoryIndex = -1;
                    for (int i = 0; i < innerList.Length; i++) {
                        if (innerList[i] == null && inventoryIndex == -1) {
                            inventoryIndex = i;
                            if (!(item.Info is IItemConsumableInfo)) {
                                success = true;
                                break;
                            }
                        } else if (innerList[i] != null && item.Info is IItemConsumableInfo && innerList[i].Same(item)) {
                            inventoryIndex = i;
                            success = true;
                            break;
                        }
                    }
                    if (success) {
                        if (innerList[inventoryIndex] != null) {
                            innerList[inventoryIndex].Count++;
                            item.Count--;
                        } else {
                            innerList[inventoryIndex] = item;
                            Remaining--;
                        }
                    }
                }
            }
            return success;
        }

        public bool Destroy(int inventoryIndex, int quantity) {
            bool success = false;
            // pull the item
            var item = this.Get(inventoryIndex);

            if (item.Count == quantity) {
                item.Count = 0; // item is destroyed don't do anything with the item to make it unuseable
                success = true;
            } else if (item.Count > quantity) {
                item.Count -= quantity; // some item remains
                this.Set(item, inventoryIndex);
                success = true;
            }

            return success;
        }

        public IItem Get(int index) {
            IItem item = innerList[index];
            if (item != null) {
                innerList[index] = null;
                Remaining++;
            }
            return item;
        }

        public Inventory() {

        }
        public Inventory(int size) {
            this.innerList = new IItem[size];
            this.equiped = new IItem[GameGlobal.EquipTypeCount];
            this.Size = size;
            this.Remaining = size;
        }

        [ProtoAfterDeserialization]
        private void OnDeserialize() {
            if (this.equiped == null)
                this.equiped = new IItem[GameGlobal.EquipTypeCount];
            if (this.innerList == null)
                this.innerList = new IItem[this.Size];

        }

        public override string ToString() {
            string rtn = "{\"Equiped\":{";
            bool first = true;
            for (int i = 0; i < equiped.Length; i++ ) {
                if (equiped[i] != null) {
                    if (!first) {
                        rtn += ",";
                    }
                    rtn += "\"" + ((ItemEquipType)i).ToString() + "\":" + equiped[i].ToString();
                    first = false;
                }
            }
            first = true;
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
