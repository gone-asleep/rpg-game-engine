using GameEngine.Entities.Skills;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Items.Info {
    [ProtoContract]
    [ProtoInclude(99, typeof(ItemInfo))]
    [ProtoInclude(104, typeof(ItemClothingInfo))]
    public interface IItemClothingInfo : IItemInfo, IEquipableItemInfo, IMarketableItemInfo {
    }

    [ProtoContract]
    public class ItemClothingInfo : ItemInfo, IItemClothingInfo {
        [ProtoMember(5)]
        public InventorySlot EquipType { get; protected set; }

        [ProtoMember(6)]
        public float MarketValue { get; protected set; }

        public ItemClothingInfo() {

        }

        public ItemClothingInfo(ItemType type, InventorySlot equipType, float marketValue, string name = null, string description = null) :
            base(ItemClassCode.Armor, type, name, description) {
            this.EquipType = equipType;
            this.MarketValue = marketValue;
        }
    }
}
