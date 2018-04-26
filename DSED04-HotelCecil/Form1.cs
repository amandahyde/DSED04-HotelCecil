using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DSED04_HotelCecil.Data;

namespace DSED04_HotelCecil
{
    public partial class Form1 : Form
    {

        Guests myGuests = new Guests();
        Rooms myRoom = new Rooms();
        Booking myBooking = new Booking();
        Billings myBillings = new Billings();

     

        public Form1()
        {
            InitializeComponent();

            //set screen size
            this.MaximumSize = new System.Drawing.Size(1500, 1100);
            this.MinimumSize = new System.Drawing.Size(1500, 1100);
            Guests.Visible = false;


            //get and display data grid view for all four tables
            DGVRoom.DataSource = myRoom.AllRooms();
            DGVRoom.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DGVGuests.DataSource = myGuests.AllGuests();
            DGVGuests.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DGVBookings.DataSource = myBooking.AllBookings();
            DGVBookings.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DGVBillings.DataSource = myBillings.AllBillings();
            DGVBillings.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //set check in date so a past date cannot be selected
            DTPCheckIN.MinDate = DateTime.Today;

            //set check out value to be at least 1 day after check in
            DTPCheckOUT.Value = DTPCheckIN.Value.AddDays(1).Date;

            DTPCheckOUT.MinDate = DTPCheckIN.Value.AddDays(1).Date;

            lbldays.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        //display guest info on form
        private void DGVGuests_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtGuestID.Text = DGVGuests.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtGuestName.Text = DGVGuests.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtPhone.Text = DGVGuests.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtAddress.Text = DGVGuests.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtCity.Text = DGVGuests.Rows[e.RowIndex].Cells[3].Value.ToString();

            CBXGuest.Items.Add(DGVGuests.Rows[e.RowIndex].Cells[1].Value.ToString());

        

          

        }


        //delete guest
        public void btnDeleteGuest_Click(object sender, EventArgs e)
        {
    
            myGuests.GuestID = Convert.ToInt16(txtGuestID.Text);

            myGuests.DeleteGuest();

            DGVGuests.DataSource = myGuests.AllGuests();
        }


        //insert guest and refresh data grid view
       public void btnInsert_Click(object sender, EventArgs e)
       {

           myGuests.GuestAddress = txtAddress.Text;
           myGuests.GuestName = txtGuestName.Text;
           myGuests.GuestPhone = txtPhone.Text;
           myGuests.GuestCity = txtCity.Text;

           myGuests.InsertGuest();

           DGVGuests.DataSource = myGuests.AllGuests();
        }


        //update guest details and refresh data grid view
        private void btnUpdate_Click(object sender, EventArgs e)
        {

            myGuests.GuestID = Convert.ToInt16(txtGuestID.Text);

            myGuests.GuestAddress = txtAddress.Text;
            myGuests.GuestName = txtGuestName.Text;
            myGuests.GuestPhone = txtPhone.Text;
            myGuests.GuestCity = txtCity.Text;

            myGuests.UpdateGuest();

            DGVGuests.DataSource = myGuests.AllGuests();

        }


        //display room details on editable form
        private void DGVRoom_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtRoomID.Text = DGVRoom.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtRoomName.Text = DGVRoom.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtSingleBeds.Text = DGVRoom.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtDoubleBeds.Text = DGVRoom.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtRoomCost.Text = DGVRoom.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtAmenitiesList.Text = DGVRoom.Rows[e.RowIndex].Cells[5].Value.ToString();

            CBXRoom.Items.Add(DGVRoom.Rows[e.RowIndex].Cells[1].Value.ToString());

        }


        //delete room
        private void btnDeleteRoom_Click(object sender, EventArgs e)
        {
            

            try
            {
                myRoom.RoomTypeID = Convert.ToInt16(txtRoomID.Text);
                myRoom.DeleteRoom();
                DGVRoom.Refresh();
            }
            catch (Exception)
            {

                MessageBox.Show("You cannot delete a room with a current booking");
            }
          

            DGVRoom.DataSource = myRoom.AllRooms();

         
        }


        //add new room
        private void btnInsertRoom_Click(object sender, EventArgs e)
        {
            myRoom.RoomTypeName = txtRoomName.Text;
            myRoom.SingleBed = Convert.ToInt16(txtSingleBeds.Text);
            myRoom.QueenBed = Convert.ToInt16(txtDoubleBeds.Text);
            myRoom.RoomFeatures = txtAmenitiesList.Text;
           

            myRoom.InsertRoom();
            DGVRoom.Refresh();

            DGVRoom.DataSource = myRoom.AllRooms();
        }


