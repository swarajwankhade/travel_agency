using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;

namespace BL
{
    public class UserBL
    {
        public DataTable AuthenticateUser(string userName, string password)
        {
            try
            {
                DataAccess da = new DataAccess();
                return da.CheckUser(userName, password);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
