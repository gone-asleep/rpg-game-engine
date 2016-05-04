using GameData;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData.Info {
    [ProtoContract]
    [ProtoInclude(99, typeof(ItemInfo))]
    [ProtoInclude(103, typeof(ItemArmorInfo))]
    public interface IItemArmorInfo : IItemInfo, IEquipableItemInfo, ISkillfullItemInfo, IMarketableItemInfo {
        float DefenseValue { get; }
    }

    [ProtoContract]
    public class ItemArmorInfo : ItemInfo, IItemArmorInfo {
        [ProtoMember(5)]
        public InventorySlot EquipType { get; protected set; }

        [ProtoMember(6)]
        public SkillType ApplyableSkill { get; protected set; }

        [ProtoMember(7)]
        public float DefenseValue { get; protected set; }

        [ProtoMember(8)]
        public float MarketValue { get; protected set; }

        public ItemArmorInfo() {

        }

        public ItemArmorInfo(ItemType type, InventorySlot equipType, float defenseValue, float marketValue, string name = null, string description = null) :
            base(ItemClassCode.Armor, type, name, description) {
            this.EquipType = equipType;
            this.DefenseValue = defenseValue;
            this.MarketValue = marketValue;
        }
    }
}