        //update room and refresh DGV
        private void btnUpdateRoom_Click(object sender, EventArgs e)
        {

            myRoom.RoomTypeID = Convert.ToInt16(txtRoomID.Text);

            myRoom.RoomTypeName = txtRoomName.Text;
            myRoom.SingleBed = Convert.ToInt16(txtSingleBeds.Text);
            myRoom.QueenBed = Convert.ToInt16(txtDoubleBeds.Text);
            myRoom.RoomFeatures = txtAmenitiesList.Text;
            myRoom.RoomCharge = Convert.ToDecimal(txtRoomCost.Text);



            myRoom.UpdateRoom();
            DGVRoom.Refresh();

            DGVRoom.DataSource = myRoom.AllRooms();
        }


        //Add new hotel booking and refresh DGV
        private void btnBooking_Click(object sender, EventArgs e)
        {
           

            try
            {
                myBooking.BookingDate = DateTime.Now;
                myBooking.GuestID = Convert.ToInt16(CBXGuest.SelectedValue);
                myBooking.RoomID = Convert.ToInt16(CBXRoom.SelectedValue);
                myBooking.CheckIn = Convert.ToDateTime(DTPCheckIN.Value);
                myBooking.CheckOut = Convert.ToDateTime(DTPCheckOUT.Value);
                myBooking.NumGuests = Convert.ToInt16(txtNOGuests.Text);
                myBooking.Wifi = CbxWifi.Checked;
                myBooking.ExtraBed = Convert.ToInt16(txtExtraBeds.Text);
                myBooking.ExtraGuest = Convert.ToInt16(txtExtraGuest.Text);

                //calculate total days customer is staying in hotel
                lblDay.Text = (myBooking.CheckOut - myBooking.CheckIn).TotalDays.ToString();

                lbldays.Visible = false;

     

                //if checkbox is checked customer is charged for wifi
                if (CbxWifi.Checked)
                {
                    myBooking.Wifi = true;
                }

                //if checkbox is checked customer if charged for bar
                if (CbxBreakfast.Checked == true)
                {
                    myBooking.MiniBar = true;
                }


                //customer cannot book more than 2 extras beds or guests
                if (myBooking.ExtraBed >= 3 || myBooking.ExtraGuest >= 3)
                {
                    MessageBox.Show("No more than 2 Extra Beds / Guests are allowed per room. Please change your selection!");

                }


                else
                {
                    myBooking.AddBooking();
                    MessageBox.Show("Booking has been made!");
                    Guests.Visible = true;
                    panel4.Visible = false;


                }

                DGVBookings.Refresh();

                DGVBookings.DataSource = myBooking.AllBookings();


               
            }
            catch (Exception)
            {
                //show if not all fields entered correctly
                MessageBox.Show("All fields must be entered");
            }
           
        
        }


        //display booking information to non editable form
        private void DGVBookings_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            lblGuestIDFK.Text = DGVBookings.Rows[e.RowIndex].Cells[0].Value.ToString();
            lblGuestName.Text = DGVBookings.Rows[e.RowIndex].Cells[1].Value.ToString();
            lblRoomIDFK.Text = DGVBookings.Rows[e.RowIndex].Cells[2].Value.ToString();
 
            lblRoomBooked.Text = DGVBookings.Rows[e.RowIndex].Cells[3].Value.ToString();
            lblBookingDate.Text = DGVBookings.Rows[e.RowIndex].Cells[8].Value.ToString();
            lblCheckInDate.Text = DGVBookings.Rows[e.RowIndex].Cells[7].Value.ToString();
            lblCheckOutDate.Text = DGVBookings.Rows[e.RowIndex].Cells[8].Value.ToString();
            lblWifi.Text = DGVBookings.Rows[e.RowIndex].Cells[10].Value.ToString();
            lblBarCharge.Text = DGVBookings.Rows[e.RowIndex].Cells[9].Value.ToString();
           lblExtraBed.Text = DGVBookings.Rows[e.RowIndex].Cells[12].Value.ToString();
           lblExtraGuest.Text  = DGVBookings.Rows[e.RowIndex].Cells[13].Value.ToString();
            lblBookingRoomCharge.Text = DGVBookings.Rows[e.RowIndex].Cells[4].Value.ToString();


            if (lblWifi.Text == "False")
            {
                lblWifi.Text = "No";
            }

            else
            {
                lblWifi.Text = "Yes";
            }

            if (lblBarCharge.Text == "False")
            {
                lblBarCharge.Text = "Yes";
            }
            else
            {
                lblBarCharge.Text = "Yes";
            }

            //generate costs of extra beds
            if (lblExtraBed.Text == "1")

            {
                lblExtraBed.Text = "10.00";
            }

          
            else if (lblExtraBed.Text == "2")
            {
                lblExtraBed.Text = "15.00";
            }

            else
            {
                lblExtraBed.Text = "0";
            }

