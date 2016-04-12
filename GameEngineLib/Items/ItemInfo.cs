using GameEngine.Entities.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Items {
    public interface IItemInfo {
        /// <summary>
        /// 
        /// </summary>
        ItemClassCode ClassCode { get; }

        ItemType TypeCode { get; }

        SkillType ApplyableSkill { get; }

        ItemEquipType EquipType { get; }

        bool Stackable { get; }

        float BaseLineDamage { get; }
        /// <summary>
        /// the individuals name of this entity
        /// </summary>
        string Name { get; }
    }

    public class ItemInfo : IItemInfo {
        public ItemClassCode ClassCode { get; private set; }

        public ItemType TypeCode { get; private set; }

        public ItemEquipType EquipType {get; private set;}

        public SkillType ApplyableSkill { get; private set; }

        public float BaseLineDamage { get; private set; }

        public string Name { get; private set; }

        public bool Stackable { get; private set; }

        public ItemInfo(ItemClassCode classCode, ItemType type, ItemEquipType equipType, SkillType applyableSkill, bool stackable, float baselineDamage, string name = null) {
            if (Name == null) {
                this.Name = TypeCode.ToString() + " " + TypeCode.ToString();
            }
            this.Name = name;
            this.TypeCode = type;
            this.ClassCode = classCode;
            this.EquipType = equipType;
            this.BaseLineDamage = baselineDamage;
            this.Stackable = stackable;
            this.ApplyableSkill = applyableSkill;
        }
    }
}
