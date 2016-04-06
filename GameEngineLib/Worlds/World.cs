using GameEngine.Worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine {
    public class World {
        public List<Entity> Entities { get; set; }
        public Map Map { get; private set; }
        public World() {
            this.Entities = new List<Entity>();
        }
        public void SetMap(Map map) {
            this.Map = map;
        }
        public void Refresh() {
            foreach (var entity in Entities) {
                entity.Refresh();
            }
        }
        public void Draw() {
            this.Map.Draw();
            foreach (var entity in Entities) {
                entity.Draw();
            }
        }
    }
}
