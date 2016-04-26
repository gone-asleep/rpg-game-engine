using GameEngine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Factories {
    public enum EntityProfileType {
        Specific,
        HumanMiner,
        HumanGuard,
        HumanFarmer,
        HumanWeaponDealer,
        HumanWeaponSmith,
        HumanMerchant,
        HumanAdventurer,
        HumanSquire,
        HumanDrunk,
        HumanBanker,
        HumanInnKeeper,
        // and many .. many more... consider dropping race from this condition and allow it to be passed in
    }

    [Flags]
    public enum NameCatagoryCode : int {
        Female = 1,
        Male = 2,
        FamilyName = 4,
        Human = 8,
        HumanFamilyName = (int)NameCatagoryCode.Human | (int)NameCatagoryCode.FamilyName,
        HumanFemale = (int)NameCatagoryCode.Female | (int)NameCatagoryCode.Human | (int)NameCatagoryCode.HumanFamilyName,
        HumanMale = (int)NameCatagoryCode.Male | (int)NameCatagoryCode.Human | (int)NameCatagoryCode.HumanFamilyName,

    }
    public struct EntityProfile {
        public int Level;

        public double Difficulty;

        public EntityOccupation Occupation;

        public EntityRace Race;

        public NameCatagoryCode NameCode;

        public EntityProfile(int level, NameCatagoryCode nameCode, EntityOccupation occupation, EntityRace race, float difficulty) {
            Level = level;
            Race = race;
            NameCode = nameCode;
            Occupation = occupation;
            Difficulty = difficulty;
        }
    }
}
