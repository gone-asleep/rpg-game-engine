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
    [ProtoInclude(102, typeof(ItemToolInfo))]
    public interface IItemToolInfo : IItemInfo, IWieldableItemInfo, ISkillfullItemInfo, IMarketableItemInfo {
    }

    [ProtoContract]
    public class ItemToolInfo : ItemInfo, IItemToolInfo {
        [ProtoMember(5)]
        public ItemWieldType WieldType { get; protected set; }

        [ProtoMember(6)]
        public SkillType ApplyableSkill { get; protected set; }

        [ProtoMember(7)]
        public float MarketValue { get; protected set; }

        public ItemToolInfo() {

        }

        public ItemToolInfo(ItemType type, ItemWieldType wieldType, SkillType applyableSkill, float marketValue, string name = null, string description = null) :
            base(ItemClassCode.Weapon, type, name, description) {
            this.WieldType = wieldType;
            this.ApplyableSkill = applyableSkill;
            this.MarketValue = marketValue;
        }
    }
}
