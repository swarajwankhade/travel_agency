using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;
using System.Data;

namespace TravelAgency
{
    public partial class BookingTransaction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ClearText();
        }
        protected void ddlAirbus_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            AirbusBinding();
        }
        protected void ddlBookingType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBookingType.SelectedValue == "Select")
            {
                divAirlines.Visible = false;
                divHotel.Visible = false;
                divCar.Visible = false;
            }
            else if (ddlBookingType.SelectedValue == "All")
            {
                divAirlines.Visible = true;
                divHotel.Visible = true;
                divCar.Visible = true;
                AirlinesDropdownBinding();
                CarRentalDDlBindings();
            }
            else if (ddlBookingType.SelectedValue == "Airways")
            {
                divAirlines.Visible = true;
                divHotel.Visible = false;
                divCar.Visible = false;
                AirlinesDropdownBinding();
            }
            else if (ddlBookingType.SelectedValue == "Hotel")
            {
                divAirlines.Visible = false;
                divHotel.Visible = true;
                divCar.Visible = false;
                HotelDDlBindings();
            }
            else if (ddlBookingType.SelectedValue == "Car")
            {
                divAirlines.Visible = false;
                divHotel.Visible = false;
                divCar.Visible = true;
                CarRentalDDlBindings();
            }


        }

        #region Airlines
        private void AirlinesDropdownBinding()
        {
            AirlineBinding();
            AirbusBinding();
        }

        private void AirlineBinding()
        {
            if (Session["LoggedInUser"] != null)
            {
                AirlineBL abl = new AirlineBL();
                DataTable dt = (DataTable)Session["LoggedInUser"];
                ddlAirlines.DataSource = abl.GetAirlines(dt.Rows[0]["airbus_user_name"].ToString(), dt.Rows[0]["airbus_password"].ToString());
                ddlAirlines.DataValueField = "airline_id";
                ddlAirlines.DataTextField = "airline_name";
                ddlAirlines.DataBind();
            }
        }

        private void AirbusBinding()
        {
            if (Session["LoggedInUser"] != null)
            {
                AirlineBL abl = new AirlineBL();
                DataTable dt = (DataTable)Session["LoggedInUser"];
                ddlAirbus.DataSource = abl.GetAirbus(dt.Rows[0]["airbus_user_name"].ToString(), dt.Rows[0]["airbus_password"].ToString(), int.Parse(ddlAirlines.SelectedValue), ddlAirbusFrom.SelectedValue, ddlAirbusTo.SelectedValue);
                ddlAirbus.DataValueField = "airbus_id";
                ddlAirbus.DataTextField = "airbus_code";
                ddlAirbus.DataBind();
            }
        }

        protected void btnAirlineBookedSearch_OnClick(object sender, EventArgs e)
        {
            if (Session["LoggedInUser"] != null)
            {
                if (ddlAirbus.SelectedValue != null)
                {
                    AirlineBL abl = new AirlineBL();
                    DataTable dt = (DataTable)Session["LoggedInUser"];
                    ddlAvailableSeatNumbers.DataSource = abl.GetSlots(dt.Rows[0]["airbus_user_name"].ToString(), dt.Rows[0]["airbus_password"].ToString(), int.Parse(ddlAirbus.SelectedValue), DateTime.Parse(txtTravelDate.Text));
                    ddlAvailableSeatNumbers.DataValueField = "airbus_seats_id";
                    ddlAvailableSeatNumbers.DataTextField = "airbus_seats_number";
                    ddlAvailableSeatNumbers.DataBind();
                }
            }
        }

        private void Clear()
        {
            AirlineBL abl = new AirlineBL();
            DataTable dt = (DataTable)Session["LoggedInUser"];
            ddlAvailableSeatNumbers.DataSource = abl.GetSlots(dt.Rows[0]["airbus_user_name"].ToString(), dt.Rows[0]["airbus_password"].ToString(), 0, DateTime.Parse(txtTravelDate.Text));
            ddlAvailableSeatNumbers.DataValueField = "airbus_seats_id";
            ddlAvailableSeatNumbers.DataTextField = "airbus_seats_number";
            ddlAvailableSeatNumbers.DataBind();

            gvAirlineBookedSeats.DataSource = abl.GetBookedSlots(dt.Rows[0]["airbus_user_name"].ToString(), dt.Rows[0]["airbus_password"].ToString(), 0);
            gvAirlineBookedSeats.DataBind();
        }

        private void ClearText()
        {
            lblTicketNumber.Text = "";
            lblBError.Text = "";
        }
        protected void btnAirbusSeatBook_OnClick(object sender, EventArgs e)
        {

            if (Session["LoggedInUser"] != null)
            {
                if (ddlAirbus.SelectedValue != null)
                {
                    AirlineBL abl = new AirlineBL();
                    DataTable dt = (DataTable)Session["LoggedInUser"];
                    lblTicketNumber.Text = "Booking Refeence Number" + abl.BookSlot(dt.Rows[0]["airbus_user_name"].ToString(), dt.Rows[0]["airbus_password"].ToString(), int.Parse(ddlAirbus.SelectedValue), int.Parse(ddlAvailableSeatNumbers.SelectedValue), DateTime.Parse(txtTravelDate.Text), false, txtSSN.Text, dt.Rows[0]["airbus_user_name"].ToString()).ToString();
                    btnAirlineBookedSearch_OnClick(null, null);
                    btnAlreadyBookedTickets_OnClick(null, null);
                }
            }
        }

        protected void btnAlreadyBookedTickets_OnClick(object sender, EventArgs e)
        {
            if (Session["LoggedInUser"] != null)
            {
                if (ddlAirbus.SelectedValue != null)
                {
                    AirlineBL abl = new AirlineBL();
                    DataTable dt = (DataTable)Session["LoggedInUser"];
                    gvAirlineBookedSeats.DataSource = abl.GetBookedSlots(dt.Rows[0]["airbus_user_name"].ToString(), dt.Rows[0]["airbus_password"].ToString(), int.Parse(ddlAirbus.SelectedValue));
                    gvAirlineBookedSeats.DataBind();
                }
            }
        }

        protected void ddlAirlines_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            AirbusBinding();
            Clear();
        }

        protected void gvAirlineBookedSeats_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName.Equals("Update Ticket") && e.CommandArgument != null)
            {
                if (txtTravelDate.Text.Length == 0)
                {
                    lblBError.Text = "Please select date.";
                    return;
                }
                if (ddlAvailableSeatNumbers.SelectedValue == null || ddlAvailableSeatNumbers.SelectedValue == "")
                {
                    lblBError.Text = "Please select slot number.";
                    return;
                }
                if (ddlAirbus.SelectedValue == null || ddlAirbus.SelectedValue == "")
                {
                    lblBError.Text = "Please select Airbus.";
                    return;
                }
                AirlineBL abl = new AirlineBL();
                DataTable dt = (DataTable)Session["LoggedInUser"];

                abl.UpdateSlot(dt.Rows[0]["airbus_user_name"].ToString(), dt.Rows[0]["airbus_password"].ToString(), id, int.Parse(ddlAirbus.SelectedValue), int.Parse(ddlAvailableSeatNumbers.SelectedValue), DateTime.Parse(txtTravelDate.Text));
            }
            else if (e.CommandName.Equals("Delete Ticket") && e.CommandArgument != null)
            {
                AirlineBL abl = new AirlineBL();
                DataTable dt = (DataTable)Session["LoggedInUser"];
                abl.DeleteSlot(dt.Rows[0]["airbus_user_name"].ToString(), dt.Rows[0]["airbus_password"].ToString(), id);
            }
            else if (e.CommandName.Equals("Confirm") && e.CommandArgument != null)
            {
                if (txtPaidAmount.Text.Length == 0)
                {
                    lblBError.Text = "Please enter amount.";
                    return;
                }
                int amt = 0;
                int.TryParse(txtPaidAmount.Text, out amt);
                if (amt == 0)
                {
                    lblBError.Text = "Please enter correct amount.";
                    return;
                }
                AirlineBL abl = new AirlineBL();
                DataTable dt = (DataTable)Session["LoggedInUser"];
                abl.ConfirmSlot(dt.Rows[0]["airbus_user_name"].ToString(), dt.Rows[0]["airbus_password"].ToString(), id, int.Parse(ddlAvailableSeatNumbers.SelectedValue), Decimal.Parse(txtPaidAmount.Text));
            }
            btnAlreadyBookedTickets_OnClick(null, null);
        }

        protected void gvAirlineBookedSeats_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dt = (DataTable)Session["LoggedInUser"];
                if (int.Parse(dt.Rows[0]["user_role_id"].ToString()) != 1)
                {
                    ImageButton ib = (ImageButton)e.Row.FindControl("imgBtnDelete");
                    ib.Visible = false;
                }

                HiddenField hdnIsEditable = (HiddenField)e.Row.FindControl("hdnIsEditable");
                if (bool.Parse(hdnIsEditable.Value))
                {
                    ImageButton ib = (ImageButton)e.Row.FindControl("imgBtnEdit");
                    ib.Visible = false;
                    ImageButton ib1 = (ImageButton)e.Row.FindControl("imgBtnConfirm");
                    ib1.Visible = false;
                    ImageButton ib2 = (ImageButton)e.Row.FindControl("imgBtnDelete");
                    ib2.Visible = false;
                }
                
            }
        }

        protected void gvAirlineBookedSeats_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvAirlineBookedSeats.PageIndex = e.NewPageIndex;
                btnAlreadyBookedTickets_OnClick(null, null);
            }
            catch (Exception ex)
            {

            }
        }

        
        #endregion

        #region CarRentals

        private void CarRentalBinding()
        {
            if (Session["LoggedInUser"] != null)
            {
                CarRentalBL cbl = new CarRentalBL();
                DataTable dt = (DataTable)Session["LoggedInUser"];
                ddlCarRentals.DataSource = cbl.GetCarRentals(dt.Rows[0]["car_user_name"].ToString(), dt.Rows[0]["car_password"].ToString());
                ddlCarRentals.DataValueField = "car_rental_id";
                ddlCarRentals.DataTextField = "car_rental_name";
                ddlCarRentals.DataBind();
            }
        }

        private void CarBinding()
        {
            if (Session["LoggedInUser"] != null)
            {
                if (ddlCarRentals.Items.Count > 0 && ddlCarRentals.SelectedValue != null && ddlCarRentals.SelectedValue != "")
                {
                    CarRentalBL cbl = new CarRentalBL();
                    DataTable dt = (DataTable)Session["LoggedInUser"];
                    ddlCars.DataSource = cbl.GetCars(dt.Rows[0]["car_user_name"].ToString(), dt.Rows[0]["car_password"].ToString(), int.Parse(ddlCarRentals.SelectedValue));
                    ddlCars.DataValueField = "id";
                    ddlCars.DataTextField = "car_number";
                    ddlCars.DataBind();
                }
            }
        }

        private void CarRentalDDlBindings()
        {
            CarRentalBinding();
            CarBinding();
        }

        protected void ddlCarRentals_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ClearCar();
            CarBinding();
        }

        protected void ddlCars_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ClearCar();
            //CarBinding();
        }


        protected void btnAlreadyBookedCars_OnClick(object sender, EventArgs e)
        {
            if (Session["LoggedInUser"] != null)
            {
                if (ddlAirbus.SelectedValue != null)
                {
                    CarRentalBL abl = new CarRentalBL();
                    DataTable dt = (DataTable)Session["LoggedInUser"];
                    gvBookedCars.DataSource = abl.GetBookedCars(dt.Rows[0]["car_user_name"].ToString(), dt.Rows[0]["car_password"].ToString(), int.Parse(ddlCars.SelectedValue), DateTime.Parse(txtCarBookDate.Text));
                    gvBookedCars.DataBind();
                }
            }
        }

        protected void btnCarBook_OnClick(object sender, EventArgs e)
        {

            if (Session["LoggedInUser"] != null)
            {
                if (ddlAirbus.SelectedValue != null)
                {
                    CarRentalBL cbl = new CarRentalBL();
                    DataTable dt = (DataTable)Session["LoggedInUser"];
                    lblTicketNumber.Text = "Booking Refeence Number" + cbl.BookCar(dt.Rows[0]["car_user_name"].ToString(), dt.Rows[0]["car_password"].ToString(), int.Parse(ddlCars.SelectedValue), ddlCars.SelectedItem.Text.Split("-".ToCharArray()).GetValue(0).ToString(), DateTime.Parse(txtCarBookDate.Text), false, txtPassSSNCar.Text, dt.Rows[0]["car_user_name"].ToString()).ToString();
                    btnCarBookedSearch_OnClick(null, null);
                    btnAlreadyBookedCars_OnClick(null, null);
                }
            }
        }

        protected void btnCarBookedSearch_OnClick(object sender, EventArgs e)
        {
            if (Session["LoggedInUser"] != null)
            {
                if (ddlAirbus.SelectedValue != null)
                {
                    CarRentalBL cbl = new CarRentalBL();
                    DataTable dt = (DataTable)Session["LoggedInUser"];
                    ddlAvailableCars.DataSource = cbl.GetAvailableCars(dt.Rows[0]["car_user_name"].ToString(), dt.Rows[0]["car_password"].ToString(), int.Parse(ddlCars.SelectedValue), DateTime.Parse(txtCarBookDate.Text));
                    ddlAvailableCars.DataValueField = "id";
                    ddlAvailableCars.DataTextField = "car_number";
                    ddlAvailableCars.DataBind();
                }
            }
        }

        protected void gvBookedCars_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName.Equals("Update CarBId") && e.CommandArgument != null)
            {
                if (txtCarBookDate.Text.Length == 0)
                {
                    lblCarError.Text = "Please select date.";
                    return;
                }
                if (ddlAvailableCars.SelectedValue == null || ddlAvailableCars.SelectedValue == "")
                {
                    lblCarError.Text = "Please select Available Car.";
                    return;
                }
                if (ddlCars.SelectedValue == null || ddlCars.SelectedValue == "")
                {
                    lblCarError.Text = "Please select Car.";
                    return;
                }
                CarRentalBL abl = new CarRentalBL();
                DataTable dt = (DataTable)Session["LoggedInUser"];

                abl.UpdateSlot(dt.Rows[0]["car_user_name"].ToString(), dt.Rows[0]["car_password"].ToString(), id, int.Parse(ddlAvailableCars.SelectedValue), DateTime.Parse(txtCarBookDate.Text));
            }
            else if (e.CommandName.Equals("Delete CarBId") && e.CommandArgument != null)
            {
                CarRentalBL abl = new CarRentalBL();
                DataTable dt = (DataTable)Session["LoggedInUser"];
                abl.DeleteSlot(dt.Rows[0]["car_user_name"].ToString(), dt.Rows[0]["car_password"].ToString(), id);
            }
            else if (e.CommandName.Equals("Confirm") && e.CommandArgument != null)
            {
                if (txtCarAmountPaid.Text.Length == 0)
                {
                    lblCarError.Text = "Please enter amount.";
                    return;
                }
                int amt = 0;
                int.TryParse(txtCarAmountPaid.Text, out amt);
                if (amt == 0)
                {
                    lblCarError.Text = "Please enter correct amount.";
                    return;
                }
                CarRentalBL abl = new CarRentalBL();
                DataTable dt = (DataTable)Session["LoggedInUser"];
                abl.ConfirmSlot(dt.Rows[0]["car_user_name"].ToString(), dt.Rows[0]["car_password"].ToString(), id, Decimal.Parse(txtCarAmountPaid.Text));
            }
            btnAlreadyBookedCars_OnClick(null, null);
        }

        protected void gvBookedCars_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dt = (DataTable)Session["LoggedInUser"];
                if (int.Parse(dt.Rows[0]["user_role_id"].ToString()) != 1)
                {
                    ImageButton ib = (ImageButton)e.Row.FindControl("imgBtnDelete");
                    ib.Visible = false;
                }

                HiddenField hdnIsEditable = (HiddenField)e.Row.FindControl("hdnIsEditable");
                if (bool.Parse(hdnIsEditable.Value))
                {
                    ImageButton ib = (ImageButton)e.Row.FindControl("imgBtnEdit");
                    ib.Visible = false;
                    ImageButton ib1 = (ImageButton)e.Row.FindControl("imgBtnConfirm");
                    ib1.Visible = false;
                    ImageButton ib2 = (ImageButton)e.Row.FindControl("imgBtnDelete");
                    ib2.Visible = false;
                }

            }
        }

        protected void gvBookedCars_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvBookedCars.PageIndex = e.NewPageIndex;
                btnAlreadyBookedCars_OnClick(null, null);
            }
            catch (Exception ex)
            {

            }
        }

        private void ClearCar()
        {
        }

        #endregion


        #region Hotels


        private void HotelBinding()
        {
            if (Session["LoggedInUser"] != null)
            {
                HotelBL cbl = new HotelBL();
                DataTable dt = (DataTable)Session["LoggedInUser"];
                ddlHotels.DataSource = cbl.GetHotels(dt.Rows[0]["hotel_user_name"].ToString(), dt.Rows[0]["hotel_password"].ToString());
                ddlHotels.DataValueField = "hotel_id";
                ddlHotels.DataTextField = "hotel_name";
                ddlHotels.DataBind();
            }
        }

        private void RoomBinding()
        {
            if (Session["LoggedInUser"] != null)
            {
                if (ddlHotels.Items.Count > 0 && ddlHotels.SelectedValue != null && ddlHotels.SelectedValue != "")
                {
                    HotelBL cbl = new HotelBL();
                    DataTable dt = (DataTable)Session["LoggedInUser"];
                    ddlRooms.DataSource = cbl.GetRooms(dt.Rows[0]["hotel_user_name"].ToString(), dt.Rows[0]["hotel_password"].ToString(), int.Parse(ddlHotels.SelectedValue));
                    ddlRooms.DataValueField = "room_id";
                    ddlRooms.DataTextField = "room_number";
                    ddlRooms.DataBind();
                }
            }
        }

        private void HotelDDlBindings()
        {
            HotelBinding();
            RoomBinding();
        }

        protected void ddlHotels_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ClearRoom();
            RoomBinding();
        }

        protected void ddlRooms_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ClearRoom();
            //RoomBinding();
        }


        protected void btnAlreadyBookedRooms_OnClick(object sender, EventArgs e)
        {
            if (Session["LoggedInUser"] != null)
            {
                if (ddlAirbus.SelectedValue != null)
                {
                    HotelBL abl = new HotelBL();
                    DataTable dt = (DataTable)Session["LoggedInUser"];
                    gvBookedRooms.DataSource = abl.GetBookedRooms(dt.Rows[0]["hotel_user_name"].ToString(), dt.Rows[0]["hotel_password"].ToString(), int.Parse(ddlRooms.SelectedValue), DateTime.Parse(txtRoomBookDate.Text));
                    gvBookedRooms.DataBind();
                }
            }
        }

        protected void btnRoomBook_OnClick(object sender, EventArgs e)
        {

            if (Session["LoggedInUser"] != null)
            {
                if (ddlAirbus.SelectedValue != null)
                {
                    HotelBL cbl = new HotelBL();
                    DataTable dt = (DataTable)Session["LoggedInUser"];
                    lblTicketNumber.Text = "Booking Refeence Number" + cbl.BookRoom(dt.Rows[0]["hotel_user_name"].ToString(), dt.Rows[0]["hotel_password"].ToString(), int.Parse(ddlAvailableRooms.SelectedValue), ddlAvailableRooms.SelectedItem.Text, int.Parse(ddlHotels.SelectedValue), DateTime.Parse(txtRoomBookDate.Text), false, txtPassSSNRoom.Text, dt.Rows[0]["hotel_user_name"].ToString()).ToString();
                    btnRoomBookedSearch_OnClick(null, null);
                    btnAlreadyBookedRooms_OnClick(null, null);
                }
            }
        }

        protected void btnRoomBookedSearch_OnClick(object sender, EventArgs e)
        {
            if (Session["LoggedInUser"] != null)
            {
                if (ddlAirbus.SelectedValue != null)
                {
                    HotelBL cbl = new HotelBL();
                    DataTable dt = (DataTable)Session["LoggedInUser"];
                    ddlAvailableRooms.DataSource = cbl.GetAvailableRooms(dt.Rows[0]["hotel_user_name"].ToString(), dt.Rows[0]["hotel_password"].ToString(), int.Parse(ddlHotels.SelectedValue), int.Parse(ddlRooms.SelectedValue), DateTime.Parse(txtRoomBookDate.Text));
                    ddlAvailableRooms.DataValueField = "room_id";
                    ddlAvailableRooms.DataTextField = "room_number";
                    ddlAvailableRooms.DataBind();
                }
            }
        }

        protected void gvBookedRooms_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName.Equals("Update RoomBId") && e.CommandArgument != null)
            {
                if (txtRoomBookDate.Text.Length == 0)
                {
                    lblRoomError.Text = "Please select date.";
                    return;
                }
                if (ddlHotels.SelectedValue == null || ddlHotels.SelectedValue == "")
                {
                    lblRoomError.Text = "Please select Hotel.";
                    return;
                }
                if (ddlAvailableRooms.SelectedValue == null || ddlAvailableRooms.SelectedValue == "")
                {
                    lblRoomError.Text = "Please select Available Room.";
                    return;
                }
                if (ddlRooms.SelectedValue == null || ddlRooms.SelectedValue == "")
                {
                    lblRoomError.Text = "Please select Room.";
                    return;
                }
                HotelBL abl = new HotelBL();
                DataTable dt = (DataTable)Session["LoggedInUser"];

                abl.UpdateSlot(dt.Rows[0]["hotel_user_name"].ToString(), dt.Rows[0]["hotel_password"].ToString(), id, int.Parse(ddlHotels.SelectedValue),int.Parse(ddlAvailableRooms.SelectedValue), DateTime.Parse(txtRoomBookDate.Text));
            }
            else if (e.CommandName.Equals("Delete RoomBId") && e.CommandArgument != null)
            {
                HotelBL abl = new HotelBL();
                DataTable dt = (DataTable)Session["LoggedInUser"];
                abl.DeleteSlot(dt.Rows[0]["hotel_user_name"].ToString(), dt.Rows[0]["hotel_password"].ToString(), id);
            }
            else if (e.CommandName.Equals("Confirm") && e.CommandArgument != null)
            {
                if (txtRoomAmountPaid.Text.Length == 0)
                {
                    lblRoomError.Text = "Please enter amount.";
                    return;
                }
                int amt = 0;
                int.TryParse(txtRoomAmountPaid.Text, out amt);
                if (amt == 0)
                {
                    lblRoomError.Text = "Please enter correct amount.";
                    return;
                }
                HotelBL abl = new HotelBL();
                DataTable dt = (DataTable)Session["LoggedInUser"];
                abl.ConfirmSlot(dt.Rows[0]["hotel_user_name"].ToString(), dt.Rows[0]["hotel_password"].ToString(), id, Decimal.Parse(txtRoomAmountPaid.Text));
            }
            btnAlreadyBookedRooms_OnClick(null, null);
        }

        protected void gvBookedRooms_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dt = (DataTable)Session["LoggedInUser"];
                if (int.Parse(dt.Rows[0]["user_role_id"].ToString()) != 1)
                {
                    ImageButton ib = (ImageButton)e.Row.FindControl("imgBtnDelete");
                    ib.Visible = false;
                }

                HiddenField hdnIsEditable = (HiddenField)e.Row.FindControl("hdnIsEditable");
                if (bool.Parse(hdnIsEditable.Value))
                {
                    ImageButton ib = (ImageButton)e.Row.FindControl("imgBtnEdit");
                    ib.Visible = false;
                    ImageButton ib1 = (ImageButton)e.Row.FindControl("imgBtnConfirm");
                    ib1.Visible = false;
                    ImageButton ib2 = (ImageButton)e.Row.FindControl("imgBtnDelete");
                    ib2.Visible = false;
                }

            }
        }

        protected void gvBookedRooms_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvBookedRooms.PageIndex = e.NewPageIndex;
                btnAlreadyBookedRooms_OnClick(null, null);
            }
            catch (Exception ex)
            {

            }
        }

        private void ClearRoom()
        {
        }

        #endregion

    }
}
