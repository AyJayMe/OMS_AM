using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess;
using Domain;
using Controllers;

namespace OMS_UnitTesting
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void NewOrder()
        {
            OrderStates expectedState = OrderStates.New;
            DateTime expectedDateTime = DateTime.Now;
            OrderHeader testOrderHeader = OrderController.Instance.CreateNewOrderHeader();
            Assert.IsTrue(DateTime.Compare(testOrderHeader.Date, expectedDateTime) <= 1, "Time was not within the expected range.");
            Assert.IsTrue(testOrderHeader.State == expectedState, "Created order state was not in the expected range.");
            Assert.IsTrue(testOrderHeader.Id > 0, "OrderHeader was not valid.");
        }
    }
}
