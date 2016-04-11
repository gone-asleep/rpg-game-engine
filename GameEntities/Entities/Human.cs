using GameEngine;
using GameEngine.Entities.Skills;
using GameEngine.Entities.Stats;
using GameEngine.Factories;
using GameEngine.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEntities.Entities {
    public static class Human {

        private static readonly float[][] statsMultByOccupation = new float[][] {
           /*Warrior*/ new float[] { /*strength*/1.0f, /*Stamina*/1.0f, /*Wisdom*/1.0f, /*Inteligence*/1.0f, /*Charisma*/1.0f, /*Agility*/1.0f, /*Luck*/1.0f, /*Speed*/1.0f }, 
           /* Thief */ new float[] { /*strength*/1.0f, /*Stamina*/1.0f, /*Wisdom*/1.0f, /*Inteligence*/1.0f, /*Charisma*/1.0f, /*Agility*/1.0f, /*Luck*/1.0f, /*Speed*/1.0f }, 
        };
        


        public static readonly Func<EntityProfile, Entity> HumanConstructor = (profile) => {
            Entity entity = new Entity(GlobalLookup.IDs.Next());
            
            int occupationIndex;
            if (profile.Occupation == EntityOccupation.RandomOccupation) {
               occupationIndex = GlobalLookup.Rand.Next(0, statsMultByOccupation.Length-1);
            } else {
                occupationIndex = (int)profile.Occupation;
            }

            float[] statsMult = statsMultByOccupation[occupationIndex];

            entity.Stats.Set(StatType.Agility, statsMult[0] * profile.Level);
            entity.Stats.Set(StatType.Charisma, statsMult[0] * profile.Level);
            entity.Stats.Set(StatType.Inteligence, statsMult[0] * profile.Level);
            entity.Stats.Set(StatType.Luck, statsMult[0] * profile.Level);
            entity.Stats.Set(StatType.Stamina, statsMult[0] * profile.Level);
            entity.Stats.Set(StatType.Strength, statsMult[0] * profile.Level);
            entity.Stats.Set(StatType.Wisdom, statsMult[0] * profile.Level);
            entity.Stats.Set(StatType.Speed, statsMult[0] * profile.Level);
            
            // based on occupation generate some skills

            // based on occupation generate some inventory items

            return entity;
        };

    }
}
