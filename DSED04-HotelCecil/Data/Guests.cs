using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DSED04_HotelCecil.Data;

namespace DSED04_HotelCecil.Data
{

  
    }
class Guests
{

    public int GuestID { get; set; }

    public string GuestName { get; set; }

    public string GuestPhone { get; set; }

    public string GuestAddress { get; set; }

    public string GuestCity { get; set; }

    public IEnumerable AllGuests()
    {
        using (var context = new Entities2())
        {
            var alldata = from g in context.Guests
                          select new
                          {
                              g.GuestID,
                              g.Name,
                              g.Address,
                              g.City,
                              g.Phone,

                          };

            return alldata.ToList();

        }
    }

    public IEnumerable GuestsID()

    {
        using (var context = new Entities2())
        {
            var alldata = from g in context.Guests
                          select new
                          {
                             
                              g.GuestID,
                           

                          };

            return alldata.ToList();

        }
    }

    public void DeleteGuest()
    {

        try
        {



            using (var context = new Entities2())
            {

                var guest = (from g in context.Guests where g.GuestID == GuestID select g).SingleOrDefault();

                context.Guests.Remove(guest);

                context.SaveChanges();
            }

        }
      
            catch (Exception)
        {
            MessageBox.Show("You cannot delete a guest with a current booking");
        }


    
    }
    


   
        public void InsertGuest()
    {
        using (var context = new Entities2())
        {
            var guestd = new Guest();

            guestd.Name = GuestName;
            guestd.Address = GuestAddress;
            guestd.Phone = GuestPhone;
            guestd.City = GuestCity;


            /*  Add to entity set of context  */

            context.Guests.Add(guestd);
            context.SaveChanges();
        }
    }

    public void UpdateGuest()
    {
        using (var context = new Entities2())
        {
          

            var query = from g in context.Guests where g.GuestID == this.GuestID select g;


            //int id = guestidnumber;
            var guest = query.FirstOrDefault();

            guest.GuestID = this.GuestID;
            guest.Name = this.GuestName;
            guest.Address = this.GuestAddress;
            guest.Phone = this.GuestPhone;
            guest.City = this.GuestCity;
            //guestd.Name = GuestName;
            //guestd.Address = GuestAddress;
            //guestd.Phone = GuestPhone;
            //guestd.City = GuestCity;


            ///*  Add to entity set of context  */

            context.Guests.AddOrUpdate(guest);
            context.SaveChanges();




        }
    }

}
       