            //generate costs of extra guests
            if (lblExtraGuest.Text == "1")

            {
                lblExtraGuest.Text = "10.00";
            }


            else if (lblExtraGuest.Text == "2")
            {
                lblExtraGuest.Text = "15.00";
            }

            else
            {
                lblExtraGuest.Text = "0";
            }


        
      
        }

        private void btnBill_Click(object sender, EventArgs e)
        {
            //work out billing charges 

            if (lblDay.Text == "2")

            {
                myBillings.TotalRoomCharge = myBooking.RoomCharge * 2;

            }

            myBillings.GuestIDFK = Convert.ToInt16(lblGuestIDFK.Text);
            myBillings.RoomIDFK = Convert.ToInt16(lblRoomIDFK.Text);
            myBillings.ExtraBedCharge = Convert.ToDecimal(lblExtraBed.Text);
            myBillings.ExtraGuestCharge = Convert.ToDecimal(lblExtraGuest.Text);
            myBillings.TotalRoomCharge = Convert.ToDecimal(lblBookingRoomCharge.Text) + Convert.ToDecimal(lblExtraBed.Text) + Convert.ToDecimal(lblExtraGuest.Text) + myBillings.WifiCharge + myBillings.BarCharge;

            //add additional costs
            if (lblWifi.Text == "Yes")
            {
                myBillings.WifiCharge = 5;
                lblBillWifiCharge.Text = "5.00";
            }

            else
            {
                myBillings.WifiCharge = 0;
                lblBillWifiCharge.Text = "0";
              
            }


            if (lblBarCharge.Text == "Yes")
            {
                myBillings.BarCharge = 10;
                lblBillBreakfastCharge.Text = "10";
            }

            else
            {
                myBillings.BarCharge = 0;
                lblBillBreakfastCharge.Text = "0";
            }

            
            //display billing charges on form
            lblBillGuest.Text = lblGuestName.Text;
            lblBillRoomName.Text = lblRoomBooked.Text;
            lblBillExtraGuestCharge.Text = lblExtraGuest.Text;
            lblBillExtraBedCharge.Text = lblExtraBed.Text;
            lblBillRoomCharge.Text = lblBookingRoomCharge.Text;
            lblTotalRoomCharge.Text = Convert.ToString(myBillings.TotalRoomCharge);

            
           
            //add the billing
            myBillings.AddBilling();
            DGVBillings.Refresh();

            panel3.Visible = true;
            lblBilling.Visible = true;
            Guests.Visible = false;
            btnbook.Enabled = false;

        
        }

        private void DGVBillings_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
         
            //display billing info on form
            lblBillingGuest.Text = DGVBillings.Rows[e.RowIndex].Cells[2].Value.ToString();
            lblBillingRoom.Text = DGVBillings.Rows[e.RowIndex].Cells[4].Value.ToString();
            lblBillingWifi.Text = DGVBillings.Rows[e.RowIndex].Cells[7].Value.ToString();
            lblBillingBar.Text = DGVBillings.Rows[e.RowIndex].Cells[5].Value.ToString();
            lblBillingExtraBedCharge.Text = DGVBillings.Rows[e.RowIndex].Cells[7].Value.ToString();
            lblBillingTotalCharge.Text = DGVBillings.Rows[e.RowIndex].Cells[9].Value.ToString();
            lblBillingGuestCharge.Text = DGVBillings.Rows[e.RowIndex].Cells[8].Value.ToString();
        }

        private void btnCharge_Click(object sender, EventArgs e)
        {
            //charge guest bill
            MessageBox.Show ("Guest Has Been Charged");
            Guests.Visible = true;
            panel3.Visible = false;
            btnbook.Enabled = true;
        }

        private void btnbook_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;
            Guests.Visible = false;
            
            //fill select box
            CBXGuest.DataSource = myGuests.AllGuests();
     
            
            CBXGuest.DisplayMember = "Name";
            CBXGuest.ValueMember = "GuestID";
            CBXGuest.SelectedValue = "GuestID";
          
           
           
            //  CBXGuest.DataSource = myGuests.GuestsName();
            CBXRoom.DataSource = myRoom.AllRooms();
            CBXRoom.ValueMember = "RoomTypeID";
            CBXRoom.DisplayMember = "RoomTypeName";
            CBXRoom.SelectedValue = "RoomTypeID";
        }

        private void btnCancelBooking_Click(object sender, EventArgs e)
        {
            //cancel booking
            panel4.Visible = false;
            Guests.Visible = true;
        }

        private void btnbillcancel_Click(object sender, EventArgs e)
        {
            //cancel billing
            panel3.Visible = false;
            Guests.Visible = true;
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            
            pictureBox1.Visible = false;
            btnEnter.Visible = false;
            Guests.Visible = true;
        }



    }
}
