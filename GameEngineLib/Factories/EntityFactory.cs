using GameEngine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Factories {
    public class EntityFactory {
        private Dictionary<EntityTypeCode, Func<EntityFactoryTypeProfile, Entity>> Factories { get; set; }

         public EntityFactory() {
             this.Factories = new Dictionary<EntityTypeCode, Func<EntityFactoryTypeProfile, Entity>>();
        }

         public void Add(EntityTypeCode code, Func<EntityFactoryTypeProfile, Entity> func) {
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
}
