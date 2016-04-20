using GameEngine.Entities;
using GameEngine.Global;
using GameEngine.Items;
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

        public bool Contains(IItem item) {
            return this.innerList.Contains(item);
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
                       
                        if (item.Info.Stackable && item.Same(existingItem)) {
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
                            if (!item.Info.Stackable) {
                                success = true;
                                break;
                            }
                        } else if (innerList[i] != null && item.Info.Stackable && innerList[i].Same(item) ) {
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
    }
}
