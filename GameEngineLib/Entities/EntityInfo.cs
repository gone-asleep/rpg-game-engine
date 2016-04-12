using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Entities {
    public interface IEntityInfo {
        /// <summary>
        /// The Race of this entity, all entities should have a race
        /// </summary>
        EntityRace Race { get;  }

        /// <summary>
        /// The occupation of this entity, this helps to define how the entity interacts with the player
        /// </summary>
        EntityOccupation Occupation { get;  }

        /// <summary>
        /// the individuals name of this entity
        /// </summary>
        string Name { get; }
    }

    public class EntityInfo : IEntityInfo {
        public EntityRace Race { get; private set; }

        public EntityOccupation Occupation { get; private set; }

        public string Name { get; private set; }

        public EntityInfo(EntityRace race, EntityOccupation occupation, string name = null) {
            if (Name == null) {
                this.Name = Race.ToString() + " " + Occupation.ToString();
            }
            this.Name = name;
            this.Race = race;
            this.Occupation = occupation;
        }
    }
}
