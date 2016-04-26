using GameEngine.Entities;
using GameEngine.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Factories {

    public enum ItemProfileType {
        Specific,
        InventoryWarrior,
        InventoryThief,
        InventoryBard,
        InventoryBarbarian,
        InventoryMonk,
        InventoryPalidin,
        InventoryRanger,
        InventorySorcerer,
        InventoryWarlock,
        InventoryDrunk,
        InventoryFisherman,
        InventoryGuard,
        InventoryMiner,
        InventoryScout
    }

    public struct ItemProfile {
        public int Level;
        public ItemType Type;
        public ItemQualityCode qualityCode;
        public ItemProfileType ItemProfileType;
        public ItemProfile(int level, ItemType type, ItemQualityCode quality) {
            Level = level;
            Type = type;
            qualityCode = quality;
            ItemProfileType = ItemProfileType.Specific;
        }
        public ItemProfile(int level, ItemProfileType itemProfileType) {
            Level = level;
            ItemProfileType = itemProfileType;
            qualityCode = (ItemQualityCode)0;
            Type = (ItemType)0;
        }
    }
}
