using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine {
    public class EntityFactory {
        Dictionary<EntityTypeCode, Func<EntityFactoryTypeProfile, Entity>> Factories { get; set; }

         public EntityFactory() {
             this.Factories = new Dictionary<EntityTypeCode, Func<EntityFactoryTypeProfile, Entity>>();
        }

         public void AddFactoryConstructor(EntityTypeCode code, Func<EntityFactoryTypeProfile, Entity> func) {
            this.Factories.Add(code, func);
        }

         public Entity Generate(EntityTypeCode typeCode, EntityFactoryTypeProfile profile = new EntityFactoryTypeProfile()) {
            if (this.Factories.ContainsKey(typeCode)) {
                var entity = Factories[typeCode](profile);
                entity.Refresh();
                return entity;
            }
            return null;
        }
    }

    public struct EntityFactoryTypeProfile {
        public int Level;

        public EntityFactoryTypeProfile(int level) {
            Level = level;
        }
    }
}
