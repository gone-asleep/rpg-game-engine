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
        public float Quality { get; private set; }
        public int Count { get; private set; }
        public IItemInfo Info { get; private set; }
        public IStatModifier Modifier { get; private set; }

        public bool CanStack(Item item) {
            if (item.Info.Stackable) {
                if (item.Id != this.Id && item.Info.TypeCode == this.Info.TypeCode) {
                    return true;
                }
            }
            return false;
        }

        public void Stack(Item item) {
            if (this.CanStack(item)) {
                this.Count++;
                item.Count--;
            }
        }

        public Item(IItemInfo info, IStatModifier modifier) {
            this.Id = GameGlobal.IDs.Next();
            this.Count = 1;
            this.Info = info;
            this.Modifier = modifier;
        }

        public override string ToString() {
            return (this.Info.Name??this.Info.TypeCode.ToString()) + "{" + this.Modifier.ToString() + "}";
        }
    }
}
