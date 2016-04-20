using GameEngine;
using GameEngine.Entities;
using GameEngine.Entities.Stats;
using GameEngine.Factories;
using GameEngine.Global;
using GameEngine.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEntities.Entities {
    //public static class Player {
    //    private static bool isLoaded = false;

    //    private static readonly float[][] statsMultByOccupation = new float[][] {
    //       /*Warrior*/ new float[] { /*strength*/1.0f, /*Stamina*/1.0f, /*Wisdom*/1.0f, /*Inteligence*/1.0f, /*Charisma*/1.0f, /*Agility*/1.0f, /*Luck*/1.0f, /*Speed*/1.0f }, 
    //       /* Thief */ new float[] { /*strength*/1.0f, /*Stamina*/1.0f, /*Wisdom*/1.0f, /*Inteligence*/1.0f, /*Charisma*/1.0f, /*Agility*/1.0f, /*Luck*/1.0f, /*Speed*/1.0f }, 
    //    };

    //    public static readonly Func<EntityProfile, Entity> HumanConstructor = (profile) => {
    //        var h1 = GameGlobal.Factories.Items.Generate(typeCode: ItemType.HealingPotion);
    //        var h2 = GameGlobal.Factories.Items.Generate(typeCode: ItemType.HealingPotion);
    //        Item sword = GameGlobal.Factories.Items.Generate(typeCode: ItemType.LongSword);
    //        Entity player = GameGlobal.Factories.Entities.Generate(typeCode: EntityRace.Human);
    //        player.Inventory.Set(h1,-1);
    //        player.Inventory.Set(h2, -1);
    //        player.Inventory.Set(sword, 5);
    //        player.Inventory.SetEquiped(5, player.Stats, player.Abilities);
    //        return player;
    //    };

    //    public static void Load() {
    //        if (!isLoaded) {
    //            // don't load this yet
    //            GameGlobal.Factories.Entities.Add(EntityRace.Human, Human.HumanConstructor);
    //            isLoaded = true;
    //        }
    //    }
    //}
}
