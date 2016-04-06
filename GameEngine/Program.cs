using GameEngine.Effects;
using GameEngine.Worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine {
    class Program {
        static void Initialize() {
            GameGlobal.ItemFactory.AddFactoryConstructor(ItemTypeCode.LongSword, (profile) => {
                Item sword = new Item("A Sword", ItemTypeCode.LongSword, ItemClassCode.Weapon, ItemEquipType.LeftHand);
                sword.Modifier.Define(StatType.Strength, StatValueOp.Add, 4.0F);
                return sword;
            });

            GameGlobal.ItemFactory.AddFactoryConstructor(ItemTypeCode.HealingPotion, (profile) => {
                Item item = new Item("Potion of Healing", ItemTypeCode.HealingPotion, ItemClassCode.Potion, ItemEquipType.None);
                item.Modifier.Define(StatType.Strength, StatValueOp.Add, 4.0F);
                return item;
            });

            GameGlobal.EntityFactory.AddFactoryConstructor(EntityTypeCode.Human, (profile) => {
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

            GameGlobal.MapFactory.AddFactoryConstructor(MapTypeCode.Test, (profile) => {
                var map = new Map(20, 20, 40);
                map.Tiles[5][5] = 3;
                map.SetTiles(0, 0, 3, 3, 1);
                map.SetTiles(3, 2, 1, 6, 1);
                return map;
            });


            GameGlobal.EffectFactory.AddFactoryConstructor(EffectTypeCode.Strengthen, (profile) => {
                float seconds = 30.0f;
                Effect effect = new Effect(EffectTypeCode.Strengthen, EffectClassCode.EntityStatusEffect ,GameGlobal.CurrentTick,GameGlobal.CurrentTick + seconds);
                effect.Modifier.Define(StatType.Strength, StatValueOp.Multiply, 1.5f); // +50%
                return effect;
            });

            GameGlobal.EffectFactory.AddFactoryConstructor(EffectTypeCode.Weaken, (profile) => {
                float seconds = 30.0f;
                Effect effect = new Effect(EffectTypeCode.Weaken, EffectClassCode.EntityStatusEffect, GameGlobal.CurrentTick, GameGlobal.CurrentTick + seconds);
                effect.Modifier.Define(StatType.Strength, StatValueOp.Multiply, 0.5f); //-50%
                return effect;
            });

        }

        static Item sword;
        static void CreateTestData() {
            var h1 = GameGlobal.ItemFactory.Generate(typeCode: ItemTypeCode.HealingPotion);
            var h2 = GameGlobal.ItemFactory.Generate(typeCode: ItemTypeCode.HealingPotion);
            sword = GameGlobal.ItemFactory.Generate(typeCode: ItemTypeCode.LongSword);
            GameGlobal.SetPlayer(GameGlobal.EntityFactory.Generate(typeCode: EntityTypeCode.Human));
            GameGlobal.World.SetMap(GameGlobal.MapFactory.Generate(typeCode: MapTypeCode.Test));

            GameGlobal.Player.Inventory.Add(h1);
            GameGlobal.Player.Inventory.Add(h2); // these stack
            GameGlobal.Player.Inventory.Add(sword); // this does not

            GameGlobal.Player.Equip(sword); // this fires on the first tick
        }
        static void Main(string[] args) {
            Initialize();
            CreateTestData();
        }
    }
}
