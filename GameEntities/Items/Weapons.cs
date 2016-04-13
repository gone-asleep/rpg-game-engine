using GameEngine.Entities.Skills;
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
            IItemInfo info = new ItemInfo(ItemClassCode.Weapon, ItemType.LongSword, ItemEquipType.LeftHand, SkillType.TwoHanded, false, true, 3);
            Item item = new Item(GameGlobal.IDs.Next(), info, null);
            return item;
        };

        public static void Load() {
            if (!isLoaded) {
                GameGlobal.Factories.Items.Add(ItemType.LongSword, Weapons.LongSwordConstructor);
                isLoaded = true;
            }
        }
    }
}
