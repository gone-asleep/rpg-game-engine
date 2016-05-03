using GameEngine.Global;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Currency {
    [ProtoContract]
    [ProtoInclude(100, typeof(EntityCurrencyBag))]
    public interface IEntityCurrencyBag {
        /// <summary>
        /// Total value contained in bag
        /// </summary>
        long TotalValue { get; }

        /// <summary>
        /// Number of Gold Coins
        /// </summary>
        int Gold { get; }

        /// <summary>
        /// Number of Silver Coins
        /// </summary>
        int Silver { get; }

        /// <summary>
        /// Number of Copper Coins
        /// </summary>
        int Copper { get; }

        void Put(IEntityCurrencyBag currency);

        void Empty();

        IEntityCurrencyBag Take(int gold, int silver, int copper);

        IEntityCurrencyBag Take(int totalValue);
    }

    [ProtoContract]
    public class EntityCurrencyBag : IEntityCurrencyBag {
        public long TotalValue {
            get { 
                return this.Gold * GameGlobal.GoldAbsoluteValue + 
                    this.Silver * GameGlobal.SilverAbsoluteValue + 
                    this.Copper * GameGlobal.CopperAbsoluteValue; 
            }
        }

        [ProtoMember(1)]
        public int Gold { get; private set; }

        [ProtoMember(2)]
        public int Silver { get; private set; }

        [ProtoMember(3)]
        public int Copper { get; private set; }

        public EntityCurrencyBag() { }

        public EntityCurrencyBag(int gold, int silver, int copper) {
            this.Gold = gold;
            this.Silver = silver;
            this.Copper = copper;
        }

        public void Empty() {
            this.Gold = 0;
            this.Copper = 0;
            this.Silver = 0;
        }

        public void Put(IEntityCurrencyBag currency) {
            this.Gold += currency.Gold;
            this.Silver += currency.Silver;
            this.Copper += currency.Copper;
            currency.Empty();
        }

        public IEntityCurrencyBag Take(int gold, int silver, int copper) {
            if (this.Gold >= gold && this.Silver >= silver && this.Copper >= copper) {
                this.Gold -= gold;
                this.Silver -= silver;
                this.Copper -= copper;
                return new EntityCurrencyBag(gold, silver, copper);
            }
            return null;
        }

        public IEntityCurrencyBag Take(int totalValue) {
            if (this.TotalValue >= totalValue) {
                int gold = Math.Min(this.Gold, totalValue / GameGlobal.GoldAbsoluteValue);
                totalValue -= gold * GameGlobal.GoldAbsoluteValue;
                int silver = Math.Min(this.Silver, totalValue / GameGlobal.SilverAbsoluteValue);
                totalValue -= silver * GameGlobal.SilverAbsoluteValue;
                int copper = Math.Min(this.Copper, totalValue / GameGlobal.CopperAbsoluteValue);
                totalValue -= copper * GameGlobal.CopperAbsoluteValue;
                return this.Take(gold, silver, copper);
            }
            return null;
        }
    }
}
