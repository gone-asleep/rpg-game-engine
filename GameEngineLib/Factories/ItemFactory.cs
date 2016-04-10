using GameEngine.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Factories {
    public class ItemFactory {
        private Dictionary<ItemType, Func<ItemFactoryTypeProfile, Item>> Factories { get; set; }

        public ItemFactory() {
            this.Factories = new Dictionary<ItemType, Func<ItemFactoryTypeProfile, Item>>();
        }

        public void Add(ItemType code, Func<ItemFactoryTypeProfile, Item> func) {
            this.Factories.Add(code, func);
        }

        public Item Generate(ItemType typeCode, ItemFactoryTypeProfile profile = new ItemFactoryTypeProfile()) {
            if (this.Factories.ContainsKey(typeCode)) {
                return Factories[typeCode](profile);
            }
            return null;
        }
    }
}
