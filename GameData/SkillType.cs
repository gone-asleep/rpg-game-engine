using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameData {
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

        UniversalMagic = 14,
        /// <summary>
        /// spells of protection, blocking, and banishing. Specialists are called abjurers.
        /// </summary>
        Abjuration = 15,
        /// <summary>
        /// spells that bring creatures or materials. Specialists are called conjurers.
        /// </summary>
        Conjuration = 16,
        /// <summary>
        /// spells that reveal information. Specialists are called diviners.
        /// </summary>
        Divination = 17,
        /// <summary>
        ///  spells that magically imbue the target or give the caster power over the target. Specialists are called enchanters.
        /// </summary>
        Enchantment = 18,
        /// <summary>
        /// spells that manipulate energy or create something from nothing. Specialists are called evokers.
        /// </summary>
        Evocation = 19,
        /// <summary>
        /// spells that alter perception or create false images. Specialists are called illusionists.
        /// </summary>
        Illusion = 20,
        /// <summary>
        /// spells that manipulate life or life force. Specialists are called necromancers.
        /// </summary>
        Necromancy = 21,
        /// <summary>
        /// spells that transform the target. Specialists are called transmuters.
        /// </summary>
        Transmutation = 22,
        /// <summary>
        /// Proficiency in using a Fishing Pole
        /// </summary>
        Fishing = 23
    }
}
