using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSED04_HotelCecil.Data;

namespace DSED04_HotelCecil
{
    class Booking
    {
        public int GuestID { get; set; }

        public int RoomID { get; set; }

        public int BookingID { get; set; }

        public int NumGuests { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public DateTime BookingDate { get; set; }

        public bool MiniBar { get; set; }

        public bool Wifi { get; set; }

        public int ExtraBed { get; set; }

        public int ExtraGuest { get; set; }

        public decimal RoomCharge { get; set; }

        public IEnumerable AllBookings()
        {
            using (var context = new Entities2())
            {
                var alldata = from b in context.Bookings
                    where b.GuestIDFK == b.Guest.GuestID && b.RoomIDFK == b.RoomType.RoomTypeID
                    select new
                    {
                        b.GuestIDFK,
                        b.Guest.Name,
                        b.RoomIDFK,
                        b.RoomType.RoomTypeName,
                        b.RoomType.RoomCharge,
                        b.BookingID,
                        b.NumGuests,
                        b.BookingDate,
                        b.CheckOut,
                        b.CheckIn,
                        b.MiniBar,
                        b.Wifi,
                        b. ExtraBed,
                        b. ExtraGuest,


                    };

                return alldata.ToList();

            }
        }
        public void AddBooking()
        {
         
                using (var context = new Entities2())
                {
                    var rb = new Data.Booking();

                    rb.NumGuests = NumGuests;
                rb.BookingDate = BookingDate;
                rb.GuestIDFK = GuestID;
                rb.RoomIDFK = RoomID;
                rb.CheckIn = CheckIn;
                rb.CheckOut = CheckOut;
                rb.MiniBar = MiniBar;
                    rb.Wifi = Wifi;
                    rb.ExtraGuest = ExtraGuest;
                    rb.ExtraBed = ExtraBed;



                    context.Bookings.Add(rb);
                   context.SaveChanges();
                }
        }
    }


}


