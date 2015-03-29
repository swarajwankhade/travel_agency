using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BL
{
    public class HotelBL
    {
        public DataTable GetHotels(string userName, string password)
        {
            try
            {
                HotelTSWebServices.SOAPHeaderService service = new HotelTSWebServices.SOAPHeaderService();
                HotelTSWebServices.UserCredentials user = new HotelTSWebServices.UserCredentials();
                user.userName = userName;
                user.password = password;

                service.UserCredentialsValue = user;

                return service.GetHotels();
            }
            catch (Exception ex)
            { throw ex; }
        }

        public DataTable GetRooms(string userName, string password, int hotelId)
        {
            try
            {
                HotelTSWebServices.SOAPHeaderService service = new HotelTSWebServices.SOAPHeaderService();
                HotelTSWebServices.UserCredentials user = new HotelTSWebServices.UserCredentials();
                user.userName = userName;
                user.password = password;

                service.UserCredentialsValue = user;

                return service.GetRoom(hotelId);
            }
            catch (Exception ex)
            { throw ex; }
        }

        public DataTable GetAvailableRooms(string userName, string password, int hotelId, int roomId, DateTime travelDate)
        {
            try
            {
                HotelTSWebServices.SOAPHeaderService service = new HotelTSWebServices.SOAPHeaderService();
                HotelTSWebServices.UserCredentials user = new HotelTSWebServices.UserCredentials();
                user.userName = userName;
                user.password = password;

                service.UserCredentialsValue = user;

                return service.GetAvailableRooms(hotelId, roomId, travelDate);
            }
            catch (Exception ex)
            { throw ex; }
        }

        public DataTable GetBookedRooms(string userName, string password, int roomId, DateTime travelDate)
        {
            try
            {
                HotelTSWebServices.SOAPHeaderService service = new HotelTSWebServices.SOAPHeaderService();
                HotelTSWebServices.UserCredentials user = new HotelTSWebServices.UserCredentials();
                user.userName = userName;
                user.password = password;

                service.UserCredentialsValue = user;

                return service.GetBookedRooms(roomId, travelDate);
            }
            catch (Exception ex)
            { throw ex; }
        }

        public int BookRoom(string userName, string password, int roomId, string roomNumber,int hotelId, DateTime travelDate, bool isLocked, string passengerSSN, string updatedUser)
        {
            try
            {
                HotelTSWebServices.SOAPHeaderService service = new HotelTSWebServices.SOAPHeaderService();
                HotelTSWebServices.UserCredentials user = new HotelTSWebServices.UserCredentials();
                user.userName = userName;
                user.password = password;

                service.UserCredentialsValue = user;

                return service.BookRoom(roomId, roomNumber, hotelId, travelDate, isLocked, passengerSSN, updatedUser);
            }
            catch (Exception ex)
            { throw ex; }
        }

        public int UpdateSlot(string userName, string password, int bookId, int hotelId, int roomId, DateTime travelDate)
        {
            try
            {
                HotelTSWebServices.SOAPHeaderService service = new HotelTSWebServices.SOAPHeaderService();
                HotelTSWebServices.UserCredentials user = new HotelTSWebServices.UserCredentials();
                user.userName = userName;
                user.password = password;

                service.UserCredentialsValue = user;

                return service.UpdateSlot(bookId, roomId, hotelId, travelDate);
            }
            catch (Exception ex)
            { throw ex; }
        }

        public int ConfirmSlot(string userName, string password, int bId, decimal paidAmount)
        {
            try
            {
                HotelTSWebServices.SOAPHeaderService service = new HotelTSWebServices.SOAPHeaderService();
                HotelTSWebServices.UserCredentials user = new HotelTSWebServices.UserCredentials();
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
                HotelTSWebServices.SOAPHeaderService service = new HotelTSWebServices.SOAPHeaderService();
                HotelTSWebServices.UserCredentials user = new HotelTSWebServices.UserCredentials();
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
