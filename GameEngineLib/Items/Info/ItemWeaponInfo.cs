using GameEngine.Entities.Skills;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Items;

namespace GameEngine.Items.Info {
    [ProtoContract]
    [ProtoInclude(99, typeof(ItemInfo))]
    [ProtoInclude(101, typeof(ItemWeaponInfo))]
    public interface IItemWeaponInfo : IItemInfo, IWieldableItemInfo, ISkillfullItemInfo {
        float BaseDamage { get; }
    }

    [ProtoContract]
    public class ItemWeaponInfo : ItemInfo, IItemWeaponInfo {

        [ProtoMember(5)]
        public float BaseDamage { get; private set; }

        [ProtoMember(6)]
        public ItemWieldType WieldType { get; protected set; }

        [ProtoMember(7)]
        public SkillType ApplyableSkill { get; protected set; }

        public ItemWeaponInfo() {

        }

        public ItemWeaponInfo(ItemType type, ItemWieldType wieldType, SkillType applyableSkill, float baselineDamage, string name = null, string description = null) :
            base(ItemClassCode.Weapon, type, name, description) {
            this.BaseDamage = baselineDamage;
            this.WieldType = wieldType;
            this.ApplyableSkill = applyableSkill;
        }
    }
}
