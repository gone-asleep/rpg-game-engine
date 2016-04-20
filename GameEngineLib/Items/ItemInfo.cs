using GameEngine.Entities.Skills;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Items {
    [ProtoContract]
    [ProtoInclude(9, typeof(ItemInfo))]
    [ProtoInclude(10, typeof(WeaponInfo))]
    public interface IWeaponInfo : IItemInfo, IEquipableItemInfo {
        float BaseDamage { get; }
        SkillType ApplyableSkill { get; }
    }

    [ProtoContract]
    [ProtoInclude(1, typeof(ItemInfo))]
    public interface IItemInfo {
        /// <summary>
        /// 
        /// </summary>
        ItemClassCode ClassCode { get; }

        
        ItemType TypeCode { get; }

        bool Stackable { get; }


        /// <summary>
        /// the individuals name of this entity
        /// </summary>
        string Name { get; }
    }

    public interface IEquipableItemInfo {
        ItemEquipType EquipType { get; }
    }

    [ProtoContract]
    [ProtoInclude(13, typeof(WeaponInfo))]
    public class ItemInfo : IItemInfo {
        [ProtoMember(1)]
        public ItemClassCode ClassCode { get; protected set; }
        
        [ProtoMember(2)]
        public ItemType TypeCode { get; protected set; }
        
        [ProtoMember(3)]
        public bool Stackable { get; protected set; }
        
        [ProtoMember(4)]
        public string Name { get; protected set; }

        public ItemInfo() {

        }

        public ItemInfo(ItemClassCode classCode, ItemType type, bool stackable, string name = null) {
            if (Name == null) {
                this.Name = TypeCode.ToString() + " " + TypeCode.ToString();
            }
            this.Name = name;
            this.TypeCode = type;
            this.ClassCode = classCode;
            this.Stackable = stackable;
        }

        public override bool Equals(object obj) {
            ItemInfo other =  obj as ItemInfo;
            if (other != null) {
                return other.ClassCode == this.ClassCode && other.TypeCode == this.TypeCode && other.Stackable == this.Stackable && other.Name == this.Name;
            }
            return false;
        }
    }

    [ProtoContract]
    public class WeaponInfo : ItemInfo, IWeaponInfo {
        
        [ProtoMember(5)]
        public float BaseDamage { get; private set; }
        
        [ProtoMember(6)]
        public ItemEquipType EquipType { get; protected set; }
        
        [ProtoMember(7)]
        public SkillType ApplyableSkill { get; protected set; }

        public WeaponInfo() {

        }

        public WeaponInfo(ItemType type, ItemEquipType equipType, SkillType applyableSkill, float baselineDamage, string name = null) :
            base(ItemClassCode.Weapon, type, false/*assume all weapons are not stackable*/, name) {
                this.BaseDamage = baselineDamage;
                this.EquipType = EquipType;
                this.ApplyableSkill = applyableSkill;
        }
    }
}
