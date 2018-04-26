using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSED04_HotelCecil.Data
{
    class Rooms
    {
        public int RoomTypeID { get; set; }

        public string RoomTypeName { get; set; }

        public int SingleBed { get; set; }

        public int QueenBed { get; set; }

        public decimal RoomCharge { get; set; }

        public string RoomFeatures { get; set; }

        public IEnumerable AllRooms()
        {
            using (var context = new Entities2())
            {
                var alldata = from r in context.RoomTypes
                    select new
                    {
                        r.RoomTypeID,
                        r.RoomTypeName,
                        r.SingleBed,
                        r.QueenBed,
                        r.RoomCharge,
                        r.RoomFeatures
                    };

                return alldata.ToList();

            }
        }

        public IEnumerable RoomName()
        {
            using (var context = new Entities2())
            {
                var alldata = from r in context.RoomTypes
                              select new
                              {
                                 
                                  r.RoomTypeName,
                               
                              };

                return alldata.ToList();

            }
        }
        public void DeleteRoom()
        {
            using (var context = new Entities2())
            {

                var room = (from r in context.RoomTypes where r.RoomTypeID == RoomTypeID select r).SingleOrDefault();

                context.RoomTypes.Remove(room);

                context.SaveChanges();
            }
        }

        public void InsertRoom()
        {
            using (var context = new Entities2())
            {
                var roomn = new RoomType();

                roomn.RoomTypeName = RoomTypeName;
                roomn.SingleBed = SingleBed;
                roomn.QueenBed = QueenBed;
                roomn.RoomFeatures = RoomFeatures;
               roomn.RoomCharge = RoomCharge;


                /*  Add to entity set of context  */

                context.RoomTypes.Add(roomn);
                context.SaveChanges();
            }
        }

        public void UpdateRoom()
        {
            using (var context = new Entities2())
            {


                var query = from r in context.RoomTypes where r.RoomTypeID == this.RoomTypeID select r;


                //int id = guestidnumber;
                var room = query.FirstOrDefault();

                room.RoomTypeID = this.RoomTypeID;
                room.RoomTypeName = this.RoomTypeName;
                room.SingleBed = this.SingleBed;
                room.QueenBed = this.QueenBed;
                room.RoomFeatures = this.RoomFeatures;
                room.RoomCharge = this.RoomCharge;



                context.SaveChanges();




            }
        }
    }

   
}
