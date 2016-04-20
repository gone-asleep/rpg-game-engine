using GameEngine.Items;
using GameEngine.Worlds;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine {
    public interface IWorld {
        Dictionary<Guid, IEntity> Entities { get; }
        Dictionary<Guid, IItem> Items { get; }
        Map Map { get; }

        void RegisterEntity(IEntity entity);
        void RegisterItem(IItem item);
    }

    public class World : IWorld {
        public Dictionary<Guid, IEntity> Entities { get; private set; }
        public Dictionary<Guid, IItem> Items { get; private set; }
        
        public Map Map { get; private set; }
        
        public World() {
            this.Items = new Dictionary<Guid, IItem>();
            this.Entities = new Dictionary<Guid, IEntity>();
        }

        public void RegisterEntity(IEntity entity) {
            this.Entities.Add(entity.ID, entity);
        }
        public void UnregisterEntity(Guid id) {
            this.Entities.Remove(id);
        }

        public void RegisterItem(IItem item) {
            this.Items.Add(item.ID, item);
        }

        public IItem GetItem(Guid guid) {
            IItem item;
            if (Items.TryGetValue(guid, out item)) {
                Items.Remove(guid);
            }
            return item;
        }

        public void SetMap(Map map) {
            this.Map = map;
        }
    }
}
