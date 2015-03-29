using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BL
{
    public class CarRentalBL
    {
        public DataTable GetCarRentals(string userName, string password)
        {
            try
            {
                CarRentalTSWebServices.SOAPHeaderService service = new CarRentalTSWebServices.SOAPHeaderService();
                CarRentalTSWebServices.UserCredentials user = new CarRentalTSWebServices.UserCredentials();
                user.userName = userName;
                user.password = password;

                service.UserCredentialsValue = user;

                return service.GetCarRentals();
            }
            catch (Exception ex)
            { throw ex; }
        }

        public DataTable GetCars(string userName, string password, int carRentalId)
        {
            try
            {
                CarRentalTSWebServices.SOAPHeaderService service = new CarRentalTSWebServices.SOAPHeaderService();
                CarRentalTSWebServices.UserCredentials user = new CarRentalTSWebServices.UserCredentials();
                user.userName = userName;
                user.password = password;

                service.UserCredentialsValue = user;

                return service.GetCar(carRentalId);
            }
            catch (Exception ex)
            { throw ex; }
        }

        public DataTable GetAvailableCars(string userName, string password, int carId, DateTime travelDate)
        {
            try
            {
                CarRentalTSWebServices.SOAPHeaderService service = new CarRentalTSWebServices.SOAPHeaderService();
                CarRentalTSWebServices.UserCredentials user = new CarRentalTSWebServices.UserCredentials();
                user.userName = userName;
                user.password = password;

                service.UserCredentialsValue = user;

                return service.GetAvailableCars(carId, travelDate);
            }
            catch (Exception ex)
            { throw ex; }
        }

        public DataTable GetBookedCars(string userName, string password, int carId, DateTime travelDate)
        {
            try
            {
                CarRentalTSWebServices.SOAPHeaderService service = new CarRentalTSWebServices.SOAPHeaderService();
                CarRentalTSWebServices.UserCredentials user = new CarRentalTSWebServices.UserCredentials();
                user.userName = userName;
                user.password = password;

                service.UserCredentialsValue = user;

                return service.GetBookedCars(carId, travelDate);
            }
            catch (Exception ex)
            { throw ex; }
        }

        public int BookCar(string userName, string password, int carId, string carNumber, DateTime travelDate, bool isLocked, string passengerSSN, string updatedUser)
        {
            try
            {
                CarRentalTSWebServices.SOAPHeaderService service = new CarRentalTSWebServices.SOAPHeaderService();
                CarRentalTSWebServices.UserCredentials user = new CarRentalTSWebServices.UserCredentials();
                user.userName = userName;
                user.password = password;

                service.UserCredentialsValue = user;

                return service.BookCar(carId, carNumber, travelDate, isLocked, passengerSSN, updatedUser);
            }
            catch (Exception ex)
            { throw ex; }
        }

        public int UpdateSlot(string userName, string password, int bookId, int carId, DateTime travelDate)
        {
            try
            {
                CarRentalTSWebServices.SOAPHeaderService service = new CarRentalTSWebServices.SOAPHeaderService();
                CarRentalTSWebServices.UserCredentials user = new CarRentalTSWebServices.UserCredentials();
                user.userName = userName;
                user.password = password;

                service.UserCredentialsValue = user;

                return service.UpdateSlot(bookId, carId, travelDate);
            }
            catch (Exception ex)
            { throw ex; }
        }

        public int ConfirmSlot(string userName, string password, int bId, decimal paidAmount)
        {
            try
            {
                CarRentalTSWebServices.SOAPHeaderService service = new CarRentalTSWebServices.SOAPHeaderService();
                CarRentalTSWebServices.UserCredentials user = new CarRentalTSWebServices.UserCredentials();
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
                CarRentalTSWebServices.SOAPHeaderService service = new CarRentalTSWebServices.SOAPHeaderService();
                CarRentalTSWebServices.UserCredentials user = new CarRentalTSWebServices.UserCredentials();
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
