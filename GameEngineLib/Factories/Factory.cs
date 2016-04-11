using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Factories {
    public class Factory<TypeCode, Type, TypeCreationProfile> {
        private Dictionary<TypeCode, Func<TypeCreationProfile, Type>> Factories { get; set; }

         public Factory() {
             this.Factories = new Dictionary<TypeCode, Func<TypeCreationProfile, Type>>();
        }

         public void Add(TypeCode code, Func<TypeCreationProfile, Type> func) {
            this.Factories.Add(code, func);
        }

         public Type Generate(TypeCode typeCode, TypeCreationProfile profile = default(TypeCreationProfile)) {
            if (this.Factories.ContainsKey(typeCode)) {
                var entity = Factories[typeCode](profile);
                return entity;
            }
            return default(Type);
        }

    }
}
