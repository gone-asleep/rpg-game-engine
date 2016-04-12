using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Effects {

    public interface IEffectInfo {
        EffectClass ClassCode { get; }
        EffectType TypeCode { get; }
        string Name { get; }
    }

    public class EffectInfo : IEffectInfo {
        public EffectType TypeCode { get; private set; }
        public EffectClass ClassCode { get; private set; }
        public string Name { get; private set; }

        public EffectInfo(EffectType typeCode, EffectClass classCode, string name = null) {
            this.TypeCode = typeCode;
            this.ClassCode = classCode;
            this.Name = name;
        }
    }
}
