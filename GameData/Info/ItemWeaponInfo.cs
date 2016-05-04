using ProtoBuf;

namespace GameData.Info {
    [ProtoContract]
    [ProtoInclude(99, typeof(ItemInfo))]
    [ProtoInclude(101, typeof(ItemWeaponInfo))]
    public interface IItemWeaponInfo : IItemInfo, IWieldableItemInfo, ISkillfullItemInfo, IMarketableItemInfo {
        float BaseDamage { get; }
    }

    [ProtoContract]
    public class ItemWeaponInfo : ItemInfo, IItemWeaponInfo {

        [ProtoMember(5)]
        public float BaseDamage { get; private set; }

        [ProtoMember(6)]
        public float SkilledUseConstant { get; private set; }
        
        [ProtoMember(7)]
        public float BaseSpeed { get; private set; }

        [ProtoMember(8)]
        public ItemWieldType WieldType { get; protected set; }

        [ProtoMember(9)]
        public SkillType ApplyableSkill { get; protected set; }
       
        [ProtoMember(10)]
        public WeaponStatCalculationFunction Function { get; protected set; }

        [ProtoMember(11)]
        public float MarketValue { get; protected set; }

        public ItemWeaponInfo() {

        }

        public ItemWeaponInfo(ItemType type, ItemWieldType wieldType, SkillType applyableSkill, float marketValue, float baselineDamage, string name = null, string description = null) :
            base(ItemClassCode.Weapon, type, name, description) {
            this.BaseDamage = baselineDamage;
            this.WieldType = wieldType;
            this.ApplyableSkill = applyableSkill;
        }
    }
}
