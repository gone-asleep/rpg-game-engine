using GameData;
using GameEngine.Areas;
using GameEngine.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weighted_Randomizer;

namespace GameEntities.AreaFactory {
    public class AreaFactory : IFactoryProducer<IArea, AreaProfile> {

        private IDictionary<AreaCode, IWeightedRandomizer<AreaCode>> areaProbabilityProfile = new Dictionary<AreaCode, IWeightedRandomizer<AreaCode>>();

        private void buildAreaProbabilityProfile() {
            areaProbabilityProfile[AreaCode.City] = new StaticWeightedRandomizer<AreaCode>() {
                { AreaCode.MerchantHousing , 1},
                { AreaCode.Market , 5},
                { AreaCode.Farm, 10},
                { AreaCode.CommonerHousing, 5},
                { AreaCode.NobleHousing, 2 },

            };
            areaProbabilityProfile[AreaCode.Market] = new StaticWeightedRandomizer<AreaCode>() {
                { AreaCode.AlchemistShop, 1},
                { AreaCode.ArmorsmithShop , 5},
                { AreaCode.BlacksmithShop, 10},
                { AreaCode.BookSellerShop, 5},
                { AreaCode.CarpenterShop, 2 },
                { AreaCode.ExoticMaterialsShop, 2 },
                { AreaCode.GlassblowerShop, 2 },
                { AreaCode.LeatherworkerShop, 2 },
                { AreaCode.MagewareShop, 2 },
                { AreaCode.MapVendor, 2 },
                { AreaCode.PotterShop, 2 },
                { AreaCode.RareMaterialsShop, 2 },
                { AreaCode.TailorShop, 2 },
                { AreaCode.WeaponsmithShop, 2 },
                { AreaCode.Tavern, 2 }
            };
        }

        public IArea Create(AreaProfile profile) {
            if (profile.AreaCode == AreaCode.City) {

            }
            return null;
        }
    }
}
