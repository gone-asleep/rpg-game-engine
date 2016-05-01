using GameEngine.Entities.Skills;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Items.Info {

    public interface ISkillfullItemInfo {
        SkillType ApplyableSkill { get; }
    }

    public interface IEquipableItemInfo {
        ItemEquipType EquipType { get; }
    }

    [ProtoContract]
    [ProtoInclude(99, typeof(ItemInfo))]
    public interface IItemInfo {
        ItemClassCode ClassCode { get; }
        ItemType TypeCode { get; }

        string Name { get; }

        string Description { get; }
    }

    [ProtoContract]
    [ProtoInclude(100, typeof(ItemConsumableInfo))]
    [ProtoInclude(101, typeof(ItemWeaponInfo))]
    [ProtoInclude(102, typeof(ItemToolInfo))]
    [ProtoInclude(102, typeof(ItemArmorInfo))]
    public class ItemInfo : IItemInfo {
        [ProtoMember(1)]
        public ItemClassCode ClassCode { get; protected set; }
        
        [ProtoMember(2)]
        public ItemType TypeCode { get; protected set; }
        
        [ProtoMember(3)]
        public string Name { get; protected set; }

        [ProtoMember(4)]
        public string Description { get; protected set; }

        public ItemInfo() {

        }

        public ItemInfo(ItemClassCode classCode, ItemType type,  string name = null, string description = null) {
            if (Name == null) {
                this.Name = TypeCode.ToString() + " " + TypeCode.ToString();
            }
            this.Name = name;
            this.Description = description;
            this.TypeCode = type;
            this.ClassCode = classCode;
        }

        public override bool Equals(object obj) {
            ItemInfo other =  obj as ItemInfo;
            if (other != null) {
                return other.ClassCode == this.ClassCode && other.TypeCode == this.TypeCode && other.Name == this.Name;
            }
            return false;
        }
    }
}