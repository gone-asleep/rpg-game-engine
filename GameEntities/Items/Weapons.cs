using GameEngine.Entities.Stats;
using GameEngine.Factories;
using GameEngine.Global;
using GameEngine.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEntities.Items {
    public static class Weapons {
        private static bool isLoaded = false;

        public static readonly Func<ItemProfile, Item> LongSwordConstructor = (profile) => {
            Item item = new Item("Potion of Healing", ItemType.HealingPotion, ItemClassCode.Potion, ItemEquipType.LeftHand);
            item.Modifier.Define(StatType.Strength, StatModifierType.Add, 4.0F);
            return item;
        };

        public static void Load() {
            if (!isLoaded) {
                GlobalLookup.Factories.Items.Add(ItemType.LongSword, Weapons.LongSwordConstructor);
                isLoaded = true;
            }
        }
    }
}
