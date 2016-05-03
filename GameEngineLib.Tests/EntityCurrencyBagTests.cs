using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameEngine.Currency;

namespace GameEngineLib.Tests {
    [TestClass]
    public class EntityCurrencyBagTests {
        [TestMethod]
        public void CreateEntityCurrencyBag() {
            IEntityCurrencyBag currencyBag = new EntityCurrencyBag(1, 2, 3);
            Assert.AreEqual(1, currencyBag.Gold);
            Assert.AreEqual(2, currencyBag.Silver);
            Assert.AreEqual(3, currencyBag.Copper);
        }

        [TestMethod]
        public void EntityCurrencyBagTake() {
            IEntityCurrencyBag currencyBag = new EntityCurrencyBag(1, 2, 3);

        }

    }
}
