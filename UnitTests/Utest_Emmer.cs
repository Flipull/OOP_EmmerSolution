using LiquidContainerLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    [TestClass]
    public class Utest_Emmer
    {
        /*
            emmer: check default ctor 0L/12L
            olievat: default const TotalCapacity=159
            emmer: 0..TotalCapacity/ TotalCapacity=[10..inf]
            regenton: default const sizes S,M,L
            all concrete types:
                uint amount of content
                uint capamount of total capacity
                exceptest: amount == [-inf..-1]
                exceptest: amount > capamount
                exceptest: capamount < 10
                assert: emmer.Cap equals amount
                assert: emmer.Cap < emmer.TotalCap
                assert: emmer.CapLeft equals TotalCap-amount
        */
        [TestMethod]
        public void EmmerDefaults()
        {
            //init test
            uint default_inhoud = 0;
            uint default_maxinhoud = 12;
            //do test
            var b = new Emmer();
            
            //evaluate test
            Assert.IsTrue(b.Content == default_inhoud);
            Assert.IsTrue(b.Capacity == default_maxinhoud);
            Assert.IsTrue(b.CapacityLeft == default_maxinhoud - default_inhoud);
        }
        [TestMethod]
        public void EmmerAreCustomValuesSet()
        {
            //init test
            uint inhoud = 1;
            uint max_inhoud = 12;
            //do test
            var b = new Emmer(inhoud, max_inhoud);

            //evaluate test
            Assert.IsTrue(b.Content == inhoud);
            Assert.IsTrue(b.Capacity == max_inhoud);
            Assert.IsTrue(b.CapacityLeft == max_inhoud - inhoud);
        }
        [TestMethod]
        public void EmmerMaxCapacityOutofBoundary1()
        {
            //init test
            uint maxinhoud = 0/*Liter*/;
            //do test
            //evaluate test
            Assert.ThrowsException<ArgumentException>(() => new Emmer(capacity: maxinhoud));
        }
        [TestMethod]
        public void EmmerMaxCapacityOutofBoundary2()
        {
            //init test
            uint maxinhoud = 9/*Liter*/;
            //do test
            //evaluate test
            Assert.ThrowsException<ArgumentException>(() => new Emmer(capacity: maxinhoud));
        }
        public void EmmerCanContentBeLargerThenTotalCapacity()
        {
            //init test
            uint inhoud = 13/*Liter*/;
            uint max_inhoud = 12;
            //do test
            //evaluate test
            Assert.ThrowsException<ArgumentException>(() => new Emmer(inhoud, max_inhoud));
        }


        [TestMethod]
        public void EmmerCustomMinValuesInclusive()
        {
            //init test
            uint inhoud = 0/*Liter*/;
            uint max_inhoud = 10;
            //do test
            var b = new Emmer(inhoud, max_inhoud);

            //evaluate test
            Assert.IsTrue(b.Content == inhoud);
            Assert.IsTrue(b.Capacity == max_inhoud);
            Assert.IsTrue(b.CapacityLeft == max_inhoud - inhoud);
        }
        /*
        [TestMethod]
        public void EmmerCustomMaxValuesInclusive()
        {

        }
        */

        [TestMethod]
        public void EmmerVulTest1()
        {
            //init test
            uint liters_water = 5/*Liter*/;
            var b = new Emmer();
            //do test
            b.Fill(liters_water);
            //evaluate test
            Assert.IsTrue(b.Content == liters_water);
        }
        [TestMethod]
        public void EmmerVulTest2()
        {
            //init test
            uint liters_water = 5/*Liter*/;
            var donor = new Emmer(liters_water);
            var b = new Emmer();
            //do test
            b.Fill(donor);
            //evaluate test
            Assert.IsTrue(b.Content == liters_water);
        }
        [TestMethod]
        public void EmmerLeegTest1()
        {
            //init test
            uint liters_water = 5/*Liter*/;
            uint verwacht_resterend = 0;
            var b = new Emmer(liters_water);
            //do test
            b.Empty();
            //evaluate test
            Assert.IsTrue(b.Content == verwacht_resterend);
        }
        [TestMethod]
        public void EmmerLeegTest2()
        {
            //init test
            uint liters_water = 5/*Liter*/;
            uint liters_te_verwijderen = 2/*Liter*/;
            uint verwacht_resterend = 3;
            var b = new Emmer(liters_water);
            //do test
            b.Empty(liters_te_verwijderen);
            //evaluate test
            Assert.IsTrue(b.Content == verwacht_resterend);
        }

        [TestMethod]
        public void EmmerFullWithoutOtherEventsTest()
        {
            //init test
            uint liters_water = 12/*Liter*/;
            var b = new Emmer();
            bool has_triggered_event = false;
            b.Full += () => { has_triggered_event = true; };
            b.Overflows += () => { Assert.Fail(); return true; };
            b.Overflown += (uint amount) => { Assert.Fail(); };
            //do test
            b.Fill(liters_water);
            //evaluate test
            Assert.IsTrue(has_triggered_event);
            Assert.IsTrue(b.Content == b.Capacity);
        }
        [TestMethod]
        public void EmmerWillOverflowTest()
        {
            //init test
            uint liters_water = 20/*Liter*/;
            uint default_inhoud = 0;
            var b = new Emmer();
            bool has_triggered_event = false;
            b.Overflows += () => { has_triggered_event = true; return false; };
            //do test
            b.Fill(liters_water);
            //evaluate test
            Assert.IsTrue(has_triggered_event);
            Assert.IsTrue(b.Content == default_inhoud);
        }
        [TestMethod]
        public void EmmerHasOverflownTest()
        {
            //init test
            uint liters_water = 20/*Liter*/;
            uint overflow_amount = 8;//based on default 12L;20-12=8
            var b = new Emmer();
            bool has_triggered_event = false;
            b.Overflown += (uint amount) => { 
                    Assert.IsTrue(amount == overflow_amount);
                    has_triggered_event = true;
            };
            //do test
            b.Fill(liters_water);
            //evaluate test
            Assert.IsTrue(has_triggered_event);
            Assert.IsTrue(b.Content == b.Capacity);
        }

        /* !!!!! */
        [TestMethod]
        public void EmmerStopOverflowingCrazyTest()
        {
            //init test
            uint liters_water = 20/*Liter*/;
            uint default_inhoud = 0;
            var b = new Emmer();
            b.Overflows += () => { return false; };
            b.Overflows += () => { return true; };
            b.Overflown += (uint amount) => { Assert.Fail(); };
            //do test
            b.Fill(liters_water);
            //evaluate test
            Assert.IsTrue(b.Content == default_inhoud);
        }
        /* !!!!! */
        [TestMethod]
        public void EmmerCanBeFilledByItself()
        {
            //init test
            uint liters_water = 10/*Liter*/;
            var b = new Emmer(liters_water);
            b.Overflows += () => { Assert.Fail(); return true; };
            b.Overflown += (uint amount) => Assert.Fail();
            //do test

            //test exceptions when thrown during filling? 
            //as exception maybe is expected behaviour (or no change at all)
            //evaluate test
            Assert.ThrowsException<ArgumentException>(() => b.Fill(b));
            Assert.IsTrue(b.Content == liters_water);
        }

    }
}
