using System;

namespace GameData {

    //http://www-cs-students.stanford.edu/~amitp/game-programming/polygon-map-generation/#source

    public enum AreaElevationCode : int {
        KM9, // Maximum Height
        KM8,
        KM7,
        KM6, //<0.1%
        KM5, //0.5%
        KM4, //1.1% // high mountain
        KM3, //2.2%
        
        KM2, //4.5% 
        KM1, //20.9% // average land heigth (
        KM_1, //8.5%
        KM_2, //3.0%
        KM_3, //4.8%
        KM_4, //13.9% // average sea depth
        KM_5, //23.2%
        KM_6, //16.4%
        KM_7, //1%
        KM_8, // <0.1%
        KM_9,
        KM_10,
        KM_11, // Maximum Depth
    }

    public enum AreaClimateCode : int {
        Arctic,
        Boreal,
        Temperate,
        Subtropical,
        Tropical,
    }

    //source for some of the items
    //http://www.cartographersguild.com/showthread.php?t=22151

    // awesome city creation guide
    //http://www222.pair.com/sjohn/blueroom/demog.htm

    public enum AreaCode : int {
        Street,
        Grassland,
        GrassyHills,
        Rock,
        Hills,
        Mountains,
        MountainPeak,
        HighMountains,
        HighMountainPeak,
        Swamp,
        Beach,
        Desert,
        RockyDesert,
        LightForest,
        HeavyForest,
        ForestedHills,
        ForestedMountains,
        Plains,
        Cliff,
        Volcano,
        Canyon,
        Lake,
        River,
        Stream,
        PavedRoad,
        UnpavedRoad,
        Bridge,
        Ford, //type of bridge

        ShallowWater,
        MidDeepWater,
        DeepWater,

        Town,
        LargeTown,
        City,
        Kingdom,
        Quarry,
        LumberjackCamp,
        HunterCamp,
        Mine,
        Farm,
        SmeltingFurnace,
        Plantation,

        Housing,
        PeasantHousing,
        CommonerHousing,
        MerchantHousing,
        BureaucratHousing,
        PoliticalHousing,
        NobleHousing,
        Barracks,

        Temple,
        Shrine,
        Church,
        Sanctuary,

        Market,

        BlacksmithShop,
        CarpenterShop,
        PotterShop,
        GlassblowerShop,
        TailorShop,
        LeatherworkerShop,
        ArmorsmithShop,
        WeaponsmithShop,
        AlchemistShop,
        Stables,
        MagewareShop,
        Spices,
        Cloth,
        Slaves,
        RareMaterialsShop,
        ExoticMaterialsShop, 
        MapVendor,
        BookSellerShop,
        Brothel,
        Inn,

        Tavern,
        Pub,
        Gladiator,

        /// <summary>
        /// Area of buisiness for shops
        /// </summary>
        ServiceRoom,

        School,
        Library,
    }
}
