using GameEngine.Global;
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
        /// The day entity was born relative to world begining
        /// </summary>
        int DayOfBirth { get; }

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
        public string FirstName { get; private set; }
        
        [ProtoMember(4)]
        public string LastName { get; private set; }

        [ProtoMember(5)]
        public string Name { get; private set; }

        /// <summary>
        /// The day entity was born relative to world begining
        /// </summary>
        [ProtoMember(6)]
        public int DayOfBirth { get; private set; }
       
        [ProtoMember(6)]
        public int YearsOld {
            get {
                return (GameGlobal.WorldDayCurrent - DayOfBirth) / 365;
            }
        }

        public EntityInfo() {
        }

        public EntityInfo(EntityRace race, EntityOccupation occupation, int daysOld, string firstName, string lastName=null) {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Race = race;
            this.Occupation = occupation;
            this.DayOfBirth = (GameGlobal.WorldDayCurrent - daysOld);
            // this generates the full name of the entity as it will be referenced in most places within the game
            if (!string.IsNullOrEmpty(LastName)) {
                if (Occupation != EntityOccupation.None) {
                    Name = FirstName + " " + LastName + " the " + Occupation.ToString();
                } else {
                    Name = FirstName + " " + LastName;
                }
            } else {
                if (Occupation != EntityOccupation.None) {
                    Name = FirstName + " the " + Occupation.ToString();
                } else {
                    Name = FirstName;
                }
            }
        }
    }
}
