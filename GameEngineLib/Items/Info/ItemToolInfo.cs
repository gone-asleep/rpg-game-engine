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
    public interface IItemToolInfo : IItemInfo, IEquipableItemInfo, ISkillfullItemInfo {
    }

    [ProtoContract]
    public class ItemToolInfo : ItemInfo, IItemToolInfo {
        [ProtoMember(5)]
        public ItemEquipType EquipType { get; protected set; }

        [ProtoMember(6)]
        public SkillType ApplyableSkill { get; protected set; }

        public ItemToolInfo() {

        }

        public ItemToolInfo(ItemType type, ItemEquipType equipType, SkillType applyableSkill, string name = null, string description = null) :
            base(ItemClassCode.Weapon, type, name, description) {
            this.EquipType = equipType;
            this.ApplyableSkill = applyableSkill;
        }
    }
}
