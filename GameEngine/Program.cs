using GameEngine.Effects;
using GameEngine.Entities;
using GameEngine.Entities.Stats;
using GameEngine.Global;
using GameEngine.Items;
using GameEngine.Worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine {
    class Program {
        static void Initialize() {
            GlobalLookup.Factories.Items.Add(ItemType.LongSword, (profile) => {
                Item sword = new Item("A Sword", ItemType.LongSword, ItemClassCode.Weapon, ItemEquipType.LeftHand);
                sword.Modifier.Define(StatType.Strength, StatValueOp.Add, 4.0F);
                return sword;
            });

            GlobalLookup.Factories.Items.Add(ItemType.HealingPotion, (profile) => {
                Item item = new Item("Potion of Healing", ItemType.HealingPotion, ItemClassCode.Potion, ItemEquipType.LeftHand);
                item.Modifier.Define(StatType.Strength, StatValueOp.Add, 4.0F);
                return item;
            });

            GlobalLookup.Factories.Entities.Add(EntityTypeCode.Human, (profile) => {
                Entity entity = new Entity("Ken");
                entity.Stats.Set(StatType.Agility, 1.0F);
                entity.Stats.Set(StatType.Charisma, 1.0F);
                entity.Stats.Set(StatType.Inteligence, 1.0F);
                entity.Stats.Set(StatType.Luck, 1.0F);
                entity.Stats.Set(StatType.Stamina, 1.0F);
                entity.Stats.Set(StatType.Strength, 1.0F);
                entity.Stats.Set(StatType.Wisdom, 1.0F);
                entity.Stats.Set(StatType.Speed, 20.0F);
                return entity;
            });

            GlobalLookup.Factories.Maps.Add(MapTypeCode.Test, (profile) => {
                var map = new Map(20, 20, 40);
                map.Tiles[5][5] = 3;
                map.SetTiles(0, 0, 3, 3, 1);
                map.SetTiles(3, 2, 1, 6, 1);
                return map;
            });


            GlobalLookup.Factories.Effects.Add(EffectTypeCode.Strengthen, (profile) => {
                float seconds = 30.0f;
                Effect effect = new Effect(EffectTypeCode.Strengthen, EffectClassCode.EntityStatusEffect ,GlobalLookup.Time.Current ,GlobalLookup.Time.Current + seconds);
                effect.Modifier.Define(StatType.Strength, StatValueOp.Multiply, 1.5f); // +50%
                return effect;
            });

            GlobalLookup.Factories.Effects.Add(EffectTypeCode.Weaken, (profile) => {
                float seconds = 30.0f;
                Effect effect = new Effect(EffectTypeCode.Weaken, EffectClassCode.EntityStatusEffect, GlobalLookup.Time.Current, GlobalLookup.Time.Current + seconds);
                effect.Modifier.Define(StatType.Strength, StatValueOp.Multiply, 0.5f); //-50%
                return effect;
            });

        }

        static Item sword;
        static void CreateTestData() {
            var h1 = GlobalLookup.Factories.Items.Generate(typeCode: ItemType.HealingPotion);
            var h2 = GlobalLookup.Factories.Items.Generate(typeCode: ItemType.HealingPotion);
            sword = GlobalLookup.Factories.Items.Generate(typeCode: ItemType.LongSword);
            GlobalLookup.AddPlayer(GlobalLookup.Factories.Entities.Generate(typeCode: EntityTypeCode.Human));
            GlobalLookup.World.SetMap(GlobalLookup.Factories.Maps.Generate(typeCode: MapTypeCode.Test));

            GlobalLookup.Player.Receive(h1, true);
            GlobalLookup.Player.Receive(h2, true); // these stack
            GlobalLookup.Player.Receive(sword, true); // this does not
            GlobalLookup.Player.Equip(sword, true); // this fires on the first tick
        }
        static void Main(string[] args) {
            Initialize();
            CreateTestData();
        }
    }
}
