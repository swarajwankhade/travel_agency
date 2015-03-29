﻿using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.IO;

namespace AirwayTSWebService
{
    /// <summary>
    /// 
    /// </summary>
    public partial class DataAccess
    {
        private string myConnectionString;
        private SqlConnection mySqlConnection;
        private SqlCommand mySqlCommand;
        private SqlDataAdapter mySqlDataAdapter;

        /// <summary>
        /// constructor
        /// </summary>
        public DataAccess()
        {
            // instantiate ConnectionString from web.config
            myConnectionString = ConfigurationManager.ConnectionStrings["sqlConnection"].ConnectionString;

            // instantiate Connection
            mySqlConnection = new SqlConnection(myConnectionString);
        }

        /// <summary>
        /// Opens, closes and executes database connection and returns query as DataTable
        /// </summary>
        /// <param name="sCommandText">dbo.Object</param>
        /// <param name="SqlParameter">SQL Parameter</param>
        /// <returns>Query result as DataTable</returns>
        private DataTable FillTable(string sCommandText, SqlParameter SqlParameter)
        {
            try
            {
                DataTable myDataTable = new DataTable();

                // open connection
                mySqlConnection.Open();

                // setup command options and parameteres
                mySqlCommand = new SqlCommand();
                mySqlCommand.Connection = mySqlConnection;
                mySqlCommand.CommandText = sCommandText;
                mySqlCommand.CommandType = CommandType.StoredProcedure;

                // include parameter if not empty (used for overload/optional FillTable())
                if (SqlParameter.ParameterName != "")
                {
                    mySqlCommand.Parameters.Add(SqlParameter);
                }

                // setup dataAdapater and fill datatTable
                mySqlDataAdapter = new SqlDataAdapter(mySqlCommand);
                mySqlDataAdapter.Fill(myDataTable);

                // close connection
                mySqlConnection.Close();

                return myDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // force close connection
                if (mySqlConnection.State != ConnectionState.Closed)
                {
                    mySqlConnection.Close();
                }
            }
        }
        /// </summary>
        /// Overloaded FillTable method with Array Parameters
        /// <param name="sCommandText">dbo.Object</param>
        /// <param name="SqlParameter">SQL Parameter</param>
        /// <returns>Query result as DataTable</returns>
        private DataTable FillTable(string sCommandText, SqlParameter[] SqlParameter)
        {
            try
            {
                DataTable myDataTable = new DataTable();

                // open connection
                mySqlConnection.Open();

                // setup command options and parameteres
                mySqlCommand = new SqlCommand();
                mySqlCommand.Connection = mySqlConnection;
                mySqlCommand.CommandText = sCommandText;
                mySqlCommand.CommandType = CommandType.StoredProcedure;

                for (int a = 0; a < SqlParameter.Count(); a++)
                {
                    mySqlCommand.Parameters.Add(SqlParameter[a]);
                }


                // setup dataAdapater and fill datatTable
                mySqlDataAdapter = new SqlDataAdapter(mySqlCommand);
                mySqlDataAdapter.Fill(myDataTable);

                // close connection


                return myDataTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                mySqlConnection.Close();
                // force close connection
                if (mySqlConnection.State != ConnectionState.Closed)
                {
                    mySqlConnection.Close();
                }
            }
        }
        /// <summary>
        /// Overloaded FillTable method without the parameter.
        /// Sends a dummy parameter to the FillTable().
        /// </summary>
        /// <returns>Query result as DataTable</returns>
        private DataTable FillTable(string sCommandText)
        {
            // create dummy parameter
            SqlParameter mySQLParam = new SqlParameter();
            mySQLParam.ParameterName = "";
            mySQLParam.Value = "";

            // execute FillTable with dummy parameter
            return FillTable(sCommandText, mySQLParam);
        }
        // overload to allow parameter arrays
        private string ScalarValue(string sCommandText, SqlParameter[] SqlParameter)
        {
            string s;   // temporary return string container

            try
            {
                // open Connection
                mySqlConnection.Open();

                // configure Command, add Parameter
                mySqlCommand = new SqlCommand();
                mySqlCommand.Connection = mySqlConnection;
                mySqlCommand.CommandText = sCommandText;
                mySqlCommand.CommandType = CommandType.StoredProcedure;

                for (int a = 0; a < SqlParameter.Count(); a++)
                {
                    mySqlCommand.Parameters.Add(SqlParameter[a]);
                }

                s = mySqlCommand.ExecuteScalar().ToString();

                //// validate scalar value if not dbNull, then if not stringNull/Empty (short-circuited)
                //if (mySqlCommand.ExecuteScalar() != null &&
                //    !String.IsNullOrEmpty(Convert.ToString(mySqlCommand.ExecuteScalar())))
                //{
                //    s = mySqlCommand.ExecuteScalar().ToString();
                //}
                //else
                //{
                //    // s = String.Empty;
                //    // throw an error
                //    throw new ApplicationException("NullScalarValue");
                //}                

                // close Connection
                mySqlConnection.Close();

                // return ExecuteScalar value
                return s;
            }
            catch (ApplicationException aex)
            {
                throw aex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // force to close connection
                if (mySqlConnection.State != ConnectionState.Closed)
                {
                    mySqlConnection.Close();
                }
            }
        }
        /// <summary>
        /// Returns the ExecuteScalar string value based from the supplied object and parameter.
        /// </summary>
        /// <param name="sCommandText">dbo.Object</param>
        /// <param name="SqlParameter">SQL Parameter</param>
        /// <returns>ExecuteScalar string value</returns>
        private string ScalarValue(string sCommandText, SqlParameter SqlParameter)
        {
            string s;   // temporary return string container

            try
            {
                // open Connection
                mySqlConnection.Open();

                // configure Command, add Parameter
                mySqlCommand = new SqlCommand();
                mySqlCommand.Connection = mySqlConnection;
                mySqlCommand.CommandText = sCommandText;
                mySqlCommand.CommandType = CommandType.StoredProcedure;
                mySqlCommand.Parameters.Add(SqlParameter);

                // validate scalar value if not dbNull, then if not stringNull/Empty (short-circuited)
                if (mySqlCommand.ExecuteScalar() != null &&
                    !String.IsNullOrEmpty(Convert.ToString(mySqlCommand.ExecuteScalar())))
                {
                    s = mySqlCommand.ExecuteScalar().ToString();
                }
                else
                {
                    // s = String.Empty;
                    // throw an error
                    throw new ApplicationException("NullScalarValue");
                }

                // close Connection
                mySqlConnection.Close();

                // return ExecuteScalar value
                return s;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // force to close connection
                if (mySqlConnection.State != ConnectionState.Closed)
                {
                    mySqlConnection.Close();
                }
            }
        }

