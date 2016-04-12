using GameEngine.Entities.Skills;
using GameEngine.Entities.Stats;
using GameEngine.Factories;
using GameEngine.Global.Providers;
using GameEngine.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Global {
    public static class GameGlobal {

        /// <summary>
        /// The Total Number of Stat Types
        /// </summary>
        public static readonly int StatTypeCount = Enum.GetNames(typeof(StatType)).Count();

        /// <summary>
        /// The Total Number of Skill Types
        /// </summary>
        public static readonly int SkillTypeCount = Enum.GetNames(typeof(SkillType)).Count();

        /// <summary>
        /// The Total Number of Items Types
        /// </summary>
        public static readonly int ItemTypeCount = Enum.GetNames(typeof(ItemType)).Count();

        /// <summary>
        /// The Total Number of ways an item can be equiped to a player
        /// </summary>
        public static readonly int EquipTypeCount = Enum.GetNames(typeof(ItemEquipType)).Count();

        /// <summary>
        /// Factories Used for generating all Game Items/Entities/Effects
        /// </summary>
        public static FactoriesProvider Factories { get; private set; }

        /// <summary>
        /// ID Provider provides IDs to all Game objects
        /// </summary>
        public static IDProvider IDs { get; private set; }

        /// <summary>
        /// Provides the current game time, used to coordinate actions, and movements
        /// </summary>
        public static TimeProvider Time { get; private set; }

        /// <summary>
        /// The World Object Provides the area to add Map Information, Entities, And Items
        /// Currently in the game
        /// </summary>
        public static World World { get; private set; }

        /// <summary>
        /// The Current Player
        /// </summary>
        public static Entity Player { get; private set; }
        
        /// <summary>
        /// This Lookup Provides Information about how Skills Effect an Entities Stats
        /// When used
        /// </summary>
        private static SkillStatInfo[][] SkillStateInfo;

        /// <summary>
        /// This Lookup Provides Information about how skills can be applied to 
        /// Item Types
        /// </summary>
        private static ItemAppliedSkillStatInfo[] SkilledItemUseInfo;


        /// <summary>
        /// Global Constructor
        /// </summary>
        static GameGlobal() {
            Factories = new FactoriesProvider();

            // initialize the skill state info
            SkillStateInfo = new SkillStatInfo[SkillTypeCount][];
            for (int i = 0; i < GameGlobal.StatTypeCount - 1; i++)
                SkillStateInfo[i] = new SkillStatInfo[StatTypeCount];

            // initialize Weapon Stat Info
            SkilledItemUseInfo = new ItemAppliedSkillStatInfo[ItemTypeCount];

            // create the ID Provider
            IDs = new IDProvider();

            // create the Time Provider
            Time = new TimeProvider();

            // create a blank world object
            World = new World();

            Rand = new Random();
        }

        public static Random Rand { get; private set; }

        public static void DefineSkillEffect(SkillType skill, StatType type, SkillStatInfo definition) {
            SkillStateInfo[(int)skill][(int)type] = definition;

        }
        public static float CalculateSkillIncrease(SkillType skill, float level) {
            // doesn't do much for now, just  return 1/floor(level)^2
            return 1.0f / (float)Math.Pow(2.0, Math.Floor(level));
        }



        public static float CalculateSkillEffect(SkillType skill, StatType type, float level, float current) {
        
           
            var info = SkillStateInfo[(int)skill][(int)type];
            if (info.Function == SkillStatInfoFunction.LinearPercent) {
                return current * (info.BaseValue + ((float)Math.Floor(level) * info.ModifierValue));
            } else { //None
                return current;
            }

        }

        public static float CalculateWeaponDamage(Item item, EntityStats stats) {
            ItemAppliedSkillStatInfo info = SkilledItemUseInfo[(int)item.Info.TypeCode];

            if (info.Function == WeaponStatCalculationFunction.StrengthAndDexterity) {
                //float augmentedStrength = stats.Get(info.appliedSkill, StatType.Strength); // this doesn't have to go back to the object, we have all info here
                //return (item.Quality * info.attackDamageBase) + // the base attack damage multiplied by the quality of the weapon
                //        (augmentedStrength * info.attackModifier);
            }
            return 0;
        }


        
        /// <summary>
        /// Sets the current player entity, and adds it to the world
        /// </summary>
        /// <param name="player"></param>
        public static void AddPlayer(Entity player) {
            // remove the current player from the world
            if (Player != null) {
                World.Entities.Remove(player);
            }
            // set the global player object
            Player = player;

            // add the new player to the world
            World.Entities.Add(player);
        }

        /// <summary>
        /// Progresses Game Time and Refreshes the World and it's contents
        /// </summary>
        /// <param name="amount"></param>
        public static void Tick(float amount = 1.0f) {
            GameGlobal.Time.Current += amount;
            World.Refresh(GameGlobal.Time.Current);
        }

        public static string GetName(Entity entity) {
            return "computed name of entity";
        }
        public static string GetName(Item item) {
            return "computed name of item";
        }
    }
}
