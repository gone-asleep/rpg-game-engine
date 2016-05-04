using GameData;
using GameData.Info;
using GameEngine.Factories;
using GameEngine.Global;
using GameEngine.Items;
using ProtoBuf;
using System;
using System.Collections.Generic;
using Weighted_Randomizer;

namespace GameEntities.Items {
    [ProtoContract]
    public class ItemsFactory : IFactoryProducer<IItem, ItemProfile> {
        [ProtoMember(1)]

        // this could be replaced with a function

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
                { Globals.ItemInfo[ItemType.LongSword] },
                { Globals.ItemInfo[ItemType.Morningstar] },
                { Globals.ItemInfo[ItemType.Mace] },
                { Globals.ItemInfo[ItemType.GreatAxe] },
                { Globals.ItemInfo[ItemType.BastardSword] },
                { Globals.ItemInfo[ItemType.WarPick] },
                { Globals.ItemInfo[ItemType.ShortSword] }
            };
            ProbabilityByItemAndOccupation[NewItemCode.Weapon][EntityOccupation.Theif] = new StaticRandomizer<IItemInfo>() {
                { Globals.ItemInfo[ItemType.Dagger], 1},
                { Globals.ItemInfo[ItemType.ShortSword], 2},
                { Globals.ItemInfo[ItemType.Crossbow], 2}
            };
            ProbabilityByItemAndOccupation[NewItemCode.Weapon][EntityOccupation.Sorcerer] = new StaticRandomizer<IItemInfo>() {
                { Globals.ItemInfo[ItemType.QuarterStaff], 1},
                { Globals.ItemInfo[ItemType.LongStaff], 2},
                { Globals.ItemInfo[ItemType.SpellBook], 2}
            };
            ProbabilityByItemAndOccupation[NewItemCode.Weapon][EntityOccupation.Drunk] = new StaticRandomizer<IItemInfo>() {
                { Globals.ItemInfo[ItemType.Dagger], 1}
            };
           
            ProbabilityByItemAndOccupation[NewItemCode.Weapon][EntityOccupation.Fisherman] = new StaticRandomizer<IItemInfo>() {
                { Globals.ItemInfo[ItemType.Dagger], 1}
            };


            // head hats and helmets
            ProbabilityByItemAndOccupation[NewItemCode.ArmorHead] = new Dictionary<EntityOccupation, IWeightedRandomizer<IItemInfo>>();
            ProbabilityByItemAndOccupation[NewItemCode.ArmorHead][EntityOccupation.Fisherman] = new StaticRandomizer<IItemInfo>() {
                { Globals.ItemInfo[ItemType.StrawHat], 2},
                { Globals.ItemInfo[ItemType.ClothCap], 1}
            };

            // head hats and helmets
            ProbabilityByItemAndOccupation[NewItemCode.ArmorChest] = new Dictionary<EntityOccupation, IWeightedRandomizer<IItemInfo>>();
            ProbabilityByItemAndOccupation[NewItemCode.ArmorChest][EntityOccupation.Fisherman] = new StaticRandomizer<IItemInfo>() {
                { Globals.ItemInfo[ItemType.ClothShirt], 2},
                { Globals.ItemInfo[ItemType.ClothVest], 1}
            };
            // head hats and helmets
            ProbabilityByItemAndOccupation[NewItemCode.ArmorLegs] = new Dictionary<EntityOccupation, IWeightedRandomizer<IItemInfo>>();
            ProbabilityByItemAndOccupation[NewItemCode.ArmorLegs][EntityOccupation.Fisherman] = new StaticRandomizer<IItemInfo>() {
                { Globals.ItemInfo[ItemType.ClothLeggings], 1}
            };
            // head hats and helmets
            ProbabilityByItemAndOccupation[NewItemCode.ArmorFeet] = new Dictionary<EntityOccupation, IWeightedRandomizer<IItemInfo>>();
            ProbabilityByItemAndOccupation[NewItemCode.ArmorFeet][EntityOccupation.Fisherman] = new StaticRandomizer<IItemInfo>() {
                { Globals.ItemInfo[ItemType.ClothShoes], 1}
            };

            // tools of profession
            ProbabilityByItemAndOccupation[NewItemCode.Tool] = new Dictionary<EntityOccupation, IWeightedRandomizer<IItemInfo>>();
            ProbabilityByItemAndOccupation[NewItemCode.Tool][EntityOccupation.Fisherman] = new StaticRandomizer<IItemInfo>() {
                { Globals.ItemInfo[ItemType.FishingPole], 10},
                { Globals.ItemInfo[ItemType.FishingNet], 3},
                { Globals.ItemInfo[ItemType.CrabTrap], 2}
            };

            // tool consumables for profession
            ProbabilityByItemAndOccupation[NewItemCode.ToolMaterial] = new Dictionary<EntityOccupation, IWeightedRandomizer<IItemInfo>>();
            ProbabilityByItemAndOccupation[NewItemCode.ToolMaterial][EntityOccupation.Fisherman] = new StaticRandomizer<IItemInfo>() {
                { Globals.ItemInfo[ItemType.Worm], 10},
                { Globals.ItemInfo[ItemType.Insect], 7},
                { Globals.ItemInfo[ItemType.Clam], 5},
                { Globals.ItemInfo[ItemType.Minnow], 3},
                { Globals.ItemInfo[ItemType.FishingHook], 3},
                { Globals.ItemInfo[ItemType.DoughBall], 2}
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
                itemInfo = Globals.ItemInfo[profile.Type];
                item = new Item(GameGlobal.IDs.Next(), itemInfo, null, profile.qualityCode, 1);
            }
            return item;
        }
    }
}
