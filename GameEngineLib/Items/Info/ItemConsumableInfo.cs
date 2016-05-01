using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Items.Info {
    [ProtoContract]
    [ProtoInclude(99, typeof(ItemInfo))]
    [ProtoInclude(100, typeof(ItemConsumableInfo))]
    public interface IItemConsumableInfo : IItemInfo {
    }

    [ProtoContract]
    public class ItemConsumableInfo : ItemInfo, IItemConsumableInfo {

        public ItemConsumableInfo() {

        }

        public ItemConsumableInfo(ItemClassCode classCode, ItemType type, string name = null, string description = null) :
            base(classCode, type, name, description) {
        }
    }
}
