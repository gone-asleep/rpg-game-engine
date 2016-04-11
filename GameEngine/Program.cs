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
                sword.Modifier.Define(StatType.Strength, StatModifierType.Add, 4.0F);
                return sword;
            });

            GlobalLookup.Factories.Items.Add(ItemType.HealingPotion, (profile) => {
                Item item = new Item("Potion of Healing", ItemType.HealingPotion, ItemClassCode.Potion, ItemEquipType.LeftHand);
                item.Modifier.Define(StatType.Strength, StatModifierType.Add, 4.0F);
                return item;
            });

            

            GlobalLookup.Factories.Maps.Add(MapType.Test, (profile) => {
                var map = new Map(20, 20, 40);
                map.Tiles[5][5] = 3;
                map.SetTiles(0, 0, 3, 3, 1);
                map.SetTiles(3, 2, 1, 6, 1);
                return map;
            });


            GlobalLookup.Factories.Effects.Add(EffectType.Strengthen, (profile) => {
                float seconds = 30.0f;
                Effect effect = new Effect(EffectType.Strengthen, EffectClass.EntityStatusEffect ,GlobalLookup.Time.Current ,GlobalLookup.Time.Current + seconds);
                effect.Modifier.Define(StatType.Strength, StatModifierType.Multiply, 1.5f); // +50%
                return effect;
            });

            GlobalLookup.Factories.Effects.Add(EffectType.Weaken, (profile) => {
                float seconds = 30.0f;
                Effect effect = new Effect(EffectType.Weaken, EffectClass.EntityStatusEffect, GlobalLookup.Time.Current, GlobalLookup.Time.Current + seconds);
                effect.Modifier.Define(StatType.Strength, StatModifierType.Multiply, 0.5f); //-50%
                return effect;
            });

        }

        static Item sword;
        static void CreateTestData() {
            var h1 = GlobalLookup.Factories.Items.Generate(typeCode: ItemType.HealingPotion);
            var h2 = GlobalLookup.Factories.Items.Generate(typeCode: ItemType.HealingPotion);
            sword = GlobalLookup.Factories.Items.Generate(typeCode: ItemType.LongSword);
            GlobalLookup.AddPlayer(GlobalLookup.Factories.Entities.Generate(typeCode: EntityType.Human));
            GlobalLookup.World.SetMap(GlobalLookup.Factories.Maps.Generate(typeCode: MapType.Test));

            GlobalLookup.Player.Receive(h1);
            GlobalLookup.Player.Receive(h2); // these stack
            GlobalLookup.Player.Receive(sword); // this does not
            GlobalLookup.Player.Equip(sword); // this fires on the first tick
        }
        static void Main(string[] args) {
            //Initialize();
            //CreateTestData();
            for (float i = 0.001f; i < 1.0; i+= 0.01f) {

                Console.WriteLine(new string('*', (int)(10 * Extensions.NextGaussian(i, .5, 1))));
            }
            Console.ReadKey();
        }
    }
}
