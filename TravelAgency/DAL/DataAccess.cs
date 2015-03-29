using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;

namespace DAL
{
    public class DataAccess
    {
        public string myConnectionString;
        public SqlConnection mySqlConnection;
        public SqlCommand mySqlCommand;
        public SqlDataAdapter mySqlDataAdapter;

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

        public string GetRegionCode_ByCountrCode(string sCountryCode)
        {
            // add parameter
            SqlParameter mySQLParam = new SqlParameter();
            mySQLParam.ParameterName = "@sCountryCode";
            mySQLParam.Value = sCountryCode;

            return ScalarValue("dbo.[Country_ReturnRegionCode_ByCountryCode]", mySQLParam);
        }


        public void SaveProfile(string pRequestorId,
                                string pSalesTeam,
                                string pSegmentCode,
                                string pSegmentAreaCode,
                                string pRole,
                                string pCulture,
                                string pCultureTimeZone,
                                string pProgType,
                                bool pHPSW,
                                string pPartnerName,
                                string pCurrencyCode //14.06 June Release
            )
        {
            string sCommandText = "dbo.[User_Profile_Default_Save]";

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
                    new SqlParameter("@sRequestorId", pRequestorId),
                    new SqlParameter("@sSalesTeam", pSalesTeam),
                    new SqlParameter("@sSegmentCode", pSegmentCode),
                    new SqlParameter("@sSegmentAreaCode", pSegmentAreaCode),
                    new SqlParameter("@sRole", pRole),
                    new SqlParameter("@sCulture", pCulture),
                    new SqlParameter("@sCultureTimeZone", pCultureTimeZone),
                    new SqlParameter("@sProgType", pProgType),
                    new SqlParameter("@bHPSW", pHPSW),
                    new SqlParameter("@sPartnerName", pPartnerName),
                    new SqlParameter("@sCurrencyCode", pCurrencyCode)
                };

                for (int a = 0; a < mySQLParam.Count(); a++)
                {
                    mySqlCommand.Parameters.Add(mySQLParam[a]);
                }

                mySqlCommand.ExecuteNonQuery();

                // close connection
                mySqlConnection.Close();
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


        public DataTable GetEmailRecipient(string sCountryCode, string sFieldName, bool bIsDirect)
        {
            string sCommandText = string.Empty;

            if (bIsDirect)
            { sCommandText = "dbo.[Email_Recipient_SelectOne]"; }
            else
            { sCommandText = "dbo.[Email_Recipient_Channels_SelectOne]"; }

            // add parameter
            SqlParameter[] mySQLParam = new SqlParameter[] { 
                    new SqlParameter("@sCountryCode", sCountryCode),
                    new SqlParameter("@sFieldName", sFieldName)
                };

            return FillTable(sCommandText, mySQLParam);
        }

        public DataTable CheckUser(string userName, string password)
        {
            string sCommandText = "dbo.[Find_User]";
            DataTable dataTable = new DataTable();

            // add parameter
            SqlParameter[] mySQLParam = new SqlParameter[]{
                    new SqlParameter("@user_name", userName),
                    new SqlParameter("@password", password)
            };

            dataTable = FillTable(sCommandText, mySQLParam);

                return dataTable;

        }


    }
}
