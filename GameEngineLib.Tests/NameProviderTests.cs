using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameEntities;
using GameEngine.Factories;

namespace GameEngineLib.Tests {
    [TestClass]
    public class NameProviderTests {
        [TestMethod]
        public void GenerateNameWithFamilyName() {
            NameProvider provider = new NameProvider();
            string name = provider.Create(NameCatagoryCode.HumanFemale);
            Assert.IsNotNull(name);
            Assert.IsTrue(name.Contains(" "));
        }
    }
}
