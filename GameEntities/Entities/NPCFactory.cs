using GameEngine;
using GameEngine.AI;
using GameEngine.Effects;
using GameEngine.Entities;
using GameEngine.Entities.Skills;
using GameEngine.Entities.Stats;
using GameEngine.Factories;
using GameEngine.Global;
using GameEngine.Items;
using GameEntities.Items;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weighted_Randomizer;

namespace GameEntities.Entities {
    // fisherman inspiration
    // http://runescape.wikia.com/wiki/Fish_Flingers

    [ProtoContract]
    public class NPCFactory : IFactoryProducer<IEntity, EntityProfile> {
        private ItemsFactory weaponFactory;

        public static readonly IDictionary<EntityRace, string[]> Names = new Dictionary<EntityRace, string[]>() {
            { (EntityRace.Human | EntityRace.Family), new string[] {
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
            { (EntityRace.Orc | EntityRace.Male), new string[] {
                "Gat","Xnath","Magub","Naghat","Drikdarok","Wutgar","Torg","Opeg","Bugdul","Onugug"
            } },
            { (EntityRace.Human | EntityRace.Female), new string[] {
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
            { (EntityRace.Human | EntityRace.Male), new string[] {
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

        private IDictionary<EntityOccupation, IWeightedRandomizer<NewItemCode[]>> inventoryItemProfiles = new Dictionary<EntityOccupation, IWeightedRandomizer<NewItemCode[]>>();

        private void buildInventoryItemProfiles() {
            inventoryItemProfiles[EntityOccupation.Warrior] = new StaticWeightedRandomizer<NewItemCode[]>() {
                { new NewItemCode[]{ NewItemCode.Weapon }, 1},
                { new NewItemCode[]{ NewItemCode.Weapon, NewItemCode.Shield }, 1},
                { new NewItemCode[]{ NewItemCode.Weapon, NewItemCode.Potion }, 1},
                { new NewItemCode[]{ NewItemCode.Weapon, NewItemCode.Potion, NewItemCode.Potion}, 1},
                { new NewItemCode[]{ NewItemCode.Weapon, NewItemCode.Potion, NewItemCode.ArmorHead}, 1},
                { new NewItemCode[]{ NewItemCode.Weapon, NewItemCode.Potion, NewItemCode.ArmorHead, NewItemCode.ArmorChest}, 1},
            };
            inventoryItemProfiles[EntityOccupation.None] = new StaticWeightedRandomizer<NewItemCode[]>() {
                { new NewItemCode[]{ NewItemCode.Weapon, NewItemCode.ArmorChest, NewItemCode.ArmorLegs, NewItemCode.ArmorFeet }, 1},
            };
            inventoryItemProfiles[EntityOccupation.Fisherman] = new StaticWeightedRandomizer<NewItemCode[]>() {
                { new NewItemCode[]{ NewItemCode.Weapon, NewItemCode.Tool, NewItemCode.ToolMaterial, NewItemCode.ArmorChest, NewItemCode.ArmorLegs, NewItemCode.ArmorFeet }, 1},
                { new NewItemCode[]{ NewItemCode.Weapon, NewItemCode.Tool, NewItemCode.ToolMaterial, NewItemCode.ArmorChest, NewItemCode.ArmorLegs, NewItemCode.ArmorFeet }, 1},
                { new NewItemCode[]{ NewItemCode.Weapon, NewItemCode.Tool, NewItemCode.ToolMaterial, NewItemCode.ArmorChest, NewItemCode.ArmorLegs }, 1},
                { new NewItemCode[]{ NewItemCode.Weapon, NewItemCode.Tool, NewItemCode.ToolMaterial, NewItemCode.ArmorHead, NewItemCode.ArmorChest, NewItemCode.ArmorLegs }, 1},
                { new NewItemCode[]{ NewItemCode.Weapon, NewItemCode.Tool, NewItemCode.ToolMaterial, NewItemCode.ArmorHead,  NewItemCode.ArmorLegs }, 1},
            };
        }

        // this should probably be generated using the neural network
        private float[][] nonSpecifigBaseStatsMultiple = new float[][] {
            // merchant distribution .. base on charisma, 
            new float[] { /*strength*/0.1f, /*Stamina*/0.1f, /*Wisdom*/0.1f, /*Inteligence*/0.1f, /*Charisma*/0.2f, /*Agility*/0.1f, /*Luck*/0.2f, /*Dexterity*/0.1f },
            // fighter type distribution
            new float[] { /*strength*/0.3f, /*Stamina*/0.2f, /*Wisdom*/0.05f, /*Inteligence*/0.05f, /*Charisma*/0.05f, /*Agility*/0.2f, /*Luck*/0.05f, /*Dexterity*/0.1f },
            // commoner type distribution
            new float[] { /*strength*/0.1f, /*Stamina*/0.1f, /*Wisdom*/0.1f, /*Inteligence*/0.05f, /*Charisma*/0.2f, /*Agility*/0.1f, /*Luck*/0.2f, /*Dexterity*/0.1f },
        };

        public NPCFactory(ItemsFactory weaponFactory) {
            this.weaponFactory = weaponFactory;
            occupationBaseStatsMultiple = new Dictionary<EntityOccupation, float[]>();
            this.buildInventoryItemProfiles();
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

        public EntityOccupation GetRandomOccupation() {
            return (EntityOccupation)GameGlobal.RandomInt(1, GameGlobal.OccupationTypeCount);
        }

        public EntityRace GetRandomRace() {
            int randomGender = GameGlobal.RandomInt(1, 2);
            int randomRace = (int)EntityRace.Human; // only do human for now
            //int randomRace = GameGlobal.RandomInt(GameGlobal.MinRaceCode, GameGlobal.MaxRaceCode);
            return (EntityRace)(randomGender | randomRace);
        }

        public IEntityStats GetStatsPoints(EntityOccupation occupation, float pointsToDistribute) {

            return new EntityStats(this.occupationBaseStatsMultiple[occupation].Select(i => (float)Math.Floor((i * (pointsToDistribute - 16.0f)+2.0f))).ToArray());
        }

        private Dictionary<EntityOccupation, float[]> occupationBaseStatsMultiple;


       
         private IEntityInfo GenerateEntityInfo(EntityRace race, EntityOccupation occupation) {
            if (occupation == EntityOccupation.None) {
                occupation = GetRandomOccupation();
            }
            if (race == EntityRace.None) {
                race = GetRandomRace();
            }

            string firstName = "";
            string lastName = "";
            if (race.HasFlag(EntityRace.Male)) {
                int nameIndex = GameGlobal.RandomInt(0, Names[EntityRace.Human | EntityRace.Male].Length);
                firstName = Names[EntityRace.Human | EntityRace.Male][nameIndex];
                nameIndex = GameGlobal.RandomInt(0, Names[EntityRace.Human | EntityRace.Family].Length);
                lastName = Names[EntityRace.Human | EntityRace.Family][nameIndex];
            } else if (race.HasFlag(EntityRace.Female)) {
                int nameIndex = GameGlobal.RandomInt(0, Names[EntityRace.Human | EntityRace.Female].Length);
                firstName = Names[EntityRace.Human | EntityRace.Male][nameIndex];
                nameIndex = GameGlobal.RandomInt(0, Names[EntityRace.Human | EntityRace.Family].Length);
                lastName = Names[EntityRace.Human | EntityRace.Family][nameIndex];
            }
            return new EntityInfo(race, occupation, 25 * 365, firstName, lastName);
        }

        public void AddOccupation(IEntity entity, EntityOccupation occupation) {
            if (this.inventoryItemProfiles.Keys.Contains(occupation)) {
                // get the item profile
                var itemProfileArray = this.inventoryItemProfiles[occupation].NextWithReplacement();
                for (int i = 0; i < itemProfileArray.Length; i++) {
                    IItem item = weaponFactory.Create(new ItemProfile(1, occupation, itemProfileArray[i]));
                    if (item != null) {
                        entity.Inventory.Set(item, InventorySlot.Any, entity.Stats);
                    }
                }
            }
        }

        public IEntity Create(EntityProfile profile) {
            IEntityInfo info = GenerateEntityInfo(profile.Race, profile.Occupation);
            IEntityStats stats = GetStatsPoints(profile.Occupation, profile.Level * 10);
            IEntitySkills skills = new EntitySkills();
            IInventory inventory = new Inventory(20); // smaller inventory for entity
            IEntityAbility abilities = new EntityAbility(GeneralAbilities.All, ItemAbilities.None, EntityAbilities.ModifyInterationAbilities, EffectAbilities.ModifyMagicAbilities, AIAbilities.None);
            Guid id = Guid.NewGuid();
            IEntity entity = new Entity(id, info, skills, stats, inventory, abilities);

            this.AddOccupation(entity, info.Occupation);

            return entity;
        }
    }
}