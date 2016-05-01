using GameEngine.Factories;
using GameEngine.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEntities {



    // names are currently from source
    // http://users.pgtc.com/~slmiller/fantasynames.htm
    // family names generated from http://www.mithrilandmages.com/utilities/MedievalNames.php
    //public class NameProvider {
    //    public IDictionary<NameCatagoryCode, string[]> Names = new Dictionary<NameCatagoryCode, string[]>() {
    //        { NameCatagoryCode.HumanFamilyName, new string[] {
    //            "Athanassiadi","Accardo","Araoz","Albelin","Ambrogi",
    //            "Baertschi","Boje","Bolla","Benson","Balzar",
    //            "Carnini","Cleyn","Cornelis","Chevillon","Crisalli",
    //            "Duran","Dealavallade","Dichio","Deyaert","Durlacher",
    //            "Eigner","Escz","Etxabeguren","Engemann","Elizalde",
    //            "Fissler","Ferri","Farentino","Fliehman","Freindametz",
    //            "Grob","Gilca","Guintcehv","Guterres","Gottlieb",
    //            "Hawlik","Herzog","Haltzel","Hendrickson","Hinetze",
    //            "Irastorza","Ivanov","Ilica","Iagar","Ibarran",
    //            "Jansen","Jopie","Jorise","Jadin","Joosten",
    //            //k
    //            //l
    //            //m
    //            //n
    //            //o
    //            //...
    //          }
    //        },
    //        { NameCatagoryCode.HumanFemale, new string[] {
    //            "Alyvia","Agate","Arabeth","Ardra",
    //            "Brenna",
    //            "Caryne",
    //            "Dasi","Derris","Dynie",
    //            "Eryke","Errine",
    //            "Farale",
    //            "Gavina","Glynna",
    //            "Karran","Kierst","Kira","Kyale",
    //            "Ladia",
    //            "Mora","Moriana",
    //            "Quiss",
    //            "Sadi","Salina","Samia","Sephya","Shaundra","Siveth",
    //            "Thana",
    //            "Valiah",
    //            "Zelda"} },
    //        { NameCatagoryCode.HumanMale, new string[] {
    //            "Alaric","Alaron","Alynd","Asgoth",
    //            "Berryn",
    //            "Derrib",
    //            "Eryk","Evo",
    //            "Fausto",
    //            "Gavin","Gorth",
    //            "Jarak","Jasek",
    //            "Kurn",
    //            "Lan","Ledo","Lor",
    //            "Mavel","Milandro",
    //            "Sandar","Sharn",
    //            "Tarran","Thane","Topaz","Tor","Torc","Travys","Trebor","Tylien",
    //            "Vicart",
    //            "Zircon"
    //        }}
    //    };

    //    public NameProvider(IDictionary<NameCatagoryCode, string[]> names=null) {
    //        if (names != null) {
    //            this.Names = names;
    //        }
    //    }

    //    public string Create(NameCatagoryCode code) {
    //        string result;
    //        int nameIndex = GameGlobal.RandomInt(0, Names[code].Length);
    //        result = Names[code][nameIndex];
    //        if (code.HasFlag(NameCatagoryCode.FamilyName)) {
    //            if (code.HasFlag(NameCatagoryCode.Human)) {
    //                nameIndex = GameGlobal.RandomInt(0, Names[NameCatagoryCode.HumanFamilyName].Length);
    //                result += " " + Names[NameCatagoryCode.HumanFamilyName][nameIndex];
    //            }
    //        }
    //        return result;
    //    }
    //}
}
