using GameData;
using GameEngine.Entities;
using GameEngine.Items;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEntities {
    // generate a table to determine odds of creation based on entity occupation
    // generate a table to determine odds of creation based on room
    // this matrix could be generated via a neural network agorithm
    [ProtoContract]
    public class codeProbability {
        [ProtoMember(1)]
        public float probability;
        [ProtoMember(2)]
        public int type;
        public codeProbability(ItemType type, float probability) {
            this.probability = probability;
            this.type = (int)type;
        }
        public codeProbability(ItemQualityCode type, float probability) {
            this.probability = probability;
            this.type = (int)type;
        }
        public codeProbability(EntityOccupation type, float probability) {
            this.probability = probability;
            this.type = (int)type;
        }
        
    };
}
