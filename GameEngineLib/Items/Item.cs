using GameEngine.Global;
using GameEngine.Items;
using GameEngine.Items.Info;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Items {
    /// <summary>
    /// Game Items
    /// </summary>
    [ProtoContract]
    [ProtoInclude(1, typeof(Item))]
    public interface IItem {
        /// <summary>
        /// Unique Identity for this item
        /// </summary>
        Guid ID { get; }

        /// <summary>
        /// The Quality of the item will determine 
        /// Worth, and Performance
        /// </summary>
        ItemQualityCode Quality { get; }

        /// <summary>
        /// If the item is Stackable then Count can be > 1
        /// Else it is 1
        /// </summary>
        int Count { get; set; }

        /// <summary>
        /// Common info for this item type
        /// </summary>
        IItemInfo Info { get; }

        /// <summary>
        /// A Modifier for this item
        /// </summary>
        IStatModifier Modifier { get; }


        bool Same(IItem item);
    }

    // count is not sufficient enough for security
    // this needs to be a list of guids
    [ProtoContract]
    public class Item : IItem {
        
        [ProtoMember(2)]
        public Guid ID { get; private set; }
        
        [ProtoMember(3)]
        public ItemQualityCode Quality { get; private set; }
        
        [ProtoMember(4)]
        public int Count { get; set; }
        
        [ProtoMember(5)]
        public IItemInfo Info { get; private set; }
        
        [ProtoMember(6)]
        public IStatModifier Modifier { get; private set; }

        public bool Same(IItem item) {
            return item.ID != this.ID && this.Info == this.Info; 
        }

        public Item() {

        }

        public Item(Guid id, IItemInfo info, IStatModifier modifier, ItemQualityCode quality, int count) {
            this.ID = id;
            this.Count = count;
            this.Info = info;
            this.Modifier = modifier;
            this.Quality = quality;
        }

        public override string ToString() {
            return (this.Info.Name ?? this.Info.TypeCode.ToString()) + " Quality:" + this.Quality.ToString();
        }
    }
}
