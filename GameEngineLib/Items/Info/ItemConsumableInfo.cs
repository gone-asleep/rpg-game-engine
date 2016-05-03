﻿using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Items.Info {
    [ProtoContract]
    [ProtoInclude(99, typeof(ItemInfo))]
    [ProtoInclude(100, typeof(ItemConsumableInfo))]
    public interface IItemConsumableInfo : IItemInfo, IMarketableItemInfo {
    }

    [ProtoContract]
    public class ItemConsumableInfo : ItemInfo, IItemConsumableInfo {

        [ProtoMember(6)]
        public float MarketValue { get; protected set; }

        public ItemConsumableInfo() {

        }

        public ItemConsumableInfo(ItemClassCode classCode, ItemType type, float marketValue, string name = null, string description = null) :
            base(classCode, type, name, description) {
            this.MarketValue = marketValue;
        }
    }
}
