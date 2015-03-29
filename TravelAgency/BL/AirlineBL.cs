using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BL
{
    public class AirlineBL
    {
        public DataTable GetAirlines(string userName, string password)
        {
            try
            {
                AirwayTSWebServices.SOAPHeaderService service = new AirwayTSWebServices.SOAPHeaderService();
                AirwayTSWebServices.UserCredentials user = new AirwayTSWebServices.UserCredentials();
                user.userName = userName;
                user.password = password;

                service.UserCredentialsValue = user;

                return service.GetAirlines();
            }
            catch (Exception ex)
            { throw ex; }
        }

        public DataTable GetAirbus(string userName, string password, int airlineId, string from, string to)
        {
            try
            {
                AirwayTSWebServices.SOAPHeaderService service = new AirwayTSWebServices.SOAPHeaderService();
                AirwayTSWebServices.UserCredentials user = new AirwayTSWebServices.UserCredentials();
                user.userName = userName;
                user.password = password;

                service.UserCredentialsValue = user;

                return service.GetAirbus(airlineId, from, to);
            }
            catch (Exception ex)
            { throw ex; }
        }

        public DataTable GetSlots(string userName, string password,int airbusId, DateTime travelDate)
        {
            try
            {
                AirwayTSWebServices.SOAPHeaderService service = new AirwayTSWebServices.SOAPHeaderService();
                AirwayTSWebServices.UserCredentials user = new AirwayTSWebServices.UserCredentials();
                user.userName = userName;
                user.password = password;

                service.UserCredentialsValue = user;

                return service.GetSlots(airbusId, travelDate);
            }
            catch (Exception ex)
            { throw ex; }
        }

        public DataTable GetBookedSlots(string userName, string password, int airbusId)
        {
            try
            {
                AirwayTSWebServices.SOAPHeaderService service = new AirwayTSWebServices.SOAPHeaderService();
                AirwayTSWebServices.UserCredentials user = new AirwayTSWebServices.UserCredentials();
                user.userName = userName;
                user.password = password;

                service.UserCredentialsValue = user;

                return service.GetBookedSlots(airbusId);
            }
            catch (Exception ex)
            { throw ex; }
        }

        public int BookSlot(string userName, string password, int airbusId,int seatNumber,  DateTime travelDate, bool isLocked, string passengerSSN, string updatedUser)
        {
            try
            {
                AirwayTSWebServices.SOAPHeaderService service = new AirwayTSWebServices.SOAPHeaderService();
                AirwayTSWebServices.UserCredentials user = new AirwayTSWebServices.UserCredentials();
                user.userName = userName;
                user.password = password;

                service.UserCredentialsValue = user;

                return service.BookSlot(airbusId, seatNumber, travelDate, isLocked, passengerSSN, updatedUser);
            }
            catch (Exception ex)
            { throw ex; }
        }

        public int UpdateSlot(string userName, string password, int bookId, int airbusId, int seatNumber, DateTime travelDate)
        {
            try
            {
                AirwayTSWebServices.SOAPHeaderService service = new AirwayTSWebServices.SOAPHeaderService();
                AirwayTSWebServices.UserCredentials user = new AirwayTSWebServices.UserCredentials();
                user.userName = userName;
                user.password = password;

                service.UserCredentialsValue = user;

                return service.UpdateSlot(bookId, airbusId, seatNumber, travelDate);
            }
            catch (Exception ex)
            { throw ex; }
        }

        public int ConfirmSlot(string userName, string password, int bId, int seatNumber, decimal paidAmount)
        {
            try
            {
                AirwayTSWebServices.SOAPHeaderService service = new AirwayTSWebServices.SOAPHeaderService();
                AirwayTSWebServices.UserCredentials user = new AirwayTSWebServices.UserCredentials();
                user.userName = userName;
                user.password = password;

                service.UserCredentialsValue = user;

                return service.ConfirmSlot(bId, userName, paidAmount);
            }
            catch (Exception ex)
            { throw ex; }
        }

        public int DeleteSlot(string userName, string password, int bId)
        {
            try
            {
                AirwayTSWebServices.SOAPHeaderService service = new AirwayTSWebServices.SOAPHeaderService();
                AirwayTSWebServices.UserCredentials user = new AirwayTSWebServices.UserCredentials();
                user.userName = userName;
                user.password = password;

                service.UserCredentialsValue = user;

                return service.DeleteSlot(bId);
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}
