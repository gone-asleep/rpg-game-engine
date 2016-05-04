using System;

namespace GameData {
    // 2 bits gender specification
    // 6 bits race specification


    [Flags]
    public enum EntityRace : int {
        /// <summary>
        /// Entity is not considered male or female
        /// </summary>
        Other = 3,
        /// <summary>
        /// Entity is considered male
        /// </summary>
        Male = 1,
        /// <summary>
        /// Entity is considered female
        /// </summary>
        Female = 2,

        Human = 1 << 2, // start bit for race indication
        Orc = 2 << 2,
        Goblin = 3 << 2,
        Dwarf = 4 << 2,
        Elf = 5 << 2,
        Gnome = 6 << 2,
        Halfling = 7 << 2,
        HalfElf = 8 << 2,
        HalfOrc = 9 << 2,
        Kobold = 10 << 2,
        Drake = 11 << 2,
        Mantis = 12 << 2,
        HalfGiant = 13 << 2,
        Centaur = 14 << 2,
        Fairy = 15 << 2,
        Troll = 16 << 2,
        Mermaid = 17 << 2,
        Nymph = 18 << 2,



        Container = 4096 * 2,// special case
        Family = 4096, // mostly indicates this entity has a last name...
        None = 0 // special case 
    }
}
