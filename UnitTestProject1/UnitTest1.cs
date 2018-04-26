using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSED04_HotelCecil;
using DSED04_HotelCecil.Data;


namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            Booking myBooking = new Booking();

            Assert.IsNotNull(myBooking.BookingID);


        }
    }
}
