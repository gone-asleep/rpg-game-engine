using GameEngine;
using GameEngine.AI;
using GameEngine.Effects;
using GameEngine.Entities;
using GameEngine.Entities.Skills;
using GameEngine.Entities.Stats;
using GameEngine.Factories;
using GameEngine.Global;
using GameEngine.Items;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEntities.Entities {
    [ProtoContract]
    public class NPCFactory : IFactoryProducer<IEntity, EntityProfile> {
         public static readonly IDictionary<NameCatagoryCode, string[]> Names = new Dictionary<NameCatagoryCode, string[]>() {
            { NameCatagoryCode.HumanFamilyName, new string[] {
                "Athanassiadi","Accardo","Araoz","Albelin","Ambrogi",
                "Baertschi","Boje","Bolla","Benson","Balzar",
                "Carnini","Cleyn","Cornelis","Chevillon","Crisalli",
                "Duran","Dealavallade","Dichio","Deyaert","Durlacher",
                "Eigner","Escz","Etxabeguren","Engemann","Elizalde",
                "Fissler","Ferri","Farentino","Fliehman","Freindametz",
                "Grob","Gilca","Guintcehv","Guterres","Gottlieb",
                "Hawlik","Herzog","Haltzel","Hendrickson","Hinetze",
                "Irastorza","Ivanov","Ilica","Iagar","Ibarran",
                "Jansen","Jopie","Jorise","Jadin","Joosten",
                //k
                //l
                //m
                //n
                //o
                //...
              }
            },
            { NameCatagoryCode.HumanFemale, new string[] {
                "Alyvia","Agate","Arabeth","Ardra",
                "Brenna",
                "Caryne",
                "Dasi","Derris","Dynie",
                "Eryke","Errine",
                "Farale",
                "Gavina","Glynna",
                "Karran","Kierst","Kira","Kyale",
                "Ladia",
                "Mora","Moriana",
                "Quiss",
                "Sadi","Salina","Samia","Sephya","Shaundra","Siveth",
                "Thana",
                "Valiah",
                "Zelda"} },
            { NameCatagoryCode.HumanMale, new string[] {
                "Alaric","Alaron","Alynd","Asgoth",
                "Berryn",
                "Derrib",
                "Eryk","Evo",
                "Fausto",
                "Gavin","Gorth",
                "Jarak","Jasek",
                "Kurn",
                "Lan","Ledo","Lor",
                "Mavel","Milandro",
                "Sandar","Sharn",
                "Tarran","Thane","Topaz","Tor","Torc","Travys","Trebor","Tylien",
                "Vicart",
                "Zircon"
            }}
        };

        // this should probably be generated using the neural network
        private float[][] nonSpecifigBaseStatsMultiple = new float[][] {
            // merchant distribution .. base on charisma, 
            new float[] { /*strength*/0.1f, /*Stamina*/0.1f, /*Wisdom*/0.1f, /*Inteligence*/0.1f, /*Charisma*/0.2f, /*Agility*/0.1f, /*Luck*/0.2f, /*Dexterity*/0.1f },
            // fighter type distribution
            new float[] { /*strength*/0.3f, /*Stamina*/0.2f, /*Wisdom*/0.05f, /*Inteligence*/0.05f, /*Charisma*/0.05f, /*Agility*/0.2f, /*Luck*/0.05f, /*Dexterity*/0.1f },
            // commoner type distribution
            new float[] { /*strength*/0.1f, /*Stamina*/0.1f, /*Wisdom*/0.1f, /*Inteligence*/0.05f, /*Charisma*/0.2f, /*Agility*/0.1f, /*Luck*/0.2f, /*Dexterity*/0.1f },
        };

        public NPCFactory() {
            occupationBaseStatsMultiple = new Dictionary<EntityOccupation, float[]>();
            foreach (var occupation in Enum.GetValues(typeof(EntityOccupation))) {
                occupationBaseStatsMultiple.Add((EntityOccupation)occupation, GetRandomDistribution());
            }
        }
        private float[] GetRandomDistribution() {
            float[] result = new float[GameGlobal.StatTypeCount];
            float total = 0;
            for (int i = 0; i < GameGlobal.StatTypeCount; i++) {
                result[i] = GameGlobal.RandomFloat(0.0f, 10.0f);
                total += result[i];
            }
            for (int i = 0; i < GameGlobal.StatTypeCount; i++) {
                result[i] = result[i] / total;
            }
            return result;
        }
        public IEntityStats GetStatsPoints(EntityOccupation occupation, float pointsToDistribute) {

            return new EntityStats(this.occupationBaseStatsMultiple[occupation].Select(i => (float)Math.Floor((i * (pointsToDistribute - 16.0f)+2.0f))).ToArray());
        }

        private Dictionary<EntityOccupation, float[]> occupationBaseStatsMultiple;

        
        [ProtoMember(3)]
        codeProbability[][] Probabilities = new codeProbability[][] {
            new codeProbability[]{}, //HumanMiner 
            /*HumanGuard*/     
            new codeProbability[] { 
                new codeProbability(EntityOccupation.Guard, 0.1f), 
                new codeProbability(ItemType.Morningstar, 0.2f),
                new codeProbability(ItemType.Mace, 0.2f),
                new codeProbability(ItemType.GreatAxe, 0.2f),
                new codeProbability(ItemType.BastardSword, 0.1f),
                new codeProbability(ItemType.WarPick, 0.1f),
                new codeProbability(ItemType.ShortSword, 0.1f),
            }, // do we really want to list out all probabilities based on 
            /*HumanFarmer*/
            new codeProbability[] { 
                new codeProbability(ItemType.Dagger, 0.1f), 
                new codeProbability(ItemType.ShortSword, 0.2f),
                new codeProbability(ItemType.Crossbow, 0.2f)
            },
            /*HumanWeaponDealer*/
            new codeProbability[]{},
            /*HumanWeaponSmith*/ // large heavy weapons , tanks
            new codeProbability[]{},
            /*HumanMerchant*/ // smaller weapons
            new codeProbability[]{},
            /*HumanAdventurer*/
            new codeProbability[]{},
            /*HumanSquire*/
             new codeProbability[] { 
                new codeProbability(ItemType.QuarterStaff, 0.5f), 
                new codeProbability(ItemType.LongStaff, 0.5f),
                new codeProbability(ItemType.SpellBook, 0.5f)
            },
            /*HumanDrunk*/
            new codeProbability[]{},
            /*HumanBanker*/
            new codeProbability[] { 
                new codeProbability(ItemType.Dagger, 0.1f)
            }, 
            /*HumanInnKeeper*/
            new codeProbability[] { 
                new codeProbability(ItemType.Dagger, 0.1f)
            },
        };

         private IEntityInfo GenerateEntityInfo(NameCatagoryCode code, EntityRace race, EntityOccupation occupation) {
            int nameIndex = GameGlobal.RandomInt(0, Names[code].Length);
            string firstName = Names[code][nameIndex];
            string lastName = null;
            if (code.HasFlag(NameCatagoryCode.FamilyName)) {
                if (code.HasFlag(NameCatagoryCode.Human)) {
                    nameIndex = GameGlobal.RandomInt(0, Names[NameCatagoryCode.HumanFamilyName].Length);
                    lastName = Names[NameCatagoryCode.HumanFamilyName][nameIndex];
                }
            }
            return new EntityInfo(race, occupation, firstName, lastName);
        }

        public IEntity Create(EntityProfile profile) {
            IEntityInfo info = GenerateEntityInfo(profile.NameCode, profile.Race, profile.Occupation);
            IEntityStats stats = GetStatsPoints(profile.Occupation, profile.Level * 10);
            IEntitySkills skills = new EntitySkills();
            IInventory inventory = new Inventory(20); // smaller inventory for entity
            IEntityAbility abilities = new EntityAbility(GeneralAbilities.All, ItemAbilities.None, EntityAbilities.ModifyInterationAbilities, EffectAbilities.ModifyMagicAbilities, AIAbilities.None);
            Guid id = Guid.NewGuid();
            IEntity entity = new Entity(id, info, skills, stats, inventory, abilities);
            return entity;
        }
    }
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
