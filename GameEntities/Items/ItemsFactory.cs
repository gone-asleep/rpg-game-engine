using GameEngine.Entities;
using GameEngine.Entities.Skills;
using GameEngine.Entities.Stats;
using GameEngine.Factories;
using GameEngine.Global;
using GameEngine.Items;
using GameEngine.Items.Info;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weighted_Randomizer;

namespace GameEntities.Items {
    [ProtoContract]
    public class ItemsFactory : IFactoryProducer<IItem, ItemProfile> {
        [ProtoMember(1)]
        private Dictionary<ItemType, IItemInfo> infos = new Dictionary<ItemType, IItemInfo> {
            { ItemType.LongSword, new ItemWeaponInfo(ItemType.LongSword, ItemWieldType.BothHands, SkillType.HeavyBlade, 3, "Long Sword") },
            { ItemType.QuarterStaff, new ItemWeaponInfo(ItemType.QuarterStaff, ItemWieldType.OneHand, SkillType.StaffWeapon, 4, "Quarter Staff") },
            { ItemType.LongStaff, new ItemWeaponInfo(ItemType.LongStaff, ItemWieldType.OneHand, SkillType.StaffWeapon, 4, "Long Staff") },
           
          
            { ItemType.Javelin, new ItemWeaponInfo(ItemType.Javelin, ItemWieldType.OneHand, SkillType.RangeWeapon, 6, "Javelin") },
            { ItemType.GreatClub, new ItemWeaponInfo(ItemType.GreatClub, ItemWieldType.OneHand, SkillType.BluntWeapon, 10, "Great Club") },
            { ItemType.Dagger, new ItemWeaponInfo(ItemType.Dagger, ItemWieldType.OneHand, SkillType.LightBlade,4, "Dagger")},
            { ItemType.Crossbow, new ItemWeaponInfo(ItemType.Crossbow, ItemWieldType.OneHand, SkillType.RangeWeapon,3, "Crossbow")},
            { ItemType.Morningstar, new ItemWeaponInfo(ItemType.Morningstar, ItemWieldType.OneHand, SkillType.BluntWeapon,8, "Morning Star")},
            { ItemType.Scythe, new ItemWeaponInfo(ItemType.Scythe, ItemWieldType.OneHand, SkillType.HeavyBlade,10, "Scythe")},
            { ItemType.Club, new ItemWeaponInfo(ItemType.Club, ItemWieldType.OneHand, SkillType.BluntWeapon,6, "Club")},
            { ItemType.Sling, new ItemWeaponInfo(ItemType.Sling, ItemWieldType.OneHand, SkillType.RangeWeapon,3, "Sling")},
            { ItemType.Mace, new ItemWeaponInfo(ItemType.Mace, ItemWieldType.OneHand, SkillType.BluntWeapon,8, "Mace")},
            { ItemType.Sicle, new ItemWeaponInfo(ItemType.Sicle, ItemWieldType.OneHand, SkillType.LightBlade,6, "Sicle")},
            { ItemType.Spear, new ItemWeaponInfo(ItemType.Spear, ItemWieldType.OneHand, SkillType.PolearmWeapon,8, "Spear")},
            { ItemType.Halberd, new ItemWeaponInfo(ItemType.Halberd, ItemWieldType.OneHand, SkillType.PolearmWeapon,3, "Halberd")},
            { ItemType.LongBow, new ItemWeaponInfo(ItemType.LongBow, ItemWieldType.OneHand, SkillType.RangeWeapon,3, "Long Bow")},
            { ItemType.HandAxe, new ItemWeaponInfo(ItemType.HandAxe, ItemWieldType.OneHand, SkillType.AxeWeapon,3, "Hand Axe")},
            { ItemType.ShortBow, new ItemWeaponInfo(ItemType.ShortBow, ItemWieldType.OneHand, SkillType.RangeWeapon,3, "Short Bow")},
            { ItemType.Maul, new ItemWeaponInfo(ItemType.Maul, ItemWieldType.OneHand, SkillType.BluntWeapon,3, "Maul")},
            { ItemType.GreatAxe, new ItemWeaponInfo(ItemType.GreatAxe, ItemWieldType.OneHand, SkillType.AxeWeapon,3, "Great Axe")},
            { ItemType.WarPick, new ItemWeaponInfo(ItemType.WarPick, ItemWieldType.OneHand, SkillType.AxeWeapon,3, "War Pick")},
            { ItemType.BastardSword, new ItemWeaponInfo(ItemType.BastardSword, ItemWieldType.OneHand, SkillType.HeavyBlade,3, "Bastard Sword")},
            { ItemType.Warhammer, new ItemWeaponInfo(ItemType.Warhammer, ItemWieldType.OneHand, SkillType.HammerWeapon,3, "Warhammer")},
            { ItemType.Flail, new ItemWeaponInfo(ItemType.Flail, ItemWieldType.OneHand, SkillType.FlailWeapon,3, "Flail")},
            { ItemType.BattleAxe, new ItemWeaponInfo(ItemType.BattleAxe, ItemWieldType.OneHand, SkillType.AxeWeapon,3, "Battle Axe")},
            { ItemType.ThrowingHammer, new ItemWeaponInfo(ItemType.ThrowingHammer, ItemWieldType.OneHand, SkillType.RangeWeapon,3, "Throwing Hammer")},
            { ItemType.Scimitar, new ItemWeaponInfo(ItemType.Scimitar, ItemWieldType.OneHand, SkillType.BluntWeapon,3, "Scimitar")},
            { ItemType.Glaive, new ItemWeaponInfo(ItemType.Glaive, ItemWieldType.OneHand, SkillType.PolearmWeapon,3, "Glaive")},
            { ItemType.ShortSword, new ItemWeaponInfo(ItemType.ShortSword, ItemWieldType.OneHand, SkillType.LightBlade,3, "Short Sword")},
            { ItemType.SpellBook, new ItemWeaponInfo(ItemType.SpellBook, ItemWieldType.OneHand, SkillType.UniversalMagic, 3, "Spell Book")},
            { ItemType.StrawHat, new ItemArmorInfo(ItemType.StrawHat, ItemEquipType.Head, 1, "Straw Hat")},
            { ItemType.ClothCap, new ItemArmorInfo(ItemType.ClothCap, ItemEquipType.Head, 1, "Cloth Cap")},
            { ItemType.ClothVest, new ItemArmorInfo(ItemType.ClothVest, ItemEquipType.Chest, 1, "Cloth Vest")},
            { ItemType.ClothShoes, new ItemArmorInfo(ItemType.ClothShoes, ItemEquipType.Feet, 1, "Cloth Shoes")},
            { ItemType.ClothShirt, new ItemArmorInfo(ItemType.ClothShirt, ItemEquipType.Chest, 1, "Cloth Shirt")},
            { ItemType.ClothLeggings, new ItemArmorInfo(ItemType.ClothLeggings, ItemEquipType.Legs, 1, "Cloth Leggings")},
            { ItemType.ClothGloves, new ItemArmorInfo(ItemType.ClothGloves, ItemEquipType.Hands, 1, "Cloth Gloves")},
            { ItemType.FishingPole, new ItemToolInfo(ItemType.FishingPole, ItemWieldType.OneHand, SkillType.Fishing, "Fishing Pole") },
            { ItemType.FishingNet, new ItemToolInfo(ItemType.FishingNet, ItemWieldType.BothHands, SkillType.Fishing, "Fishing Net") },
            { ItemType.CrabTrap, new ItemToolInfo(ItemType.CrabTrap, ItemWieldType.OneHand, SkillType.Fishing, "Crab Trap") },
            { ItemType.Insect, new ItemConsumableInfo(ItemClassCode.Bait | ItemClassCode.Food, ItemType.Insect, "Insect/Bait") },
            { ItemType.Minnow, new ItemConsumableInfo(ItemClassCode.Bait | ItemClassCode.Food, ItemType.Minnow, "Minnow/Bait") },
            { ItemType.Worm, new ItemConsumableInfo(ItemClassCode.Bait | ItemClassCode.Food, ItemType.Worm, "Worm/Bait") },
            { ItemType.Clam, new ItemConsumableInfo(ItemClassCode.Bait | ItemClassCode.Food, ItemType.Clam, "Clam/Bait") },
            { ItemType.DoughBall, new ItemConsumableInfo(ItemClassCode.Bait | ItemClassCode.Food, ItemType.DoughBall, "Dough Ball/Bait") },
            { ItemType.FishingHook, new ItemConsumableInfo(ItemClassCode.Bait | ItemClassCode.Food, ItemType.FishingHook, "Fish Hook") },
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

        public class ItemCreationParams : IComparable<ItemCreationParams> {
            public ItemType Type { get; set; }

            public ItemCreationParams(ItemType type) {
                this.Type = type;
            }

            public int CompareTo(ItemCreationParams other) {
                if (other.Type > this.Type) {
                    return 1;
                } else if (other.Type < this.Type) {
                    return -1;
                } else {
                    return 0;
                }
            }
        }

        IDictionary<NewItemCode, IDictionary<EntityOccupation, IWeightedRandomizer<IItemInfo>>> ProbabilityByItemAndOccupation;

        
        private void buildProbabilities() {
            ProbabilityByItemAndOccupation = new Dictionary<NewItemCode, IDictionary<EntityOccupation, IWeightedRandomizer<IItemInfo>>>();

            ProbabilityByItemAndOccupation[NewItemCode.Weapon] = new Dictionary<EntityOccupation, IWeightedRandomizer<IItemInfo>>();
            ProbabilityByItemAndOccupation[NewItemCode.Weapon][EntityOccupation.Warrior] = new StaticRandomizer<IItemInfo>() {
                { infos[ItemType.LongSword] },
                { infos[ItemType.Morningstar] },
                { infos[ItemType.Mace] },
                { infos[ItemType.GreatAxe] },
                { infos[ItemType.BastardSword] },
                { infos[ItemType.WarPick] },
                { infos[ItemType.ShortSword] }
            };
            ProbabilityByItemAndOccupation[NewItemCode.Weapon][EntityOccupation.Theif] = new StaticRandomizer<IItemInfo>() {
                { infos[ItemType.Dagger], 1},
                { infos[ItemType.ShortSword], 2},
                { infos[ItemType.Crossbow], 2}
            };
            ProbabilityByItemAndOccupation[NewItemCode.Weapon][EntityOccupation.Sorcerer] = new StaticRandomizer<IItemInfo>() {
                { infos[ItemType.QuarterStaff], 1},
                { infos[ItemType.LongStaff], 2},
                { infos[ItemType.SpellBook], 2}
            };
            ProbabilityByItemAndOccupation[NewItemCode.Weapon][EntityOccupation.Drunk] = new StaticRandomizer<IItemInfo>() {
                { infos[ItemType.Dagger], 1}
            };
           
            ProbabilityByItemAndOccupation[NewItemCode.Weapon][EntityOccupation.Fisherman] = new StaticRandomizer<IItemInfo>() {
                { infos[ItemType.Dagger], 1}
            };


            // head hats and helmets
            ProbabilityByItemAndOccupation[NewItemCode.ArmorHead] = new Dictionary<EntityOccupation, IWeightedRandomizer<IItemInfo>>();
            ProbabilityByItemAndOccupation[NewItemCode.ArmorHead][EntityOccupation.Fisherman] = new StaticRandomizer<IItemInfo>() {
                { infos[ItemType.StrawHat], 2},
                { infos[ItemType.ClothCap], 1}
            };

            // head hats and helmets
            ProbabilityByItemAndOccupation[NewItemCode.ArmorChest] = new Dictionary<EntityOccupation, IWeightedRandomizer<IItemInfo>>();
            ProbabilityByItemAndOccupation[NewItemCode.ArmorChest][EntityOccupation.Fisherman] = new StaticRandomizer<IItemInfo>() {
                { infos[ItemType.ClothShirt], 2},
                { infos[ItemType.ClothVest], 1}
            };
            // head hats and helmets
            ProbabilityByItemAndOccupation[NewItemCode.ArmorLegs] = new Dictionary<EntityOccupation, IWeightedRandomizer<IItemInfo>>();
            ProbabilityByItemAndOccupation[NewItemCode.ArmorLegs][EntityOccupation.Fisherman] = new StaticRandomizer<IItemInfo>() {
                { infos[ItemType.ClothLeggings], 1}
            };
            // head hats and helmets
            ProbabilityByItemAndOccupation[NewItemCode.ArmorFeet] = new Dictionary<EntityOccupation, IWeightedRandomizer<IItemInfo>>();
            ProbabilityByItemAndOccupation[NewItemCode.ArmorFeet][EntityOccupation.Fisherman] = new StaticRandomizer<IItemInfo>() {
                { infos[ItemType.ClothShoes], 1}
            };

            // tools of profession
            ProbabilityByItemAndOccupation[NewItemCode.Tool] = new Dictionary<EntityOccupation, IWeightedRandomizer<IItemInfo>>();
            ProbabilityByItemAndOccupation[NewItemCode.Tool][EntityOccupation.Fisherman] = new StaticRandomizer<IItemInfo>() {
                { infos[ItemType.FishingPole], 10},
                { infos[ItemType.FishingNet], 3},
                { infos[ItemType.CrabTrap], 2}
            };

            // tool consumables for profession
            ProbabilityByItemAndOccupation[NewItemCode.ToolMaterial] = new Dictionary<EntityOccupation, IWeightedRandomizer<IItemInfo>>();
            ProbabilityByItemAndOccupation[NewItemCode.ToolMaterial][EntityOccupation.Fisherman] = new StaticRandomizer<IItemInfo>() {
                { infos[ItemType.Worm], 10},
                { infos[ItemType.Insect], 7},
                { infos[ItemType.Clam], 5},
                { infos[ItemType.Minnow], 3},
                { infos[ItemType.FishingHook], 3},
                { infos[ItemType.DoughBall], 2}
            };


        }

        private int RandomWeightedIndex(codeProbability[] array) {
            float value = GameGlobal.RandomFloat(0.0f, 1.0f);
            int index = 0;
            while (array.Length > index + 1 && value > array[index + 1].probability) {
                value -= array[index++].probability;
            }
            return array[index].type;
        }

        public ItemsFactory() {
            this.buildProbabilities();
        }

        public IItem Create(ItemProfile profile) {
            Item item = null;
            ItemQualityCode itemQualityIndex;
            IItemInfo itemInfo;

            if (profile.ProfileType == ItemProfileType.Inventory) {
                // two modes we can either provide specific detail of what to construct or add something in
                if (ProbabilityByItemAndOccupation.ContainsKey(profile.itemCode)) {
                    var probabilities = ProbabilityByItemAndOccupation[profile.itemCode];
                    if (probabilities.ContainsKey(profile.EntityOccupation)) {
                        itemInfo = probabilities[profile.EntityOccupation].NextWithReplacement();
                        itemQualityIndex = (ItemQualityCode)this.RandomWeightedIndex(qualityProbabilities[(int)profile.Level]);
                        item = new Item(GameGlobal.IDs.Next(), itemInfo, null, itemQualityIndex, 1);
                    }

                }
            } else if (profile.ProfileType == ItemProfileType.Specific) {
                itemInfo = infos[profile.Type];
                item = new Item(GameGlobal.IDs.Next(), itemInfo, null, profile.qualityCode, 1);
            }
            return item;
        }
    }
}
