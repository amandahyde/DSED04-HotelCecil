using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DSED04_HotelCecil.Data
{
    class Billings
    {
       public int BillingID { get; set; }

       public int GuestIDFK { get; set; }

       public int RoomIDFK { get; set; }
    
       public decimal BarCharge { get; set; }

        public decimal WifiCharge { get; set; }

        public decimal ExtraGuestCharge { get; set; }
       
       public decimal ExtraBedCharge { get; set; }

        public decimal TotalRoomCharge { get; set; }

        public IEnumerable AllBillings()
        {
            using (var context = new Entities2())
            {
                var alldata = from bo in context.Billings
                    where  bo.RoomIDFK == bo.RoomType.RoomTypeID && bo.GuestIDFK == bo.Guest.GuestID
                              select new
                    {

                       bo.BillingID,
                       bo.GuestIDFK,
                       bo.Guest.Name,
                       bo.RoomIDFK,
                       bo.RoomType.RoomTypeName,
                       bo.BarCharge,
                       bo.WifiCharge,
                       bo.ExtraBedCharge,
                       bo.ExtraGuestCharge,
                       bo.TotalRoomCharge,
                      
                     

                    };

                return alldata.ToList();

            }
        }

        public void AddBilling()
        {

            using (var context = new Entities2())
            {
                var bo = new Data.Billing();

                bo.BillingID = BillingID;
                bo.GuestIDFK = GuestIDFK;
                bo.RoomIDFK= RoomIDFK;
                bo.BarCharge = BarCharge;
                bo.WifiCharge = WifiCharge;
                bo.ExtraGuestCharge = ExtraGuestCharge;
                bo.ExtraBedCharge = ExtraBedCharge;
                bo.TotalRoomCharge = TotalRoomCharge;


                /*  Add to entity set of context  */

                context.Billings.Add(bo);
                context.SaveChanges();
            }
        }
    }



}
