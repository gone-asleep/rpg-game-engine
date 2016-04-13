using GameEngine.Global;
using GameEngine.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine {
    public interface IEntityInventory {
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

        bool SetEquiped(int inventoryIndex, IEntityStats stats);
        bool SetUnequiped(int equipIndex, IEntityStats stats);

    }

    public class EntityInventory : IEntityInventory {
        private IItem[] innerList;

        /// <summary>
        /// Equipment Applied to this entity
        /// </summary>
        private IItem[] equiped;

        public bool Contains(IItem item) {
            return this.innerList.Contains(item);
        }

        public bool SetUnequiped(int equipIndex, IEntityStats stats) {
            bool success = false;
            if (equiped[equipIndex] != null) {
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
            if (item != null && item.Info.Equipable) {
                int equipIndex = (int)item.Info.EquipType;

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
            IItem item2 = this.Get(index2);
            this.Set(item1, index2);
            this.Set(item2, index1);
            return true;
        }

        public bool Set(IItem item, int index) {
            bool success = false; 
            if (index >= 0) {
                if (innerList[index] != null) {
                    if (innerList[index].CanStack(item)) {
                        innerList[index].Stack(item);
                        success = true;
                    }
                } else { 
                    innerList[index] = item;
                    success = true;
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
                    } else if (innerList[i] != null && innerList[i].CanStack(item)) {
                        inventoryIndex = i;
                        success = true;
                        break;
                    }
                }
                if (success) {
                    if (innerList[inventoryIndex] != null) {
                        innerList[inventoryIndex].Stack(item);
                    } else {
                        innerList[inventoryIndex] = item;
                    }
                }
            }
            return success;
        }

        public IItem Get(int index) {
            IItem item = innerList[index];
            innerList[index] = null;
            return item;
        }

        public EntityInventory(int size) {
            this.innerList = new IItem[size];
            this.equiped = new Item[GameGlobal.EquipTypeCount];
        }
    }
}
