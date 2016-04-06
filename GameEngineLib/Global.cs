using GameEngine.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine {
    public enum SkillStatInfoFunction : short {
        None,
        LinearPercent
    };

    public enum WeaponStatCalculationFunction : short {
        None,
        DamageAndDexterity,
        StrengthAndDexterity,
        DamageAndInteligence
    }

    public struct WeaponStatInfo {
        public WeaponStatCalculationFunction Function;
        public SkillType appliedSkill;
        public string name;
        public float attackModifier;
        public float attackDamageBase;
        public float attackDamageRange;
        public float attackSpeedBase;
    }

    public struct SkillStatInfo {
        public SkillStatInfoFunction Function;
        public float BaseValue;
        public float ModifierValue;
        public SkillStatInfo(SkillStatInfoFunction function, float baseValue, float modifierValue) {
            Function = function;
            BaseValue = baseValue;
            ModifierValue = modifierValue;
        }
    }
    

    public static class GameGlobal {
        public static readonly int StatCount = Enum.GetNames(typeof(StatType)).Count(); 
        public static readonly int SkillCount = Enum.GetNames(typeof(SkillType)).Count(); 
        
        public static float CurrentTick { get; private set; }

        public static ItemFactory ItemFactory = new ItemFactory();
        public static EntityFactory EntityFactory = new EntityFactory();
        public static MapFactory MapFactory = new MapFactory();
        public static EffectFactory EffectFactory = new EffectFactory();

        private static SkillStatInfo[][] SkillStateInfo;
        private static WeaponStatInfo[] WeaponStatInfo;
        static GameGlobal() {
            // initialize the skill state info
            SkillStateInfo = new SkillStatInfo[SkillCount][];
            for (int i = 0; i < GameGlobal.StatCount - 1; i++)
                SkillStateInfo[i] = new SkillStatInfo[StatCount];
        }
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
            WeaponStatInfo info = WeaponStatInfo[(int)item.TypeCode];

            if (info.Function == WeaponStatCalculationFunction.StrengthAndDexterity) {
                float augmentedStrength = stats.Get(info.appliedSkill, StatType.Strength); // this doesn't have to go back to the object, we have all info here
                return (item.Quality * info.attackDamageBase) + // the base attack damage multiplied by the quality of the weapon
                        (augmentedStrength * info.attackModifier);
            }
            return 0;
        }


        public static World World = new World();
        public static Entity Player { get; private set; }
        public static void SetPlayer(Entity player) {
            Player = player;
            World.Entities.Add(player);
        }
        private static int nextID = 0;
        public static int GetNextID() {
            return nextID++;
        }
        public static void Tick(float amount=1.0f) {
            CurrentTick+=amount;
            World.Refresh();
        }

    }

    public enum ConstructType {
        Item,
        Entity
    }

    public enum EffectTypeCode {
        Strengthen, // these should be real entries
        Weaken, 
    }

    public enum EffectClassCode {
        EntityStatusEffect
    }

    public enum EntityTypeCode {
        Human,
        Orc,
        Goblin,
        None
    }

    public enum ItemTypeCode {
        LongSword,
        ShortSword,
        HealingPotion
    }
    
    public enum ItemClassCode : long {
        Stackable = 1 >> 63,
        Potion = 1 | Stackable,
        Weapon = 2,
    }

    public enum MapTypeCode : int {
        Test = 0
    }

    public enum ItemEquipType : int {
        None = 0,
        Head = 1,
        Neck = 2,
        Chest = 4,
        Legs = 8,
        Feet = 16,
        Arms = 32,
        Shoulders = 64,
        Back = 128,
        LeftHand = 256,
        RightHand = 512
    }


    public enum SkillType : int {
        LightArmor = 0,
        HeavyArmor = 1,
        OneHanded = 2,
        TwoHanded = 3,
        Blunt = 4,
        Sneak = 5,
        Pickpocket = 6,
        LockPicking = 7
    }

    
    public enum StatType : int {
        Strength = 0,
        Stamina = 1,
        Wisdom = 2,
        Inteligence = 3,
        Charisma = 4,
        Agility = 5,
        Luck = 6,
        Speed = 7
    }

    public enum StatValueOp : int {
        None = 0,
        Multiply = 1,
        Add = 2
    }

}
