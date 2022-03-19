using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ADbSD_Coursework_I
{
    public partial class _Default : Page
    {
        private string connString = "ConnectionString_BCMS"; // Making the connection string a global variable

        protected void Page_Load(object sender, EventArgs e)
        {
            // Defining the connection string for the database
            string constr = ConfigurationManager.ConnectionStrings[this.connString].ConnectionString;

            // Setting up the object for Oracle Command and Oracle connection 
            OracleConnection oCon = new OracleConnection(constr);
            OracleCommand oCom = new OracleCommand("SELECT COUNT(*) FROM student");
            oCom.Connection = oCon;
            oCon.Open(); // Opening a connection with the dB
            object query = oCom.ExecuteNonQuery();
            oCon.Close();

            // Closing the connection to the database
            oCon.Close();

        }
    }
}