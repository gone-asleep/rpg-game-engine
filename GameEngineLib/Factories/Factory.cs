using GameEngine.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Factories {
    public class Factory<TypeCode, Type, TypeCreationProfile> {
        private Dictionary<TypeCode, IFactoryProducer<Type, TypeCreationProfile>> Factories { get; set; }

         public Factory() {
             this.Factories = new Dictionary<TypeCode, IFactoryProducer<Type, TypeCreationProfile>>();
        }

         public void Add(TypeCode code, IFactoryProducer<Type, TypeCreationProfile> producer) {
             this.Factories.Add(code, producer);
        }

         public Type Generate(TypeCode typeCode, TypeCreationProfile profile = default(TypeCreationProfile)) {
            if (this.Factories.ContainsKey(typeCode)) {
                var entity = Factories[typeCode].Create(profile);
                return entity;
            }
            return default(Type);
        }

    }
}
