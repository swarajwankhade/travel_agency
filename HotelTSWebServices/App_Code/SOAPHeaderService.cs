using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using AirwayTSWebService;
using System.Data;

/// <summary>
/// Summary description for SOAPHeaderService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(Name = "TestService", ConformsTo = WsiProfiles.BasicProfile1_1)]
[Serializable]
public class SOAPHeaderService : System.Web.Services.WebService
{
    // Visual studio will append a "UserCredentialsValue" property to the proxy class
    public UserCredentials consumer;

    public SOAPHeaderService()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    [SoapDocumentMethod(Binding = "TestService")]
    [SoapHeader("consumer", Required = true)]
    public DataTable GetHotels()
    {
        if (checkConsumer())
        {
            DataAccess db = new DataAccess();
            DataTable dt = new DataTable("hotels");
            //dt = db.GetAirlines();
            dt.Columns.Add("hotel_id", typeof(System.String));
            dt.Columns.Add("hotel_name", typeof(System.String));
            foreach (DataRow adr in db.GetHotels().Rows)
            {
                DataRow dr = dt.NewRow();
                dr["hotel_id"] = adr["id"];
                dr["hotel_name"] = adr["hotel_name"];
                dt.Rows.Add(dr);
            }
            return dt;
        }
        return null;
    }

    [WebMethod]
    [SoapDocumentMethod(Binding = "TestService")]
    [SoapHeader("consumer", Required = true)]
    public DataTable GetRoom(int hotelId)
    {
        if (checkConsumer())
        {
            DataAccess db = new DataAccess();
            DataTable dt = new DataTable("room");
            dt.Columns.Add("room_id", typeof(System.String));
            dt.Columns.Add("room_number", typeof(System.String));

            foreach (DataRow adr in db.GetRooms(hotelId).Rows)
            {
                DataRow dr = dt.NewRow();
                dr["room_id"] = adr["room_id"];
                dr["room_number"] = adr["room_number"].ToString();
                dt.Rows.Add(dr);
            }
            return dt;
        }
        return null;
    }

    [WebMethod]
    [SoapDocumentMethod(Binding = "TestService")]
    [SoapHeader("consumer", Required = true)]
    public DataTable GetAvailableRooms(int hotelId, int roomId, DateTime travelDate)
    {
        if (checkConsumer())
        {
            DataAccess db = new DataAccess();
            DataTable dt = new DataTable("room_available");
            //dt = db.GetAirlines();
            dt.Columns.Add("room_id", typeof(System.String));
            dt.Columns.Add("room_number", typeof(System.String));
            foreach (DataRow adr in db.GetAvailableRooms(hotelId, roomId, travelDate).Rows)
            {
                DataRow dr = dt.NewRow();
                dr["room_id"] = adr["room_id"];
                dr["room_number"] = adr["room_number"].ToString();
                dt.Rows.Add(dr);
            }
            return dt;
        }
        return null;
    }

    [WebMethod]
    [SoapDocumentMethod(Binding = "TestService")]
    [SoapHeader("consumer", Required = true)]
    public int BookRoom(int roomId,string roomNumber,int hotelId, DateTime travelDate,bool isLocked,string passengerSSN, string updatedUser)
    {
        if (checkConsumer())
        {
            DataAccess db = new DataAccess();
            return db.BookRoom(roomId, roomNumber, hotelId, travelDate, isLocked, passengerSSN, updatedUser);
        }
        return 0;
    }

    [WebMethod]
    [SoapDocumentMethod(Binding = "TestService")]
    [SoapHeader("consumer", Required = true)]
    public DataTable GetBookedRooms(int roomId, DateTime travelDate)
    {
        if (checkConsumer())
        {
            DataAccess db = new DataAccess();
            DataTable dt = new DataTable("booked_rooms");
            //dt = db.GetAirlines();
            dt.Columns.Add("room_booking_id", typeof(System.Int32));
            dt.Columns.Add("room_id", typeof(System.Int32));
            dt.Columns.Add("book_date", typeof(System.String));
            dt.Columns.Add("room_number", typeof(System.String));
            dt.Columns.Add("is_locked", typeof(System.String));
            dt.Columns.Add("passenger_ssn", typeof(System.String));
            dt.Columns.Add("booked_by", typeof(System.String));
            dt.Columns.Add("confirmed_by", typeof(System.String));
            dt.Columns.Add("paid_amount", typeof(System.Decimal));
            foreach (DataRow adr in db.GetBookedRooms(roomId, travelDate).Rows)
            {
                DataRow dr = dt.NewRow();
                dr["room_booking_id"] = adr["room_booking_id"];
                dr["room_id"] = adr["room_id"];
                dr["book_date"] = adr["book_date"];
                dr["room_number"] = adr["room_number"];
                dr["is_locked"] = adr["is_locked"];
                dr["passenger_ssn"] = adr["passenger_ssn"];
                dr["booked_by"] = adr["booked_by"];
                dr["confirmed_by"] = adr["confirmed_by"];
                dr["paid_amount"] = adr["paid_amount"];
                dt.Rows.Add(dr);
            }
            return dt;
        }
        return null;
    }

    [WebMethod]
    [SoapDocumentMethod(Binding = "TestService")]
    [SoapHeader("consumer", Required = true)]
    public int UpdateSlot(int bookId, int roomId, int hotelId, DateTime travelDate)
    {
        if (checkConsumer())
        {
            DataAccess db = new DataAccess();
            if ((db.GetBookedRoomsByDate(hotelId, roomId, travelDate).Rows.Count == 0))
            {
                return db.UpdateSlot(bookId, roomId, travelDate);
            }
        }
        return 0;
    }

    [WebMethod]
    [SoapDocumentMethod(Binding = "TestService")]
    [SoapHeader("consumer", Required = true)]
    public int ConfirmSlot(int bId, string userName, decimal paidAmount)
    {
        if (checkConsumer())
        {
            DataAccess db = new DataAccess();
            return db.ConfirmSlot(bId, userName, paidAmount);
        }
        return 0;
    }

    [WebMethod]
    [SoapDocumentMethod(Binding = "TestService")]
    [SoapHeader("consumer", Required = true)]
    public int DeleteSlot(int bookId)
    {
        if (checkConsumer())
        {
            DataAccess db = new DataAccess();
            return db.DeleteSlot(bookId);
        }
        return 0;
    }
       
    private bool checkConsumer()
    {
        // In this method you can check the username and password 
        // with your database or something
        // You could also encrypt the password for more security
        if (consumer != null)
        {
            DataAccess db = new DataAccess();

            //if (consumer.userName == "Ahmed" && consumer.password == "1234")
            if (db.CheckUser(consumer.userName, consumer.password))
                return true;
            else
                return false;
        }
        else
            return false;

    }

}

# region "SOAP Headers"

public class UserCredentials : System.Web.Services.Protocols.SoapHeader
{
    public string userName;
    public string password;
}

# endregion

