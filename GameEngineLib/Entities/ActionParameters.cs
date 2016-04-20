using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Entities {
    public class ActionParameters {
        public Guid entityID { get; set; }
        public Guid itemID { get; set; }

        public int targetIndex { get; set; }

        public GeneralAbilities action { get; set; }

        public float targetQuantity { get; set; }

        public System.Numerics.Vector2 position { get; set; }
    }
}
