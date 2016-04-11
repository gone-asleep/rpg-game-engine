using GameEngine;
using GameEngine.Entities;
using GameEngine.Entities.Stats;
using GameEngine.Factories;
using GameEngine.Global;
using GameEntities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEntities
{
    // currently when you load this dll, you can call EntityCollection.Load( desired time ) to allow 
    // this should probably be reworked, this is a strange pattern... 

    public static class EntityCollection
    {
        private static Dictionary<EntityType, Func<EntityProfile, Entity>> internalCollection;

        static EntityCollection() {
            internalCollection = new Dictionary<EntityType, Func<EntityProfile, Entity>>();   
            internalCollection.Add(EntityType.Human, Human.HumanConstructor);
        }

        public static void LoadAll() {
            foreach (var entity in internalCollection) {
                GlobalLookup.Factories.Entities.Add(entity.Key, entity.Value);
            }
        }

        public static void Load(EntityType code) {
            GlobalLookup.Factories.Entities.Add(code, internalCollection[code]);
        }
    }
}
