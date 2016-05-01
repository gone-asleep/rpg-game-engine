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
    [ProtoInclude(103, typeof(ItemArmorInfo))]
    public interface IItemArmorInfo : IItemInfo, IEquipableItemInfo, ISkillfullItemInfo {
        float DefenseValue { get; }
    }

    [ProtoContract]
    public class ItemArmorInfo : ItemInfo, IItemArmorInfo {
        [ProtoMember(5)]
        public ItemEquipType EquipType { get; protected set; }

        [ProtoMember(6)]
        public SkillType ApplyableSkill { get; protected set; }

        [ProtoMember(7)]
        public float DefenseValue { get; protected set; }

        public ItemArmorInfo() {

        }

        public ItemArmorInfo(ItemType type, ItemEquipType equipType, float defenseValue, string name = null, string description = null) :
            base(ItemClassCode.Armor, type, name, description) {
            this.EquipType = equipType;
            this.DefenseValue = defenseValue;
        }
    }
}