        public bool CheckUser(string userName, string password)
        {
            string sCommandText = "dbo.[Find_User]";
            DataTable dataTable = new DataTable();

            // add parameter
            SqlParameter[] mySQLParam = new SqlParameter[]{
                    new SqlParameter("@user_name", userName),
                    new SqlParameter("@password", password)
            };

            dataTable = FillTable(sCommandText, mySQLParam);

            if (dataTable.Rows.Count > 0)
            {
                return true;
            }

            return false;

        }



        public DataTable GetAirlines()
        {
            string sCommandText = string.Empty;

            sCommandText = "dbo.[GetAirlines]";

            return FillTable(sCommandText);
        }

        public DataTable GetAirbus(int airlineId, string from, string to)
        {
            string sCommandText = string.Empty;

            sCommandText = "dbo.[GetAirbus]";

            // add parameter
            SqlParameter[] mySQLParam = new SqlParameter[] { 
                    new SqlParameter("@airlineId", airlineId),
                    new SqlParameter("@from", from),
                    new SqlParameter("@to", to)
                };

            return FillTable(sCommandText, mySQLParam);
            
        }

        public DataTable GetAirbusById(int airbusId)
        {
            string sCommandText = string.Empty;

            sCommandText = "dbo.[GetAirbusById]";

            // add parameter
            SqlParameter[] mySQLParam = new SqlParameter[] { 
                    new SqlParameter("@airbusId", airbusId)
                };

            return FillTable(sCommandText, mySQLParam);

        }

        public DataTable GetBookedSlots(int airbusId)
        {
            string sCommandText = string.Empty;

            sCommandText = "dbo.[GetAllBookedSlots]";

            // add parameter
            SqlParameter[] mySQLParam = new SqlParameter[] { 
                    new SqlParameter("@airbusId", airbusId)
                };

            return FillTable(sCommandText, mySQLParam);
        }

        public DataTable GetBookedSlots(int airbusId, DateTime travelDate)
        {
            string sCommandText = string.Empty;

            sCommandText = "dbo.[GetBookedSlots]";

            // add parameter
            SqlParameter[] mySQLParam = new SqlParameter[] { 
                    new SqlParameter("@airbusId", airbusId),
                    new SqlParameter("@travelDate", travelDate)
                };

            return FillTable(sCommandText, mySQLParam);
        }

        public DataTable GetBookedSlots(int airbusId, int seatNumber, DateTime travelDate)
        {
            string sCommandText = string.Empty;

            sCommandText = "dbo.[GetBookedSlots]";

            // add parameter
            SqlParameter[] mySQLParam = new SqlParameter[] { 
                    new SqlParameter("@airbusId", airbusId),
                    new SqlParameter("@seatNumber", seatNumber),
                    new SqlParameter("@travelDate", travelDate)
                };

            return FillTable(sCommandText, mySQLParam);
        }

