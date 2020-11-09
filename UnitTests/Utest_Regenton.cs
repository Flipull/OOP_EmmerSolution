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
            var b = new Regenton(size: type_inhoud);

            //evaluate test
            Assert.IsTrue(b.TotalCapacity == verwachte_maxinhoud);
            Assert.IsTrue(b.Content == verwachte_inhoud);
        }
        [TestMethod]
        public void RegentonAreCustomValues1()
        {
            //init test
            var type_inhoud = Regenton.Size.Medium;
            uint verwachte_maxinhoud = 120;
            //do test
            var b = new Regenton(size: type_inhoud);

            //evaluate test
            Assert.IsTrue(b.TotalCapacity == verwachte_maxinhoud);
        }
        [TestMethod]
        public void RegentonAreCustomValues2()
        {
            //init test
            var type_inhoud = Regenton.Size.Large;
            uint verwachte_maxinhoud = 160;
            //do test
            var b = new Regenton(size: type_inhoud);

            //evaluate test
            Assert.IsTrue(b.TotalCapacity == verwachte_maxinhoud);
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
            Assert.IsTrue(b.TotalCapacity == default_maxinhoud);
            Assert.IsTrue(b.CapacityLeft == default_maxinhoud - inhoud);
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
            Assert.IsTrue(b.TotalCapacity == default_maxinhoud);
            Assert.IsTrue(b.CapacityLeft == default_maxinhoud - inhoud);
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
        public void RegentonVulTest2()
        {
            //init test
            uint liters_regen = 5/*Liter*/;
            var donor = new Regenton(liters_regen);
            var b = new Regenton();
            //do test
            b.Fill(donor);
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
            b.isFull += () => { has_triggered_event = true; };
            b.willOverflow += () => { Assert.Fail(); return true; };
            b.hasOverflown += (uint amount) => { Assert.Fail(); };
            //do test
            b.Fill(liters_regen);
            //evaluate test
            Assert.IsTrue(has_triggered_event);
            Assert.IsTrue(b.Content == b.TotalCapacity);
        }
        [TestMethod]
        public void RegentonWillOverflowTest()
        {
            //init test
            uint liters_regen = 200/*Liter*/;
            uint default_inhoud = 0;
            var b = new Regenton();
            bool has_triggered_event = false;
            b.willOverflow += () => { has_triggered_event = true; return false; };
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
            b.hasOverflown += (uint amount) => {
                Assert.IsTrue(amount == overflow_amount);
                has_triggered_event = true;
            };
            //do test
            b.Fill(liters_regen);
            //evaluate test
            Assert.IsTrue(has_triggered_event);
            Assert.IsTrue(b.Content == b.TotalCapacity);
        }

        /* !!!!! */
        [TestMethod]
        public void RegentonStopOverflowingCrazyTest()
        {
            //init test
            uint liters_regen = 200/*Liter*/;
            uint default_inhoud = 0;
            var b = new Regenton();
            b.willOverflow += () => { return false; };
            b.willOverflow += () => { return true; };
            b.hasOverflown += (uint amount) => { Assert.Fail(); };
            //do test
            b.Fill(liters_regen);
            //evaluate test
            Assert.IsTrue(b.Content == default_inhoud);
        }
        /* !!!!! */
        [TestMethod]
        public void RegentonCanBeFilledByItself()
        {
            //init test
            uint liters_regen = 60/*Liter*/;
            var b = new Regenton(liters_regen);
            b.hasOverflown += (uint amount) => Assert.Fail();
            b.willOverflow += () => { Assert.Fail(); return false; };
            //do test
            //test exceptions when thrown during filling? 
            //as exception maybe is expected behaviour (or no change at all)
            //evaluate test
            Assert.ThrowsException<ArgumentException>(() => b.Fill(b));
            Assert.IsTrue(b.Content == liters_regen);
        }

    }
}