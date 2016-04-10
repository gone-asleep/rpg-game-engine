using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Items {
    public enum ItemEquipType : int {
        None = 0,
        Head = 1,
        Neck = 2,
        Chest = 4,
        Legs = 8,
        Feet = 16,
        Arms = 32,
        Shoulders = 64,
        Back = 128,
        LeftHand = 256,
        RightHand = 512
    }
}
