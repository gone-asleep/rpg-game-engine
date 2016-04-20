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
    public class WeaponsFactory : IFactoryProducer<IItem, ItemProfile> {
        private IItemInfo[] info = new IItemInfo[] {
            new WeaponInfo(ItemType.LongSword, ItemEquipType.LeftHand, SkillType.HeavyBlade, 3, "Long Sword"),
            new WeaponInfo(ItemType.QuarterStaff, ItemEquipType.LeftHand, SkillType.StaffWeapon, 4, "Quarter Staff"),
            new WeaponInfo(ItemType.Javelin, ItemEquipType.LeftHand, SkillType.RangeWeapon, 6, "Javelin"),
            new WeaponInfo(ItemType.GreatClub, ItemEquipType.LeftHand, SkillType.BluntWeapon, 10, "Great Club"),
            new WeaponInfo(ItemType.Dagger, ItemEquipType.LeftHand, SkillType.LightBlade,4, "Dagger"),
            new WeaponInfo(ItemType.Crossbow, ItemEquipType.LeftHand, SkillType.RangeWeapon,3, "Crossbow"),
            new WeaponInfo(ItemType.Morningstar, ItemEquipType.LeftHand, SkillType.BluntWeapon,8, "Morning Star"),
            new WeaponInfo(ItemType.Scythe, ItemEquipType.LeftHand, SkillType.HeavyBlade,10, "Scythe"),
            new WeaponInfo(ItemType.Club, ItemEquipType.LeftHand, SkillType.BluntWeapon,6, "Club"),
            new WeaponInfo(ItemType.Sling, ItemEquipType.LeftHand, SkillType.RangeWeapon,3, "Sling"),
            new WeaponInfo(ItemType.Mace, ItemEquipType.LeftHand, SkillType.BluntWeapon,8, "Mace"),
            new WeaponInfo(ItemType.Sicle, ItemEquipType.LeftHand, SkillType.LightBlade,6, "Sicle"),
            new WeaponInfo(ItemType.Spear, ItemEquipType.LeftHand, SkillType.PolearmWeapon,8, "Spear"),
            new WeaponInfo(ItemType.Halberd, ItemEquipType.LeftHand, SkillType.PolearmWeapon,3, "Halberd"),
            new WeaponInfo(ItemType.LongBow, ItemEquipType.LeftHand, SkillType.RangeWeapon,3, "Long Bow"),
            new WeaponInfo(ItemType.HandAxe, ItemEquipType.LeftHand, SkillType.AxeWeapon,3, "Hand Axe"),
            new WeaponInfo(ItemType.ShortBow, ItemEquipType.LeftHand, SkillType.RangeWeapon,3, "Short Bow"),
            new WeaponInfo(ItemType.Maul, ItemEquipType.LeftHand, SkillType.BluntWeapon,3, "Maul"),
            new WeaponInfo(ItemType.GreatAxe, ItemEquipType.LeftHand, SkillType.AxeWeapon,3, "Great Axe"),
            new WeaponInfo(ItemType.WarPick, ItemEquipType.LeftHand, SkillType.AxeWeapon,3, "War Pick"),
            new WeaponInfo(ItemType.BastardSword, ItemEquipType.LeftHand, SkillType.HeavyBlade,3, "Bastard Sword"),
            new WeaponInfo(ItemType.Warhammer, ItemEquipType.LeftHand, SkillType.HammerWeapon,3, "Warhammer"),
            new WeaponInfo(ItemType.Flail, ItemEquipType.LeftHand, SkillType.FlailWeapon,3, "Flail"),
            new WeaponInfo(ItemType.BattleAxe, ItemEquipType.LeftHand, SkillType.AxeWeapon,3, "Battle Axe"),
            new WeaponInfo(ItemType.ThrowingHammer, ItemEquipType.LeftHand, SkillType.RangeWeapon,3, "Throwing Hammer"),
            new WeaponInfo(ItemType.Scimitar, ItemEquipType.LeftHand, SkillType.BluntWeapon,3, "Scimitar"),
            new WeaponInfo(ItemType.Glaive, ItemEquipType.LeftHand, SkillType.PolearmWeapon,3, "Glaive"),
            new WeaponInfo(ItemType.ShortSword, ItemEquipType.LeftHand, SkillType.LightBlade,3, "Short Sword"),
        };


        public IItem Create(ItemProfile profile) {
            Guid id = GameGlobal.IDs.Next();
            IItemInfo info = this.info[(int)profile.Type];
            Item item = new Item(id, info, null, 1);
            return item;
        }
    }
}
