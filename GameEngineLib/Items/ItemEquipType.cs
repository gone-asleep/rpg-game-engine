using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Items {
    [Flags]
    public enum ItemWieldType : int {
        OneHand = 0,
        BothHands = 3,
    }

    //bit of javascript to generate all Inventory enum spots
    //var f = function(count) { for (var i = 0; i < count; i++) { console.log('Inventory'+(i+1) + ' = ' + (i+12)); } };

    public enum InventorySlot : int {
        WieldPrimary = 0,
        WieldSecondary = 1,
        WieldTwoHanded = 2,
        Head = 3,
        Neck = 4,
        Chest = 5,
        Legs = 6,
        Feet = 7,
        Arms = 8,
        Shoulders = 9,
        Back = 10,
        Hands = 11,
        Inventory1 = 12,
        Inventory2 = 13,
        Inventory3 = 14,
        Inventory4 = 15,
        Inventory5 = 16,
        Inventory6 = 17,
        Inventory7 = 18,
        Inventory8 = 19,
        Inventory9 = 20,
        Inventory10 = 21,
        Inventory11 = 22,
        Inventory12 = 23,
        Inventory13 = 24,
        Inventory14 = 25,
        Inventory15 = 26,
        Inventory16 = 27,
        Inventory17 = 28,
        Inventory18 = 29,
        Inventory19 = 30,
        Inventory20 = 31,
        Inventory21 = 32,
        Inventory22 = 33,
        Inventory23 = 34,
        Inventory24 = 35,
        Inventory25 = 36,
        Inventory26 = 37,
        Inventory27 = 38,
        Inventory28 = 39,
        Inventory29 = 40,
        Inventory30 = 41,
        Inventory31 = 42,
        Inventory32 = 43,
        Inventory33 = 44,
        Inventory34 = 45,
        Inventory35 = 46,
        Inventory36 = 47,
        Inventory37 = 48,
        Inventory38 = 49,
        Inventory39 = 50,
        Inventory40 = 51,
        Inventory41 = 52,
        Inventory42 = 53,
        Inventory43 = 54,
        Inventory44 = 55,
        Inventory45 = 56,
        Inventory46 = 57,
        Inventory47 = 58,
        Inventory48 = 59,
        Inventory49 = 60,
        Inventory50 = 61,
        Inventory51 = 62,
        Inventory52 = 63,
        Inventory53 = 64,
        Inventory54 = 65,
        Inventory55 = 66,
        Inventory56 = 67,
        Inventory57 = 68,
        Inventory58 = 69,
        Inventory59 = 70,
        Inventory60 = 71,
        Any = 72,
        AnyNonEquiped = 73,
        Money = 74
    }
}
