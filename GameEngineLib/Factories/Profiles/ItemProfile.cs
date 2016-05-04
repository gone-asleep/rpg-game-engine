using GameData;
using GameEngine.Entities;
using GameEngine.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Factories {
    public enum NewItemCode {
        Weapon,
        Tool,
        ToolMaterial,
        Potion,
        Food,
        Armor,
        ArmorHead,
        ArmorChest,
        ArmorLegs,
        ArmorFeet,
        ArmorShoulders,
        ArmorArms,
        ArmorHands,
        Shield
    }

    public enum ItemProfileType {
        Inventory,
        Specific
    }

    public struct ItemProfile : IComparable<ItemProfile> {
        public int Level;
        public ItemType Type;
        public ItemQualityCode qualityCode;
        public NewItemCode itemCode;
        public EntityOccupation EntityOccupation;
        public ItemProfileType ProfileType;
        public ItemProfile(int level, ItemType type, ItemQualityCode quality) {
            Level = level;
            Type = type;
            qualityCode = quality;
            itemCode = (NewItemCode)0;
            EntityOccupation = EntityOccupation.None;
            ProfileType = ItemProfileType.Specific;
        }
        public ItemProfile(int level, EntityOccupation entityOccupation) {
            Level = level;
            EntityOccupation = entityOccupation;
            qualityCode = (ItemQualityCode)0;
            itemCode = (NewItemCode)0;
            Type = (ItemType)0;
            ProfileType = ItemProfileType.Inventory;
        }

        public ItemProfile(int level, EntityOccupation entityOccupation, NewItemCode code) {
            Level = level;
            EntityOccupation = entityOccupation;
            qualityCode = (ItemQualityCode)0;
            itemCode = code;
            Type = (ItemType)0;
            ProfileType = ItemProfileType.Inventory;
        }

        public int CompareTo(ItemProfile other) {
            int selfHash = this.GetHashCode();
            int otherHash = other.GetHashCode();
            if (otherHash > selfHash) {
                return -1;
            } else if (otherHash < selfHash) {
                return 1;
            } else {
                return 0;
            }
        }
    }
}
