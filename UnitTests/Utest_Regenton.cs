using LiquidContainerLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    [TestClass]
    public class Utest_Regenton
    {
        [TestMethod]
        public void RegentonDefaultValues()
        {
            //init test
            var type_inhoud = Regenton.Size.Small;
            uint verwachte_maxinhoud = 80;
            uint verwachte_inhoud = 0;
            //do test
            var b = new Regenton();

            //evaluate test
            Assert.IsTrue(b.Capacity == verwachte_maxinhoud);
            Assert.IsTrue(b.Content == verwachte_inhoud);
        }
        [TestMethod]
        public void RegentonAreCustomValues1()
        {
            //init test
            var type_inhoud = Regenton.Size.Medium;
            uint verwachte_maxinhoud = 120;
            //do test
            var b = new Regenton(type_inhoud);
            
            //evaluate test
            Assert.IsTrue(b.Capacity == verwachte_maxinhoud);
        }
        [TestMethod]
        public void RegentonAreCustomValues2()
        {
            //init test
            var type_inhoud = Regenton.Size.Large;
            uint verwachte_maxinhoud = 160;
            uint verwachte_inhoud = 10;
            //do test
            var b = new Regenton(verwachte_inhoud, type_inhoud);

            //evaluate test
            Assert.IsTrue(b.Content == verwachte_inhoud);
            Assert.IsTrue(b.Capacity == verwachte_maxinhoud);
        }
        public void RegentonCanContentBeLargerThenTotalCapacity()
        {
            //init test
            uint inhoud = 81/*Liter*/;
            //do test
            //evaluate test
            Assert.ThrowsException<ArgumentException>(() => new Regenton(inhoud));
        }

        [TestMethod]
        public void RegentonCustomInhoudMinValuesInclusive()
        {
            //init test
            uint inhoud = 0/*Liter*/;
            uint default_maxinhoud = 80;
            //do test
            var b = new Regenton(inhoud);
            //evaluate test
            Assert.IsTrue(b.Content == inhoud);
            Assert.IsTrue(b.Capacity == default_maxinhoud);
        }

        [TestMethod]
        public void RegentonCustomMaxValuesInclusive()
        {
            //init test
            uint inhoud = 80/*Liter*/;
            uint default_maxinhoud = 80;
            //do test
            var b = new Regenton(inhoud);
            //evaluate test
            Assert.IsTrue(b.Content == inhoud);
            Assert.IsTrue(b.Capacity == default_maxinhoud);
        }

        [TestMethod]
        public void RegentonVulTest1()
        {
            //init test
            uint liters_regen = 5/*Liter*/;
            var b = new Regenton();
            //do test
            b.Fill(liters_regen);
            //evaluate test
            Assert.IsTrue(b.Content == liters_regen);
        }
        [TestMethod]
        public void RegentonLeegTest1()
        {
            //init test
            uint liters_regen = 5/*Liter*/;
            uint verwacht_resterend = 0;
            var b = new Regenton(liters_regen);
            //do test
            b.Empty();
            //evaluate test
            Assert.IsTrue(b.Content == verwacht_resterend);
        }
        [TestMethod]
        public void RegentonLeegTest2()
        {
            //init test
            uint liters_regen = 5/*Liter*/;
            uint liters_te_verwijderen = 2/*Liter*/;
            uint verwacht_resterend = 3;
            var b = new Regenton(liters_regen);
            //do test
            b.Empty(liters_te_verwijderen);
            //evaluate test
            Assert.IsTrue(b.Content == verwacht_resterend);
        }

        [TestMethod]
        public void RegentonFullWithoutAllOtherEventsTest()
        {
            //init test
            uint liters_regen = 80/*Liter*/;
            var b = new Regenton();
            bool has_triggered_event = false;
            b.Full += () => { has_triggered_event = true; };
            b.Overflows += () => { Assert.Fail(); return true; };
            b.Overflown += (uint amount) => { Assert.Fail(); };
            //do test
            b.Fill(liters_regen);
            //evaluate test
            Assert.IsTrue(has_triggered_event);
            Assert.IsTrue(b.Content == b.Capacity);
        }
        [TestMethod]
        public void RegentonWillOverflowTest()
        {
            //init test
            uint liters_regen = 200/*Liter*/;
            uint default_inhoud = 0;
            var b = new Regenton();
            bool has_triggered_event = false;
            b.Overflows += () => { has_triggered_event = true; return false; };
            //do test
            b.Fill(liters_regen);
            //evaluate test
            Assert.IsTrue(has_triggered_event);
            Assert.IsTrue(b.Content == default_inhoud);
        }
        [TestMethod]
        public void RegentonHasOverflownTest()
        {
            //init test
            uint liters_regen = 85/*Liter*/;
            uint overflow_amount = 5;
            var b = new Regenton();
            bool has_triggered_event = false;
            b.Overflown += (uint amount) => {
                Assert.IsTrue(amount == overflow_amount);
                has_triggered_event = true;
            };
            //do test
            b.Fill(liters_regen);
            //evaluate test
            Assert.IsTrue(has_triggered_event);
            Assert.IsTrue(b.Content == b.Capacity);
        }

        [TestMethod]
        public void RegentonTestContentSetterOutOfRange()
        {
            //init test
            uint test_inhoud = 81; //(uint)Regenton.Size.Small+1;
            //do test
            var b = new Regenton();
            //evaluate test
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => b.Content = test_inhoud);
        }
    }
}