using GameEngine.Global;
using GameEngine.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Items {
    public class Item {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public float Quality { get; private set; }
        public ItemTypeCode TypeCode { get; private set; }
        public ItemClassCode ClassCode { get; private set; }
        public int Count { get; private set; }
        public ItemEquipType EquipType { get; private set; }
        public StatModifier Modifier { get; private set; }
        public StatModifier EnchantmentModifier { get; private set; }

        public bool CanAdd(Item item) {
            if (item.ClassCode.HasFlag(ItemClassCode.Stackable)) {
                if (item.Id != this.Id && item.TypeCode == this.TypeCode) {
                    return true;
                }
            }
            return false;
        }

        public void Add(Item item) {
            if (this.CanAdd(item)) {
                this.Count++;
                item.Count--;
            }
        }

        public Item(string name, ItemTypeCode typeCode, ItemClassCode classCode, ItemEquipType equipType) {
            this.Name = name;
            this.Id = GlobalLookup.GetNextID();
            this.Count = 1;
            this.EquipType = equipType;
            this.ClassCode = classCode;
            this.TypeCode = typeCode;
            this.Modifier = new StatModifier();
        }

        public override string ToString() {
            return (this.Name??this.TypeCode.ToString()) + "{" + this.Modifier.ToString() + "}";
        }
    }
}
