using CarrierProblem;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CarrierProblem.Contracts;

namespace CarrierTests
{
    [TestClass]
    public class CarrierTest
    {
        [TestMethod]
        public void Should_Return_Correct_List_For_Given_Parameters_Carrier_Manager()
        {

            var carrierWorkingBetween_1_23 = new HourlyManagedCarrier(new HourlyManagedTransport(1, 23));
            var carrierWorkingBetween_2_4 = new HourlyManagedCarrier(new HourlyManagedTransport(2, 4));
            var carrierWorkingBetween_6_9 = new HourlyManagedCarrier(new HourlyManagedTransport(6, 9));
            var carrierWorkingBetween_5_8 = new HourlyManagedCarrier(new HourlyManagedTransport(5, 8));

            var carrierManager = new HourManagedCarrierManager();
            carrierManager.Add(carrierWorkingBetween_1_23)
                          .Add(carrierWorkingBetween_2_4)
                          .Add(carrierWorkingBetween_5_8)
                          .Add(carrierWorkingBetween_6_9);

            var expected = new List<Carrier<int>>(){
                                    carrierWorkingBetween_1_23,
                                    carrierWorkingBetween_5_8,
                                    carrierWorkingBetween_6_9,
                           };

            var result = carrierManager.FindTheMostIntersectingCarriers();
            Assert.AreEqual(3, result.Count);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Should_Throw_Exception_With_Negative_CheckInTime()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new HourlyManagedCarrier(new HourlyManagedTransport(-1, 23)));
        }

        [TestMethod]
        public void Should_Throw_Exception_If_Transport_Is_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new HourlyManagedCarrier(null));
        }

        [TestMethod]
        public void Should_Throw_Exception_With_Negative_CheckOutTime()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new HourlyManagedTransport(1, -4));
        }

        [TestMethod]
        public void Should_Throw_Exception_With_Smaller_CheckOutTime_Than_CheckInTime()
        {
            Assert.ThrowsException<ApplicationException>(() => new HourlyManagedTransport(5, 1));
        }

        [TestMethod]
        public void Should_Set_Correctly_CurrentTransport()
        {
            var checkInTime = 1;
            var checkOutTime = 22;
            var hourlyManagedCarrier = new HourlyManagedCarrier(new HourlyManagedTransport(checkInTime, checkOutTime));

            Assert.AreNotEqual(null, hourlyManagedCarrier.CurrentTransport);
            Assert.AreEqual(checkInTime, hourlyManagedCarrier.CurrentTransport.CheckInTime);
            Assert.AreEqual(checkOutTime, hourlyManagedCarrier.CurrentTransport.CheckOutTime);
        }


        [TestMethod]
        public void Should_Return_Correct_Intersections_For_Given_Parameters_IntersectionCatcher()
        {
            var carrierWorkingBetween_1_23 = new HourlyManagedCarrier(new HourlyManagedTransport(1, 23));
            var carrierWorkingBetween_2_4 = new HourlyManagedCarrier(new HourlyManagedTransport(2, 4));
            var carrierWorkingBetween_6_9 = new HourlyManagedCarrier(new HourlyManagedTransport(6, 9));
            var carrierWorkingBetween_5_8 = new HourlyManagedCarrier(new HourlyManagedTransport(5, 8));

            var catcher = new HourlyManagedCarrierIntersactionCatcher();
            var carrierList = new List<Carrier<int>>()
            {
                          carrierWorkingBetween_1_23,
                          carrierWorkingBetween_2_4,
                          carrierWorkingBetween_5_8,
                          carrierWorkingBetween_6_9
            };
            
            var expected = new List<Carrier<int>>(){
                                    carrierWorkingBetween_1_23,
                                    carrierWorkingBetween_5_8,
                                    carrierWorkingBetween_6_9,
                           };

            var result = catcher.FindTheMostIntersectingCarriers(carrierList);
            Assert.AreEqual(3, result.Count);
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Should_Throw_Exception_If_Carrier_List_Is_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new HourlyManagedCarrierIntersactionCatcher().FindTheMostIntersectingCarriers(null));
        }

    }
}
