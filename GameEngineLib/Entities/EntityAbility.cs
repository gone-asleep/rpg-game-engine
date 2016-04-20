using GameEngine.AI;
using GameEngine.Effects;
using GameEngine.Global;
using GameEngine.Items;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Entities {
    /// <summary>
    /// ActionTypeGeneral
    /// Essentially Entity Permissions this object holds the most basic types
    /// of opperations an entity can do
    /// </summary>

    [ProtoContract]
    [ProtoInclude(100, typeof(EntityAbility))]
    public interface IEntityAbility {
        void Add(AIAbilities value);
        void Add(GeneralAbilities value);
        void Add(EntityAbilities value);
        void Add(ItemAbilities value);
        void Add(EffectAbilities value);
        bool Check(AIAbilities value);
        bool Check(GeneralAbilities value);
        bool Check(EntityAbilities value);
        bool Check(ItemAbilities value);
        bool Check(EffectAbilities value);
        void Remove(AIAbilities value);
        void Remove(GeneralAbilities value);
        void Remove(EntityAbilities value);
        void Remove(ItemAbilities value);
        void Remove(EffectAbilities value);

        float GetNextAvailableActionTime(IGlobal global);
        void SetNextAvailableActionTime(IGlobal global, float value);

    }

    [ProtoContract]
    public class EntityAbility : IEntityAbility {
        // currently used for queued actions
        [ProtoMember(1)]
        public float FinalActionTime { get; set; }

        //rework this
        public float GetNextAvailableActionTime(IGlobal global) {
            return (FinalActionTime > global.Time.Current) ? global.Time.Current : FinalActionTime;
        }
        public void SetNextAvailableActionTime(IGlobal global, float value) {
            this.FinalActionTime = value;
        }

        // this class may eventually contain a stack of actions
        // it may keep state information on what the entity is currently doing
        // information for the ai can be kept here
        // this could be a quick validation lookup
        [ProtoMember(2)]
        private GeneralAbilities _general;
        
        [ProtoMember(3)]
        private ItemAbilities _items;
        
        [ProtoMember(4)]
        private EntityAbilities _interactions;
        
        [ProtoMember(5)]
        private EffectAbilities _magic;
        
        [ProtoMember(6)]
        private AIAbilities _ai;

        public bool Check(GeneralAbilities value) {
            return (_general & value) > 0;
        }
        public bool Check(ItemAbilities value) {
            return (_items & value) > 0;
        }
        public bool Check(EntityAbilities value) {
            return (_interactions & value) > 0;
        }
        public bool Check(EffectAbilities value) {
            return (_magic & value) > 0;
        }
        public bool Check(AIAbilities value) {
            return (_ai & value) > 0;
        }

        public void Add(GeneralAbilities value) {
            _general |= value;
        }
        public void Add(ItemAbilities value) {
            _items |= value;
        }
        public void Add(EntityAbilities value) {
            _interactions |= value;
        }
        public void Add(EffectAbilities value) {
            _magic |= value;
        }
        public void Add(AIAbilities value) {
            _ai |= value;
        }

        public void Remove(GeneralAbilities value) {
            _general &= ~value;
        }
        public void Remove(ItemAbilities value) {
            _items &= ~value;
        }
        public void Remove(EntityAbilities value) {
            _interactions &= ~value;
        }
        public void Remove(EffectAbilities value) {
            _magic &= ~value;
        }
        public void Remove(AIAbilities value) {
            _ai &= ~value;
        }

        public EntityAbility(EntityAbility source) {
            this._general = source._general;
            this._items = source._items;
            this._interactions = source._interactions;
            this._magic = source._magic;
            this._ai = source._ai;
        }

        public EntityAbility() {

        }

        public EntityAbility(GeneralAbilities general, ItemAbilities items, EntityAbilities interactions, EffectAbilities magic, AIAbilities ai) {
            _general = general;
            _items = items;
            _interactions = interactions;
            _magic = magic;
            _ai = ai;
        }

        public override string ToString() {
            return _general.ToString() + _items.ToString() + _interactions.ToString() + _magic.ToString() + _ai.ToString();
        }

        public override bool Equals(object obj) {
            EntityAbility other = obj as EntityAbility;
            if (other != null) {
                return this._ai == other._ai &&
                        this._general == other._general &&
                        this._interactions == other._interactions &&
                        this._items == other._items &&
                        this._magic == other._magic;
            }
            return false;
        }
    }

    [Flags]
    public enum GeneralAbilities : int {
        ModifyGeneral = 1,
        Move = 2,
        Use = 4,
        Give = 8,
        Equip = 16,
        Unequip = 32,
        Receive = 64,
        AddEffect = 128,
        RemoveEffect = 256,
        DistributeSkillPoints = 512,
        Destroy = 1024,
        TakeDamage = 2048,
        GiveDamage = 4096,
        All = Int32.MaxValue,
        None = 0
    }

    

    [Flags]
    public enum EntityAbilities : long {
        ModifyInterationAbilities = 1,
        All = Int64.MaxValue,
        None = 0
    }


}
