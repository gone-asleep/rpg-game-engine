using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Entities {
    /// <summary>
    /// Entity Occupation
    /// Determins part of an entities name
    /// Has a central role in random entity generation
    /// 
    /// source for alot of the roles
    /// http://juliahwest.com/prompts/fantasy_job_list.html
    /// </summary>
    public enum EntityOccupation : int{
        None = 0,// special case describes entities that do not have a distinct game given occupation
        Warrior = 1,
        Theif = 2,
        Bard = 3,
        Barbarian = 4,
        /// <summary>
        /// a member of a religious community of men typically living under vows of poverty, chastity, and obedience.
        /// </summary>
        Monk = 5,
        Paladin = 6,
        Ranger = 7,
        Sorcerer = 8,
        Warlock = 9,
        // less playable types , but would be usefull for defining entities with certain behaviors/inventories
        Drunk,
        Fisherman,
        Guard,
        Miner,
        Scout,
        Alchemist,
        Aristocrat,
        Armorer,
        Actor,
        Artisan,
        /// <summary>
        /// A Lawyer
        /// </summary>
        Advocate,
        Astrologer,
        Baker,
        Banker,
        Barkeep,
        Barmaid,
        BeerSeller,
        Beggar,
        Blacksmith,
        Boatman,
        Bookseller,
        Bookbinder,
        Brewer,
        Brigand,
        Butcher,
        /// <summary>
        /// a person who makes or sells cutlery.
        /// </summary>
        Cutler,
        Carpenter,
        //is the study and practice of making maps. Combining science, aesthetics, and technique,
        //cartography builds on the premise that reality can be modeled in ways that communicate spatial information effectively.
        Cartographer,
        Clergyman,
        Chirurgeon, // type of doctor....
        Clerk,
        Clothworker,
        Cobbler,
        Commander, // use with guard
        Copyist,
        Counselor,
        Courtier,
        Dairymaid,
        Diplomat,
        Distiller,
        Diviner,
        Doctor,
        DomesticServant,
        Farmer,

        Fishmonger,
        /// <summary>
        /// A Soldier in the Infantry.
        /// </summary>
        Footman,
        /// <summary>
        /// A person who sells, makes, repairs, alters, cleans, or otherwise deals in clothing made of fur.
        /// </summary>
        Furrier,
        /// <summary>
        /// A galley slave is a slave rowing in a galley, either a convicted criminal sentenced to work at the oar (French: galérien), or a kind of human chattel, often a prisoner of war, assigned to his duty of rowing.
        /// </summary>
        GalleySlave,
        Gardener,
        General,
        Gladiator,
        Glovemaker,
        /// <summary>
        /// a person who sells food and small household goods.
        /// </summary>
        Grocer,
        /// <summary>
        /// a soldier of a regiment of Guards
        /// </summary>
        GuardsMan,
        /// <summary>
        /// an association of people with similar interests or pursuits
        /// exp. a medieval association of merchants or craftsmen.
        /// someone risen in status to be considered at the top of their profession.
        /// </summary>
        Guildmaster,
        Hatmaker,
        /// <summary>
        /// 
        /// </summary>
        Hearthwitch,
        /// <summary>
        /// an official messenger bringing news.
        /// </summary>
        Herald,

        Herbalist,
        Herder,
        Hermit,
        //a man, typically on horseback, who held up travelers in order to rob them.
        Highwayman,
        Historian,
        Housemaid,
        Hunter,
        /// <summary>
        /// One who illuminates manuscripts or other objects.
        /// </summary>
        Illuminator,
        Infantryman,
        Innkeeper,
        Interpreter,
        Inventor,
        Jailer,
        Jester,
        Jeweler,
        /// <summary>
        /// singer or musician traveling from place to place
        /// </summary>
        Jongleur,
        Judge, // maybe no judge
        King,
        Knight,
        Laborer,
        Leatherworker,
        Librarian,
        Linguist,
        Locksmith,
        Longbowman,
        /// <summary>
        /// a person employed in a port to load and unload ships.
        /// </summary>
        Longshoreman,
        Lord,
        Maidservant,
        /// <summary>
        /// the chief steward of a large household.
        /// </summary>
        MajorDomo,
        /// <summary>
        /// a builder and worker in stone.
        /// </summary>
        Mason,
        Mayor,
        /// <summary>
        /// a dealer in textile fabrics, especially silks, velvets, and other fine materials.
        /// </summary>
        Mercer,
        /// <summary>
        /// a person or company involved in trade, especially one dealing with foreign countries or supplying merchandise to a particular trade.
        /// </summary>
        Merchant,
        Messenger,
        Midwife,
        Miller,
        /// <summary>
        /// a head of a government department.
        /// </summary>
        Minister,
        /// <summary>
        /// singer or musician 
        /// </summary>
        Minstrel,
        /// <summary>
        /// an undertaker.
        /// </summary>
        Mortician,

        Necromancer,

        Noble,

        Nun,
        /// <summary>
        /// Sells old clothes... enough said
        /// </summary>
        OldClothesSeller,
        /// <summary>
        /// attendant to a knight, i.e., an apprentice squire. 
        /// </summary>
        Page,
        Painter,
        /// <summary>
        /// an outcast
        /// </summary>
        Pariah,
        /// <summary>
        /// creates and sells pastries
        /// </summary>
        PastryCook,
        Peasant,
        Perfumer,
        Philosopher,
        Pigkeeper,
        Pilgrim,
        Pirate,
        Plasterer,
        Potter,
        Priest,
        /// <summary>
        /// an armed ship owned and officered by private individuals holding a government commission and authorized for use in war, especially in the capture of enemy merchant shipping.
        /// </summary>
        Privateer,
        Prostitute,
        Pursemaker,
        Queen,
        Ratcatcher,
        /// <summary>
        /// the president of a village or town council.
        /// </summary>
        Reeve,
        Roofer,
        Ropemaker,

        RoyalAdviser,
        Rugmaker,
        Saddler,
        Sailor,
        Sculptor,
        Scavenger,
        Scholar,
        /// <summary>
        /// a clerk, scribe, or notary.
        /// </summary>
        Scrivener,
        Seamstress,
        Servant,
        Shaman,
        Shepherd,
        ShipCaptain,
        Shoemaker,
        Silversmith,
        Slave,
        Slaver,
        Smith,
        Soldier,
        SpiceMerchant,
        /// <summary>
        /// same thing as a Page... remove one of them
        /// </summary>
        Squire,
        /// <summary>
        /// A person who works in a stable.
        /// </summary>
        Stablehand,
        /// <summary>
        /// a person employed, or a contractor engaged, at a dock to load and unload cargo from ships.
        /// [dupe of another entry]
        /// </summary>
        Stevedore ,
        Stonemason,
        Storyteller,
        Steward,
        StreetSweeper,
        Student,
        Surveyor,
        /// <summary>
        /// a man who fights with a sword (typically with his level of skill specified).
        /// </summary>
        Swordsman,
        StreetKid,
        StreetSeller,
        /// <summary>
        /// a person who acts obsequiously toward someone important in order to gain advantage.
        /// </summary>
        Sycophant,
        Tailor,
        Tanner,
        Tavernkeeper,
        TaxCollector,
        Teacher,
        /// <summary>
        /// a driver of a team of animals.
        /// </summary>
        Teamster,
        Thatcher,
        /// <summary>
        /// a person who travels from place to place mending metal utensils as a way of making a living.
        /// </summary>
        Tinker,
        Torturer,
        /// <summary>
        /// a person employed to make public announcements in the streets or marketplace of a town.
        /// </summary>
        TownCrier,
        Toymaker,
        Trapper,
        Vendor,
        Veterinarian,
        /// <summary>
        /// a wine merchant.
        /// </summary>
        Vintner,
        Viking,
        WaterCarrier,
        Weaver,
        Wetnurse,
        Witch,
        Woodcarver,
        Woodcutter,
        WoodSeller,
        Wrestler,
        Writer
    }
}