        public DataTable GetSlots(DateTime travelDate)
        {
            string sCommandText = string.Empty;

            sCommandText = "dbo.[GetSlots]"; 

            // add parameter
            SqlParameter[] mySQLParam = new SqlParameter[] { 
                    new SqlParameter("@travelDate", travelDate)
                };

            DataTable dtSlots = FillTable(sCommandText, mySQLParam);
            DataTable dt = new DataTable("MyDataTable");
            dt.Columns.Add("column1", typeof(System.String));
            dt.Columns.Add("column2", typeof(System.String));

            DataRow dr = dt.NewRow();
            dr["column1"] = "Your Data";
            dr["column2"] = "Your Data";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["column1"] = "Your Data";
            dr["column2"] = "Your Data";
            dt.Rows.Add(dr);

            return dt;
        }

        
        public int BookSlot(int airbusId, int seatNumber, DateTime travelDate, bool isLocked, string passengerSSN, string updatedUser)
        {
            string sCommandText = "dbo.[BookSlot]";

            try
            {
                DataTable myDataTable = new DataTable();

                // open connection
                mySqlConnection.Open();

                // setup command options and parameteres
                mySqlCommand = new SqlCommand();
                mySqlCommand.Connection = mySqlConnection;
                mySqlCommand.CommandText = sCommandText;
                mySqlCommand.CommandType = CommandType.StoredProcedure;

                // add parameter
                SqlParameter[] mySQLParam = new SqlParameter[]{
                    new SqlParameter("@airbusId", airbusId),
                    new SqlParameter("@seatNumber", seatNumber),
                    new SqlParameter("@travelDate", travelDate),
                    new SqlParameter("@isLocked", isLocked),
                    new SqlParameter("@passengerSSN", passengerSSN),
                    new SqlParameter("@updatedUser", updatedUser)
                };

                for (int a = 0; a < mySQLParam.Count(); a++)
                {
                    mySqlCommand.Parameters.Add(mySQLParam[a]);
                }

                int id = Convert.ToInt32(mySqlCommand.ExecuteScalar());
                
                // close connection
                mySqlConnection.Close();
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // force close connection
                if (mySqlConnection.State != ConnectionState.Closed)
                {
                    mySqlConnection.Close();
                }
            }
        }

        public int UpdateSlot(int bookId, int slotNumber, DateTime travelDate)
        {
            string sCommandText = "dbo.[UpdateSlot]";

            try
            {
                DataTable myDataTable = new DataTable();

                // open connection
                mySqlConnection.Open();

                // setup command options and parameteres
                mySqlCommand = new SqlCommand();
                mySqlCommand.Connection = mySqlConnection;
                mySqlCommand.CommandText = sCommandText;
                mySqlCommand.CommandType = CommandType.StoredProcedure;

                // add parameter
                SqlParameter[] mySQLParam = new SqlParameter[]{
                    new SqlParameter("@id", bookId),
                    new SqlParameter("@slotNumber", slotNumber),
                    new SqlParameter("@travelDate", travelDate)
                };

                for (int a = 0; a < mySQLParam.Count(); a++)
                {
                    mySqlCommand.Parameters.Add(mySQLParam[a]);
                }

                int id = Convert.ToInt32(mySqlCommand.ExecuteScalar());

                // close connection
                mySqlConnection.Close();
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // force close connection
                if (mySqlConnection.State != ConnectionState.Closed)
                {
                    mySqlConnection.Close();
                }
            }
        }
        public int ConfirmSlot(int bId, string userName, decimal paidAmount)
        {
            string sCommandText = "dbo.[ConfirmSlot]";

            try
            {
                DataTable myDataTable = new DataTable();

                // open connection
                mySqlConnection.Open();

                // setup command options and parameteres
                mySqlCommand = new SqlCommand();
                mySqlCommand.Connection = mySqlConnection;
                mySqlCommand.CommandText = sCommandText;
                mySqlCommand.CommandType = CommandType.StoredProcedure;

                // add parameter
                SqlParameter[] mySQLParam = new SqlParameter[]{
                    new SqlParameter("@id", bId),
                    new SqlParameter("@userName", userName),
                    new SqlParameter("@paidAmount", paidAmount)
                };

                for (int a = 0; a < mySQLParam.Count(); a++)
                {
                    mySqlCommand.Parameters.Add(mySQLParam[a]);
                }

                int id = Convert.ToInt32(mySqlCommand.ExecuteScalar());

                // close connection
                mySqlConnection.Close();
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // force close connection
                if (mySqlConnection.State != ConnectionState.Closed)
                {
                    mySqlConnection.Close();
                }
            }
        }

        public int DeleteSlot(int bookId)
        {
            string sCommandText = "dbo.[DeleteSlot]";

            try
            {
                DataTable myDataTable = new DataTable();

                // open connection
                mySqlConnection.Open();

                // setup command options and parameteres
                mySqlCommand = new SqlCommand();
                mySqlCommand.Connection = mySqlConnection;
                mySqlCommand.CommandText = sCommandText;
                mySqlCommand.CommandType = CommandType.StoredProcedure;

                // add parameter
                SqlParameter[] mySQLParam = new SqlParameter[]{
                    new SqlParameter("@id", bookId)
                };

                for (int a = 0; a < mySQLParam.Count(); a++)
                {
                    mySqlCommand.Parameters.Add(mySQLParam[a]);
                }

                int id = Convert.ToInt32(mySqlCommand.ExecuteScalar());

                // close connection
                mySqlConnection.Close();
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // force close connection
                if (mySqlConnection.State != ConnectionState.Closed)
                {
                    mySqlConnection.Close();
                }
            }
        }
        
    }

}
