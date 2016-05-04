using System;

namespace GameData {
    /// <summary>
    /// For lack of a better word... culture..
    /// this enum describes some basics about how 
    /// an entity may dress/ speak/ behave
    /// this list is off the cuff right now.. needs refinement
    /// This simplifies character creation
    /// and has a role in AI
    /// 
    /// this is not currenlty used
    /// </summary>
    [Flags]
    public enum EntityCulture {
        Creature        = 1,
        Savage          = 2,
        Simple          = 3,
        Common          = 4,
        Educated        = 5,
        HighlyEducated  = 6,

        Nobel,
        Royalty,
        Peasant,
        Adventurer,
        Brute,
        Beggar,
        Mystic,
        
        Hermit,
        Monster
    }
}
