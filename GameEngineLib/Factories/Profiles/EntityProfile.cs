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

    public struct EntityProfile {
        public int Level;

        public double Difficulty;

        public EntityOccupation Occupation;

        public EntityRace Race;

        public EntityProfile(int level, EntityOccupation occupation, EntityRace race, float difficulty) {
            Level = level;
            Race = race;
            Occupation = occupation;
            Difficulty = difficulty;
        }
    }
}
