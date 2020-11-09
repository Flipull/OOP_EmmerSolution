using LiquidContainerLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    [TestClass]
    public class Utest_Olievat
    {
        [TestMethod]
        public void OlievatDefaults()
        {
            //init test
            uint default_inhoud = 0;
            uint default_maxinhoud = 159;
            //do test
            var b = new Olievat();
            
            //evaluate test
            Assert.IsTrue(b.Content == default_inhoud);
            Assert.IsTrue(b.TotalCapacity == default_maxinhoud);
            Assert.IsTrue(b.CapacityLeft == default_maxinhoud - default_inhoud);
        }
        [TestMethod]
        public void OlievatAreCustomValuesSet()
        {
            //init test
            uint inhoud = 1;
            uint default_maxinhoud = 159;
            //do test
            var b = new Olievat(inhoud);
            
            //evaluate test
            Assert.IsTrue(b.Content == inhoud);
            Assert.IsTrue(b.CapacityLeft == default_maxinhoud - inhoud);
        }
        public void OlievatCanContentBeLargerThenTotalCapacity()
        {
            //init test
            uint inhoud = 160/*Liter*/;
            //do test
            //evaluate test
            Assert.ThrowsException<ArgumentException>(() => new Olievat(inhoud) );
        }

        [TestMethod]
        public void OlievatCustomMinValuesInclusive()
        {
            //init test
            uint inhoud = 0/*Liter*/;
            uint default_maxinhoud = 159;
            //do test
            var b = new Olievat(inhoud);
            //how to test Olievat(-1); 
            //outOfRange/Argument-exceptions?

            //evaluate test
            Assert.IsTrue(b.Content == inhoud);
            Assert.IsTrue(b.CapacityLeft == default_maxinhoud - inhoud);
        }
        [TestMethod]
        public void OlievatCustomMaxValuesInclusive()
        {
            //init test
            uint inhoud = 159/*Liter*/;
            uint default_maxinhoud = 159;
            //do test
            var b = new Olievat(inhoud);
            //how to test Olievat(160); 
            //outOfRange/Argument-exceptions?
            
            //evaluate test
            Assert.IsTrue(b.Content == inhoud);
            Assert.IsTrue(b.CapacityLeft == default_maxinhoud - inhoud);
        }

        [TestMethod]
        public void OlievatVulTest1()
        {
            //init test
            uint liters_olie = 5/*Liter*/;
            var b = new Olievat();
            //do test
            b.Fill(liters_olie);
            //evaluate test
            Assert.IsTrue(b.Content == liters_olie);
        }
        [TestMethod]
        public void OlievatVulTest2()
        {
            //init test
            uint liters_olie = 5/*Liter*/;
            var donor = new Olievat(liters_olie);
            var b = new Olievat();
            //do test
            b.Fill(donor);
            //evaluate test
            Assert.IsTrue(b.Content == liters_olie);
        }
        [TestMethod]
        public void OlievatLeegTest1()
        {
            //init test
            uint liters_olie = 5/*Liter*/;
            uint verwacht_resterend = 0;
            var b = new Olievat(liters_olie);
            //do test
            b.Empty();
            //evaluate test
            Assert.IsTrue(b.Content == verwacht_resterend);
        }
        [TestMethod]
        public void OlievatLeegTest2()
        {
            //init test
            uint liters_olie = 5/*Liter*/;
            uint liters_te_verwijderen = 2/*Liter*/;
            uint verwacht_resterend = 3;
            var b = new Olievat(liters_olie);
            //do test
            b.Empty(liters_te_verwijderen);
            //evaluate test
            Assert.IsTrue(b.Content == verwacht_resterend);
        }

        [TestMethod]
        public void OlievatFullWithoutAllOtherEventsTest()
        {
            //init test
            uint liters_olie = 159/*Liter*/;
            var b = new Olievat();
            bool has_triggered_event = false;
            b.isFull += () => { has_triggered_event = true; };
            b.willOverflow += () => { Assert.Fail(); return true; };
            b.hasOverflown += (uint amount) => { Assert.Fail(); };
            //do test
            b.Fill(liters_olie);
            //evaluate test
            Assert.IsTrue(has_triggered_event);
        }
        [TestMethod]
        public void OlievatWillOverflowTest()
        {
            //init test
            uint liters_olie = 160/*Liter*/;
            uint default_inhoud = 0;
            var b = new Olievat();
            bool has_triggered_event = false;
            b.willOverflow += () => { has_triggered_event = true; return false; };
            //do test
            b.Fill(liters_olie);
            //evaluate test
            Assert.IsTrue(has_triggered_event);
            Assert.IsTrue(b.Content == default_inhoud);
        }
        [TestMethod]
        public void OlievatHasOverflownTest()
        {
            //init test
            uint liters_olie = 200/*Liter*/;
            uint overflow_amount = 41;
            var b = new Olievat();
            bool has_triggered_event = false;
            b.hasOverflown += (uint amount) => {
                Assert.IsTrue(amount == overflow_amount);
                has_triggered_event = true;
            };
            //do test
            b.Fill(liters_olie);
            //evaluate test
            Assert.IsTrue(has_triggered_event);
        }

        /* !!!!! */
        [TestMethod]
        public void OlievatStopOverflowingCrazyTest()
        {
            //init test
            uint liters_olie = 160/*Liter*/;
            uint default_inhoud = 0;
            var b = new Olievat();
            b.willOverflow += () => { return false; };
            b.willOverflow += () => { return true; };
            b.hasOverflown += (uint amount) => { Assert.Fail(); };
            //do test
            b.Fill(liters_olie);
            //evaluate test
            Assert.IsTrue(b.Content == default_inhoud);
        }
        /* !!!!! */
        [TestMethod]
        public void OlievatCanBeFilledByItself()
        {
            //init test
            uint liters_olie = 100/*Liter*/;
            var b = new Olievat(liters_olie);
            b.hasOverflown += (uint amount) => Assert.Fail();
            b.willOverflow += () => { Assert.Fail(); return false; };
            //do test

            //test exceptions when thrown during filling? 
            //as exception maybe is expected behaviour (or no change at all)
            //evaluate test
            Assert.ThrowsException<ArgumentException>(() => b.Fill(b));
            Assert.IsTrue(b.Content == liters_olie);
        }
    }
}
