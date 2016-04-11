using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Factories {

    public enum EntityOccupation {
        Warrior,
        Theif,
        RandomOccupation
    }

    public struct EntityProfile {
        public int Level;

        public double Difficulty;

        public EntityOccupation Occupation;

        public EntityProfile(int level, EntityOccupation occupation, float difficulty) {
            Level = level;
            Occupation = occupation;
            Difficulty = difficulty;
        }
    }
}
