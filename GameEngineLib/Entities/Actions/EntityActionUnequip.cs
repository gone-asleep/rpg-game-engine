using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Actions {
    public class EntityActionUnequip : EntityActionBase {
         Item equipedItem;

         public EntityActionUnequip(Entity entity,  Item item) {
            this.equipedItem = item;
            this.StartTime = entity.FinalActionTime;
            this.EndTime = entity.FinalActionTime + 1;
        }

         public override void Update(Entity entity) {
            if (this.IsCurrent()) {
                if (entity.Inventory.Contains(equipedItem)) {
                    if (entity.Equiped.ContainsKey(equipedItem.EquipType) && entity.Equiped[equipedItem.EquipType] == equipedItem) {
                        entity.Stats.RemoveModifier(equipedItem.Modifier);
                        if (equipedItem.EnchantmentModifier != null) {
                            entity.Stats.RemoveModifier(equipedItem.EnchantmentModifier);
                        }
                        entity.Equiped.Remove(equipedItem.EquipType);
                    }
                }
                this.IsFinished = true;
                Debug.WriteLine("Unequiped Item {0}", equipedItem.Name);
            }
        }
    }
}
