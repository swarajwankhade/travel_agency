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
    public DataTable GetAirlines()
    {
        if (checkConsumer())
        {
            DataAccess db = new DataAccess();
            DataTable dt = new DataTable("airline");
            //dt = db.GetAirlines();
            dt.Columns.Add("airline_id", typeof(System.String));
            dt.Columns.Add("airline_name", typeof(System.String));
            foreach (DataRow adr in db.GetAirlines().Rows)
            {
                DataRow dr = dt.NewRow();
                dr["airline_id"] = adr["airline_id"];
                dr["airline_name"] = adr["airline_name"];
                dt.Rows.Add(dr);
            }
            return dt;
        }
        return null;
    }

    [WebMethod]
    [SoapDocumentMethod(Binding = "TestService")]
    [SoapHeader("consumer", Required = true)]
    public DataTable GetAirbus(int airlineId, string from, string to)
    {
        if (checkConsumer())
        {
            DataAccess db = new DataAccess();
            DataTable dt = new DataTable("airbus");
            dt.Columns.Add("airbus_id", typeof(System.String));
            dt.Columns.Add("airbus_code", typeof(System.String));

            foreach (DataRow adr in db.GetAirbus(airlineId, from, to).Rows)
            {
                DataRow dr = dt.NewRow();
                dr["airbus_id"] = adr["airbus_id"];
                dr["airbus_code"] = adr["airbus_code"];
                dt.Rows.Add(dr);
            }
            return dt;
        }
        return null;
    }
    
    [WebMethod]
    [SoapDocumentMethod(Binding = "TestService")]
    [SoapHeader("consumer", Required = true)]
    public int UpdateSlot(int bookId, int airbusId, int slotNumber, DateTime travelDate)
    {
        if (checkConsumer())
        {
            DataAccess db = new DataAccess();
            if ((db.GetBookedSlots(airbusId, slotNumber, travelDate).Rows.Count == 0))
            {
                return db.UpdateSlot(bookId, slotNumber, travelDate);
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

    [WebMethod]
    [SoapDocumentMethod(Binding = "TestService")]
    [SoapHeader("consumer", Required = true)]
    public DataTable GetSlots(int airbusId, DateTime travelDate)
    {
        if (checkConsumer())
        {
            DataAccess db = new DataAccess();
            DataTable dt = new DataTable("airbus_seats_available");
            //dt = db.GetAirlines();
            dt.Columns.Add("airbus_seats_id", typeof(System.String));
            dt.Columns.Add("airbus_seats_number", typeof(System.String));
            DataTable dtAirbus = db.GetAirbusById(airbusId);
            int count = int.Parse(dtAirbus.Rows[0]["total_seats"].ToString());
            for (int i = 1; i <= count; i++)
            {
                if ((db.GetBookedSlots(airbusId, i, travelDate).Rows.Count == 0))
                {
                    DataRow dr = dt.NewRow();
                    dr["airbus_seats_id"] = i;
                    dr["airbus_seats_number"] = i;
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
        return null;
    }

    [WebMethod]
    [SoapDocumentMethod(Binding = "TestService")]
    [SoapHeader("consumer", Required = true)]
    public DataTable GetBookedSlots(int airbusId)
    {
        if (checkConsumer())
        {
            DataAccess db = new DataAccess();
            DataTable dt = new DataTable("airbus_seats_available");
            //dt = db.GetAirlines();
            dt.Columns.Add("id", typeof(System.Int32));
            dt.Columns.Add("airbus_id", typeof(System.Int32));
            dt.Columns.Add("seat_number", typeof(System.Int32));
            dt.Columns.Add("travel_date", typeof(System.String));
            dt.Columns.Add("is_locked", typeof(System.String));
            dt.Columns.Add("passenger_ssn", typeof(System.String));
            dt.Columns.Add("booked_by", typeof(System.String));
            dt.Columns.Add("confirmed_by", typeof(System.String));
            dt.Columns.Add("airbus_code", typeof(System.String));
            dt.Columns.Add("paid_amount", typeof(System.Decimal));
            foreach (DataRow adr in db.GetBookedSlots(airbusId).Rows)
            {
                DataRow dr = dt.NewRow();
                dr["id"] = adr["id"];
                dr["airbus_id"] = adr["airbus_id"];
                dr["seat_number"] = adr["seat_number"];
                dr["travel_date"] = adr["travel_date"];
                dr["is_locked"] = adr["is_locked"];
                dr["passenger_ssn"] = adr["passenger_ssn"];
                dr["booked_by"] = adr["booked_by"];
                dr["confirmed_by"] = adr["confirmed_by"];
                dr["airbus_code"] = adr["airbus_code"];
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
    public int BookSlot(int airbusId, int seatNumber, DateTime travelDate, bool isLocked, string passengerSSN, string updatedUser)
    {
        if (checkConsumer())
        {
            DataAccess db = new DataAccess();
            return db.BookSlot(airbusId, seatNumber, travelDate, isLocked, passengerSSN, updatedUser);
        }
        return 0;
    }

    [WebMethod]
    [SoapDocumentMethod(Binding = "TestService")]
    public DataTable GetSlotsTest(int airbusId, DateTime travelDate)
    {
        //if (checkConsumer())
        //{
        DataAccess db = new DataAccess();
        DataTable dt = new DataTable("airbus_seats_available");
        //dt = db.GetAirlines();
        dt.Columns.Add("airbus_seats_id", typeof(System.String));
        dt.Columns.Add("airbus_seats_number", typeof(System.String));
        DataTable dtAirbus = db.GetAirbusById(airbusId);
        int count = int.Parse(dtAirbus.Rows[0]["total_seats"].ToString());
        for (int i = 1; i <= count; i++)
        {
            if ((db.GetBookedSlots(airbusId, i, travelDate).Rows.Count == 0))
            {
                DataRow dr = dt.NewRow();
                dr["airbus_seats_id"] = i;
                dr["airbus_seats_number"] = i;
                dt.Rows.Add(dr);
            }
        }
        return dt;
        //}
        //return null;
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

