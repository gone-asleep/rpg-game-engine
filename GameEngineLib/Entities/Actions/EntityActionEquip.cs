using GameEngine.Items;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Actions {
    public class EntityActionEquip : EntityActionBase{
        
        Item equipedItem;

        public EntityActionEquip(Entity entity, Item item) {
            this.equipedItem = item;
            this.StartTime = entity.FinalActionTime;
            this.EndTime = entity.FinalActionTime + 1;
        }
        public override void Update(Entity entity) {
            if (this.IsCurrent()) {
                if (entity.Inventory.Contains(equipedItem)) {
                    if (equipedItem.EquipType != ItemEquipType.None) {
                        // unequip existing if available
                        if (!entity.Equiped.ContainsKey(equipedItem.EquipType)) {
                            entity.Equiped[equipedItem.EquipType] = equipedItem;
                            entity.Stats.AddModifier(equipedItem.Modifier);
                            if (equipedItem.EnchantmentModifier != null) {
                                entity.Stats.AddModifier(equipedItem.EnchantmentModifier);
                            }
                        }
                    }
                }
                this.IsFinished = true;
                Debug.WriteLine("Equiped Item {0}", equipedItem.Name);
            }
        }
    }
}
