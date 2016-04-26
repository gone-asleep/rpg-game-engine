using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Entities {
    [ProtoContract]
    public class ActionParameters {
        [ProtoMember(1)]
        public Guid entityID { get; set; }
        [ProtoMember(2)]
        public Guid itemID { get; set; }
        [ProtoMember(3)]
        public int targetIndex { get; set; }
        [ProtoMember(4)]
        public GeneralAbilities action { get; set; }
        [ProtoMember(5)]
        public float targetQuantity { get; set; }
    }
}
