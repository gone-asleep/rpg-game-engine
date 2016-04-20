using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Factories {
    public interface IFactoryProducer<Type, TypeCreationProfile> {
        Type Create(TypeCreationProfile profile);
    }
}
