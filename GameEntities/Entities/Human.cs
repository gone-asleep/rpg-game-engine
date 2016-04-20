using GameEngine;
using GameEngine.AI;
using GameEngine.Effects;
using GameEngine.Entities;
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

namespace GameEntities.Entities {
    //public static class Human {
    //    private static bool isLoaded = false;

    //    private static readonly float[][] statsMultByOccupation = new float[][] {
    //       /*Warrior*/ new float[] { /*strength*/1.0f, /*Stamina*/1.0f, /*Wisdom*/1.0f, /*Inteligence*/1.0f, /*Charisma*/1.0f, /*Agility*/1.0f, /*Luck*/1.0f, /*Speed*/1.0f }, 
    //       /* Thief */ new float[] { /*strength*/1.0f, /*Stamina*/1.0f, /*Wisdom*/1.0f, /*Inteligence*/1.0f, /*Charisma*/1.0f, /*Agility*/1.0f, /*Luck*/1.0f, /*Speed*/1.0f }, 
    //    };

    //    private static readonly float[][] skillMultByOccupation = new float[][] {
    //       /*Warrior*/ new float[] { /*strength*/1.0f, /*Stamina*/1.0f, /*Wisdom*/1.0f, /*Inteligence*/1.0f, /*Charisma*/1.0f, /*Agility*/1.0f, /*Luck*/1.0f, /*Speed*/1.0f }, 
    //       /* Thief */ new float[] { /*strength*/1.0f, /*Stamina*/1.0f, /*Wisdom*/1.0f, /*Inteligence*/1.0f, /*Charisma*/1.0f, /*Agility*/1.0f, /*Luck*/1.0f, /*Speed*/1.0f }, 
    //    };

    //    private static float[] CalculateStatsByOccupationAndLevel(int level, int occupationIndex) {
    //        return statsMultByOccupation[occupationIndex].Select(i => i * level).ToArray();
    //    }

    //    private static float[] CalculateSkillsByOccupationAndLevel(int level, int occupationIndex) {
    //        return skillMultByOccupation[occupationIndex].Select(i => i * level).ToArray();
    //    }

    //    public static readonly Func<EntityProfile,Entity> HumanConstructor = (EntityProfile profile) => {


    //        int occupationIndex;
    //        if (profile.Occupation == EntityOccupation.None) {
    //            occupationIndex = GameGlobal.Rand.Next(0, statsMultByOccupation.Length - 1);
    //        } else {
    //            occupationIndex = (int)profile.Occupation;
    //        }

    //        float[] computedStats = CalculateStatsByOccupationAndLevel(profile.Level, occupationIndex);
    //        float[] computedSkills = CalculateSkillsByOccupationAndLevel(profile.Level, occupationIndex);

    //        IEntityInfo info = new EntityInfo(EntityRace.Human, (EntityOccupation)occupationIndex);
    //        IEntityStats stats = new EntityStats(computedStats);
    //        IEntitySkills skills = new EntitySkills(computedSkills);

    //        IEntityInventory inventory = new EntityInventory(60);
    //        IEntityAbility abilities = new EntityAbility(GeneralAbilities.All, ItemAbilities.None, EntityAbilities.ModifyInterationAbilities, EffectAbilities.ModifyMagicAbilities, AIAbilities.None);

                                  
    //        // based on occupation generate some inventory items
    //        Entity entity = new Entity(GameGlobal.IDs.Next(), info, skills, stats, inventory, abilities);
    //        return entity;
    //    };

    //    public static void Load() {
    //        if (!isLoaded) {
    //            GameGlobal.Factories.Entities.Add(EntityRace.Human, Human.HumanConstructor);
    //            isLoaded = true;
    //        }
    //    }
    //}
}
