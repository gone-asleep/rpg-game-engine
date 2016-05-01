using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Items {
    [Flags]
    public enum ItemWieldType : int {
        OneHand = 0,
        LeftHand = 1,
        RightHand = 2,
        BothHands = 3,
    }

    [Flags]
    public enum ItemEquipType : int {
        Head = 0,
        Neck = 1,
        Chest = 2,
        Legs = 3,
        Feet = 4,
        Arms = 5,
        Shoulders = 6,
        Back = 7,
        Hands = 8
    }
}
