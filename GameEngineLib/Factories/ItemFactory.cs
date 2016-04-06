using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine {
    public class ItemFactory {
        Dictionary<ItemTypeCode, Func<ItemFactoryTypeProfile, Item>> Factories { get; set; }

        public ItemFactory() {
            this.Factories = new Dictionary<ItemTypeCode, Func<ItemFactoryTypeProfile, Item>>();
        }

        public void AddFactoryConstructor(ItemTypeCode code, Func<ItemFactoryTypeProfile, Item> func) {
            this.Factories.Add(code, func);
        }

        public Item Generate(ItemTypeCode typeCode, ItemFactoryTypeProfile profile = new ItemFactoryTypeProfile()) {
            if (this.Factories.ContainsKey(typeCode)) {
                return Factories[typeCode](profile);
            }
            return null;
        }
    }

    public struct ItemFactoryTypeProfile {
        public int Level;

        public ItemFactoryTypeProfile(int level) {
            Level = level;
        }
    }
}
