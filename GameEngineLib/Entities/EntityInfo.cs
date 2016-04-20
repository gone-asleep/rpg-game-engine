using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Entities {
    [ProtoContract]
    [ProtoInclude(100, typeof(EntityInfo))]
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

    [ProtoContract]
    public class EntityInfo : IEntityInfo {
        
        [ProtoMember(1)]
        public EntityRace Race { get; private set; }
        
        [ProtoMember(2)]
        public EntityOccupation Occupation { get; private set; }
        
        [ProtoMember(3)]
        public string Name { get; private set; }

        public EntityInfo() {
        }

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
