using GameEngine.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine {
    public class EntityInventory {
        public List<Item> Inventory { get; private set; }
        public int SlotCount { get; private set; }
        
        public bool Contains(Item item) {
            return this.Inventory.Contains(item);
        }

        public bool Add(Item item) {
            if (item.ClassCode.HasFlag(ItemClassCode.Stackable)) {
                var found = this.Inventory.FirstOrDefault(i => i.CanAdd(item));
                if (found != null) {
                    found.Add(item);
                    return true;
                }
            }
            if (this.Inventory.Count < SlotCount) {
                this.Inventory.Add(item);
                return true;
            }
            return false;
        }

        public bool Remove(Item item) {
            return this.Inventory.Remove(item);
        }

        public EntityInventory(int slotCount) {
            this.Inventory = new List<Item>();
            this.SlotCount = slotCount;
        }
    }
}
