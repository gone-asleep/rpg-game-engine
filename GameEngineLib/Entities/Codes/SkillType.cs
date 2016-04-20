using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Entities.Skills {
    public enum SkillType : int {
        LightArmor = 0,
        HeavyArmor = 1,
        LightBlade = 2,
        HeavyBlade = 3,
        BluntWeapon = 4,
        Sneak = 5,
        Pickpocket = 6,
        LockPicking = 7,
        RangeWeapon = 8,
        PolearmWeapon = 9,
        AxeWeapon = 10,
        /// <summary>
        ///  Weapons in the flail group have a flexible material, usually a length of chain, between a solid handle and the damage-dealing end of the weapon.
        /// </summary>
        FlailWeapon = 11,
        StaffWeapon = 12,
        HammerWeapon = 13,
    }
}
