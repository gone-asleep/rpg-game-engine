using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Entities {
    public enum EntityOccupation {
        None,// special case describes entities that do not have a distinct game given occupation
        Warrior,
        Theif,
        Bard,
        Barbarian,
        Monk,
        Paladin,
        Ranger,
        Sorcerer,
        Warlock,
        // less playable types , but would be usefull for defining entities with certain behaviors/inventories
        Drunk,
        Fisherman,
        Guard,
        Miner,
        Scout,
    }
}
