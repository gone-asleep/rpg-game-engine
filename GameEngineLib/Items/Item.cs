using GameEngine.Global;
using GameEngine.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Items {
    /// <summary>
    /// Game Items
    /// </summary>
    public interface IItem {
        /// <summary>
        /// Unique Identity for this item
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// The Quality of the item will determine 
        /// Worth, and Performance
        /// </summary>
        float Quality { get; }

        /// <summary>
        /// If the item is Stackable then Count can be > 1
        /// Else it is 1
        /// </summary>
        int Count { get;  }

        /// <summary>
        /// Common info for this item type
        /// </summary>
        IItemInfo Info { get; }

        /// <summary>
        /// A Modifier for this item
        /// </summary>
        IStatModifier Modifier { get; }

        /// <summary>
        /// Checks to see if an item can be grouped together with another
        /// </summary>
        /// <param name="item">Item to the grouped into this item</param>
        /// <returns>Value indicating Ability to stack</returns>
        bool CanStack(IItem item);
        void Stack(IItem item);
    }

    // count is not sufficient enough for security
    // this needs to be a list of guids
    public class Item : IItem {

        public Guid Id { get; private set; }

        public float Quality { get; private set; }

        public int Count { get; private set; }

        public IItemInfo Info { get; private set; }

        public IStatModifier Modifier { get; private set; }

        public bool CanStack(IItem item) {
            return (item.Info.Stackable && item.Id != this.Id && item.Info == this.Info);
        }



        public void Stack(IItem item) {
            if (this.CanStack(item)) {
                this.Count++;
                ((Item)item).Count--;
            }
        }

        public Item(Guid id, IItemInfo info, IStatModifier modifier) {
            this.Id = id;
            this.Count = 1;
            this.Info = info;
            this.Modifier = modifier;
        }

        public override string ToString() {
            return (this.Info.Name??this.Info.TypeCode.ToString()) + "{" + this.Modifier.ToString() + "}";
        }
    }
}
