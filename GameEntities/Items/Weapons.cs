using GameEngine.Entities.Skills;
using GameEngine.Entities.Stats;
using GameEngine.Factories;
using GameEngine.Global;
using GameEngine.Items;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEntities.Items {
    [ProtoContract]
    public class WeaponsFactory : IFactoryProducer<IItem, ItemProfile> {
        [ProtoMember(1)]
        private Dictionary<ItemType, IItemInfo> infos = new Dictionary<ItemType, IItemInfo> {
            { ItemType.LongSword, new WeaponInfo(ItemType.LongSword, ItemEquipType.LeftHand, SkillType.HeavyBlade, 3, "Long Sword") },
            { ItemType.QuarterStaff, new WeaponInfo(ItemType.QuarterStaff, ItemEquipType.LeftHand, SkillType.StaffWeapon, 4, "Quarter Staff") },
            { ItemType.Javelin, new WeaponInfo(ItemType.Javelin, ItemEquipType.LeftHand, SkillType.RangeWeapon, 6, "Javelin") },
            { ItemType.GreatClub, new WeaponInfo(ItemType.GreatClub, ItemEquipType.LeftHand, SkillType.BluntWeapon, 10, "Great Club") },
            { ItemType.Dagger, new WeaponInfo(ItemType.Dagger, ItemEquipType.LeftHand, SkillType.LightBlade,4, "Dagger")},
            { ItemType.Crossbow, new WeaponInfo(ItemType.Crossbow, ItemEquipType.LeftHand, SkillType.RangeWeapon,3, "Crossbow")},
            { ItemType.Morningstar, new WeaponInfo(ItemType.Morningstar, ItemEquipType.LeftHand, SkillType.BluntWeapon,8, "Morning Star")},
            { ItemType.Scythe, new WeaponInfo(ItemType.Scythe, ItemEquipType.LeftHand, SkillType.HeavyBlade,10, "Scythe")},
            { ItemType.Club, new WeaponInfo(ItemType.Club, ItemEquipType.LeftHand, SkillType.BluntWeapon,6, "Club")},
            { ItemType.Sling, new WeaponInfo(ItemType.Sling, ItemEquipType.LeftHand, SkillType.RangeWeapon,3, "Sling")},
            { ItemType.Mace, new WeaponInfo(ItemType.Mace, ItemEquipType.LeftHand, SkillType.BluntWeapon,8, "Mace")},
            { ItemType.Sicle, new WeaponInfo(ItemType.Sicle, ItemEquipType.LeftHand, SkillType.LightBlade,6, "Sicle")},
            { ItemType.Spear, new WeaponInfo(ItemType.Spear, ItemEquipType.LeftHand, SkillType.PolearmWeapon,8, "Spear")},
            { ItemType.Halberd, new WeaponInfo(ItemType.Halberd, ItemEquipType.LeftHand, SkillType.PolearmWeapon,3, "Halberd")},
            { ItemType.LongBow, new WeaponInfo(ItemType.LongBow, ItemEquipType.LeftHand, SkillType.RangeWeapon,3, "Long Bow")},
            { ItemType.HandAxe, new WeaponInfo(ItemType.HandAxe, ItemEquipType.LeftHand, SkillType.AxeWeapon,3, "Hand Axe")},
            { ItemType.ShortBow, new WeaponInfo(ItemType.ShortBow, ItemEquipType.LeftHand, SkillType.RangeWeapon,3, "Short Bow")},
            { ItemType.Maul, new WeaponInfo(ItemType.Maul, ItemEquipType.LeftHand, SkillType.BluntWeapon,3, "Maul")},
            { ItemType.GreatAxe, new WeaponInfo(ItemType.GreatAxe, ItemEquipType.LeftHand, SkillType.AxeWeapon,3, "Great Axe")},
            { ItemType.WarPick, new WeaponInfo(ItemType.WarPick, ItemEquipType.LeftHand, SkillType.AxeWeapon,3, "War Pick")},
            { ItemType.BastardSword, new WeaponInfo(ItemType.BastardSword, ItemEquipType.LeftHand, SkillType.HeavyBlade,3, "Bastard Sword")},
            { ItemType.Warhammer, new WeaponInfo(ItemType.Warhammer, ItemEquipType.LeftHand, SkillType.HammerWeapon,3, "Warhammer")},
            { ItemType.Flail, new WeaponInfo(ItemType.Flail, ItemEquipType.LeftHand, SkillType.FlailWeapon,3, "Flail")},
            { ItemType.BattleAxe, new WeaponInfo(ItemType.BattleAxe, ItemEquipType.LeftHand, SkillType.AxeWeapon,3, "Battle Axe")},
            { ItemType.ThrowingHammer, new WeaponInfo(ItemType.ThrowingHammer, ItemEquipType.LeftHand, SkillType.RangeWeapon,3, "Throwing Hammer")},
            { ItemType.Scimitar, new WeaponInfo(ItemType.Scimitar, ItemEquipType.LeftHand, SkillType.BluntWeapon,3, "Scimitar")},
            { ItemType.Glaive, new WeaponInfo(ItemType.Glaive, ItemEquipType.LeftHand, SkillType.PolearmWeapon,3, "Glaive")},
            { ItemType.ShortSword, new WeaponInfo(ItemType.ShortSword, ItemEquipType.LeftHand, SkillType.LightBlade,3, "Short Sword")},
            { ItemType.SpellBook, new WeaponInfo(ItemType.SpellBook, ItemEquipType.LeftHand, SkillType.UniversalMagic, 3, "Spell Book")}
        };

        // this could be replaced with a function
        [ProtoMember(2)]
        codeProbability[][] qualityProbabilities = new codeProbability[][] {
            //by level so far
            //level 0
            new codeProbability[]{
                new codeProbability(ItemQualityCode.Broken, 0.3f),
                new codeProbability(ItemQualityCode.Junk, 0.5f),
                new codeProbability(ItemQualityCode.Poor, 0.2f),
            },
            //level 1
            new codeProbability[]{
                new codeProbability(ItemQualityCode.Junk, 0.3f),
                new codeProbability(ItemQualityCode.Poor, 0.4f),
                new codeProbability(ItemQualityCode.Average, 0.3f)
            },
            //level 2
            new codeProbability[]{
                new codeProbability(ItemQualityCode.Junk, 0.2f),
                new codeProbability(ItemQualityCode.Poor, 0.3f),
                new codeProbability(ItemQualityCode.Average, 0.3f),
                new codeProbability(ItemQualityCode.Good, 0.2f),
            },
            //level 3
            new codeProbability[]{
                new codeProbability(ItemQualityCode.Junk, 0.1f),
                new codeProbability(ItemQualityCode.Poor, 0.2f),
                new codeProbability(ItemQualityCode.Average, 0.5f),
                new codeProbability(ItemQualityCode.Good, 0.2f),
            },
            //level 4
            new codeProbability[]{
                new codeProbability(ItemQualityCode.Poor, 0.2f),
                new codeProbability(ItemQualityCode.Average, 0.3f),
                new codeProbability(ItemQualityCode.Good, 0.4f),
                new codeProbability(ItemQualityCode.Great, 0.1f),
            },
            //level 5
            new codeProbability[]{
                new codeProbability(ItemQualityCode.Poor, 0.05f),
                new codeProbability(ItemQualityCode.Average, 0.15f),
                new codeProbability(ItemQualityCode.Good, 0.5f),
                new codeProbability(ItemQualityCode.Great, 0.3f),
            },
            //level 6
            new codeProbability[]{
                new codeProbability(ItemQualityCode.Average, 0.2f),
                new codeProbability(ItemQualityCode.Good, 0.3f),
                new codeProbability(ItemQualityCode.Great, 0.5f),
            },
            //level 6
            new codeProbability[]{
                new codeProbability(ItemQualityCode.Average, 0.1f),
                new codeProbability(ItemQualityCode.Good, 0.2f),
                new codeProbability(ItemQualityCode.Great, 0.6f),
                new codeProbability(ItemQualityCode.Flawless, 0.1f),
            },
            //level 7
            new codeProbability[]{
                new codeProbability(ItemQualityCode.Average, 0.05f),
                new codeProbability(ItemQualityCode.Good, 0.15f),
                new codeProbability(ItemQualityCode.Great, 0.45f),
                new codeProbability(ItemQualityCode.Flawless, 0.35f),
            },
             //level 8
            new codeProbability[]{
                new codeProbability(ItemQualityCode.Average, 0.03f),
                new codeProbability(ItemQualityCode.Good, 0.07f),
                new codeProbability(ItemQualityCode.Great, 0.55f),
                new codeProbability(ItemQualityCode.Flawless, 0.35f),
            },
            //level 9
            new codeProbability[]{
                new codeProbability(ItemQualityCode.Good, 0.07f),
                new codeProbability(ItemQualityCode.Great, 0.55f),
                new codeProbability(ItemQualityCode.Flawless, 0.35f),
                new codeProbability(ItemQualityCode.Superior, 0.05f),
            },
        };

        [ProtoMember(3)]
        codeProbability[][] Probabilities = new codeProbability[][] {
            new codeProbability[]{}, //InventorySpecified 
            /*InventoryWarrior*/     
            new codeProbability[] { 
                new codeProbability(ItemType.LongSword, 0.1f), 
                new codeProbability(ItemType.Morningstar, 0.2f),
                new codeProbability(ItemType.Mace, 0.2f),
                new codeProbability(ItemType.GreatAxe, 0.2f),
                new codeProbability(ItemType.BastardSword, 0.1f),
                new codeProbability(ItemType.WarPick, 0.1f),
                new codeProbability(ItemType.ShortSword, 0.1f),
            }, // do we really want to list out all probabilities based on 
            /*InventoryThief*/
            new codeProbability[] { 
                new codeProbability(ItemType.Dagger, 0.1f), 
                new codeProbability(ItemType.ShortSword, 0.2f),
                new codeProbability(ItemType.Crossbow, 0.2f)
            },
            /*InventoryBard*/
            new codeProbability[]{},
            /*InventoryBarbarian*/ // large heavy weapons , tanks
            new codeProbability[]{},
            /*InventoryPalidin*/ // smaller weapons
            new codeProbability[]{},
            /*InventoryRanger*/
            new codeProbability[]{},
            /*InventorySorcerer*/
             new codeProbability[] { 
                new codeProbability(ItemType.QuarterStaff, 0.5f), 
                new codeProbability(ItemType.LongStaff, 0.5f),
                new codeProbability(ItemType.SpellBook, 0.5f)
            },
            /*InventoryWarlock*/
            new codeProbability[]{},
            /*InventoryDrunk*/
            new codeProbability[] { 
                new codeProbability(ItemType.Dagger, 0.1f)
            }, 
            /*InventoryFisherman*/
            new codeProbability[] { 
                new codeProbability(ItemType.Dagger, 0.1f)
            },
            /*InventoryGuard*/
            new codeProbability[]{},
            /*InventoryMiner*/
            new codeProbability[]{},
            /*InventoryScout*/
            new codeProbability[]{},
        };

        private int RandomWeightedIndex(codeProbability[] array) {
            float value = GameGlobal.RandomFloat(0.0f, 1.0f);
            int index = 0;
            while (array.Length > index + 1 && value > array[index + 1].probability) {
                value -= array[index++].probability;
            }
            return array[index].type;
        }

        public IItem Create(ItemProfile profile) {
            Guid id = GameGlobal.IDs.Next();
        
            ItemType itemTypeIndex;
            ItemQualityCode itemQualityIndex;
            
            // two modes we can either provide specific detail of what to construct or add something in
            if (profile.ItemProfileType != ItemProfileType.Specific) {
                itemTypeIndex = (ItemType)this.RandomWeightedIndex(Probabilities[(int)profile.ItemProfileType]);
                itemQualityIndex = (ItemQualityCode)this.RandomWeightedIndex(qualityProbabilities[(int)profile.Level]);
            } else {
                itemTypeIndex = profile.Type; // set it equal to the provided type
                itemQualityIndex = profile.qualityCode;
            }

            Item item = new Item(id, infos[itemTypeIndex], null, itemQualityIndex, 1);
            return item;
        }
    }
}
