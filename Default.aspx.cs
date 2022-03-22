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
            oCon.Open(); // Opening a connection with the dB

            // Setting up the student dashboard content
            OracleCommand oComStudent = new OracleCommand("SELECT COUNT(*) FROM student WHERE is_deleted IS NULL");
            oComStudent.Connection = oCon;
            
            OracleDataReader reader = oComStudent.ExecuteReader();

            while (reader.Read())
            {
                totalStudentsLBL.Text = reader["Count(*)"].ToString();
            }

            // setting up the teacher dashboard content
            OracleCommand oComTeacher = new OracleCommand("SELECT COUNT(*) FROM teacher WHERE is_deleted IS NULL");
            oComTeacher.Connection = oCon;

            reader = oComTeacher.ExecuteReader();

            while (reader.Read())
            {
                totalTeachersLBL.Text = reader["Count(*)"].ToString();
            }

            // Setting up the departments dash board contents
            OracleCommand oComDepartment = new OracleCommand("SELECT COUNT(*) FROM departments WHERE is_deleted IS NULL");
            oComDepartment.Connection = oCon;

            reader = oComDepartment.ExecuteReader();

            while (reader.Read())
            {
                totalDepartmentsLBL.Text = reader["Count(*)"].ToString();
            }

            // Setting up the module dashboard contents
            OracleCommand oComModules = new OracleCommand("SELECT COUNT(*) FROM module WHERE is_deleted IS NULL");
            oComModules.Connection = oCon;

            reader = oComModules.ExecuteReader();

            while (reader.Read())
            {
                totalModulesLBL.Text = reader["Count(*)"].ToString();
            }

            // Setting up the payments dashboard contents
            OracleCommand oComPayments = new OracleCommand("SELECT SUM(payment_amount) FROM student_payment WHERE is_deleted IS NULL");
            oComPayments.Connection = oCon;

            reader = oComPayments.ExecuteReader();

            while (reader.Read())
            {
                totalPaymentsLBL.Text = reader["SUM(payment_amount)"].ToString();
            }

            // Closing the connection to the database
            oCon.Close();

        }
    }
}