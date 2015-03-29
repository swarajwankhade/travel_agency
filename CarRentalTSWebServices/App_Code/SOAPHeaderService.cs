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
    public DataTable GetCarRentals()
    {
        if (checkConsumer())
        {
            DataAccess db = new DataAccess();
            DataTable dt = new DataTable("car_rentals");
            //dt = db.GetAirlines();
            dt.Columns.Add("car_rental_id", typeof(System.String));
            dt.Columns.Add("car_rental_name", typeof(System.String));
            foreach (DataRow adr in db.GetCarRentals().Rows)
            {
                DataRow dr = dt.NewRow();
                dr["car_rental_id"] = adr["car_rental_id"];
                dr["car_rental_name"] = adr["car_rental_name"];
                dt.Rows.Add(dr);
            }
            return dt;
        }
        return null;
    }

    [WebMethod]
    [SoapDocumentMethod(Binding = "TestService")]
    [SoapHeader("consumer", Required = true)]
    public DataTable GetCar(int carRentalId)
    {
        if (checkConsumer())
        {
            DataAccess db = new DataAccess();
            DataTable dt = new DataTable("car");
            dt.Columns.Add("id", typeof(System.String));
            dt.Columns.Add("car_number", typeof(System.String));

            foreach (DataRow adr in db.GetCars(carRentalId).Rows)
            {
                DataRow dr = dt.NewRow();
                dr["id"] = adr["id"];
                dr["car_number"] = adr["car_number"].ToString() + "-" + adr["car_model"].ToString();
                dt.Rows.Add(dr);
            }
            return dt;
        }
        return null;
    }

    [WebMethod]
    [SoapDocumentMethod(Binding = "TestService")]
    [SoapHeader("consumer", Required = true)]
    public DataTable GetAvailableCars(int carRentalId, DateTime travelDate)
    {
        if (checkConsumer())
        {
            DataAccess db = new DataAccess();
            DataTable dt = new DataTable("car_available");
            //dt = db.GetAirlines();
            dt.Columns.Add("id", typeof(System.String));
            dt.Columns.Add("car_number", typeof(System.String));
            foreach (DataRow adr in db.GetAvailableCars(carRentalId, travelDate).Rows)
            {
                DataRow dr = dt.NewRow();
                dr["id"] = adr["id"];
                dr["car_number"] = adr["car_number"].ToString() + "-" + adr["car_model"].ToString();
                dt.Rows.Add(dr);
            }
            return dt;
        }
        return null;
    }

    [WebMethod]
    [SoapDocumentMethod(Binding = "TestService")]
    [SoapHeader("consumer", Required = true)]
    public int BookCar(int carId,string carNumber, DateTime travelDate,bool isLocked,string passengerSSN, string updatedUser)
    {
        if (checkConsumer())
        {
            DataAccess db = new DataAccess();
            return db.BookCar(carId, carNumber, travelDate, isLocked, passengerSSN, updatedUser);
        }
        return 0;
    }

    [WebMethod]
    [SoapDocumentMethod(Binding = "TestService")]
    [SoapHeader("consumer", Required = true)]
    public DataTable GetBookedCars(int carId, DateTime travelDate)
    {
        if (checkConsumer())
        {
            DataAccess db = new DataAccess();
            DataTable dt = new DataTable("booked_cars");
            //dt = db.GetAirlines();
            dt.Columns.Add("car_booking_id", typeof(System.Int32));
            dt.Columns.Add("car_id", typeof(System.Int32));
            dt.Columns.Add("book_date", typeof(System.String));
            dt.Columns.Add("car_number", typeof(System.String));
            dt.Columns.Add("is_locked", typeof(System.String));
            dt.Columns.Add("passenger_ssn", typeof(System.String));
            dt.Columns.Add("booked_by", typeof(System.String));
            dt.Columns.Add("confirmed_by", typeof(System.String));
            dt.Columns.Add("paid_amount", typeof(System.Decimal));
            foreach (DataRow adr in db.GetBookedCars(carId, travelDate).Rows)
            {
                DataRow dr = dt.NewRow();
                dr["car_booking_id"] = adr["car_booking_id"];
                dr["car_id"] = adr["car_id"];
                dr["book_date"] = adr["book_date"];
                dr["car_number"] = adr["car_number"];
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
    public int UpdateSlot(int bookId, int carId, DateTime travelDate)
    {
        if (checkConsumer())
        {
            DataAccess db = new DataAccess();
            if ((db.GetBookedCarsByDate(carId, travelDate).Rows.Count == 0))
            {
                return db.UpdateSlot(bookId, carId, travelDate);
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

