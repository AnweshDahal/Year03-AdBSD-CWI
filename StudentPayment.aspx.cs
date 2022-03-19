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
    public partial class WebForm7 : System.Web.UI.Page
    {
        private string connString = "ConnectionString_BCMS"; // Making the connection string a global variable
        private int studentID = -1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindData();
            }
        }

        protected void BindData(bool filtered = false)
        {
            // Defining the connection string for the database
            string constr = ConfigurationManager.ConnectionStrings[this.connString].ConnectionString;

            // Setting up the object for Oracle Command and Oracle connection 
            OracleCommand cmd = new OracleCommand();
            OracleConnection conn = new OracleConnection(constr);

            conn.Open(); // Opeaning a connection with the dB
            cmd.Connection = conn;
            // Query to select everything from the table
            cmd.CommandText = !filtered ?
                @"SELECT
                    student_payment.id,
                    student_payment.student_id,
                    student.student_title,
                    student.student_name,
                    student.student_address,
                    student_payment.payment_amount,
                    student_payment.payment_date,
                    departments.department_name || ' Department' AS MANAGED_BY
                FROM
                    student_payment
                JOIN
                    student
                ON
                    student_payment.student_id = student.student_id
                JOIN
                    departments
                ON
                    student_payment.managed_by = departments.department_id" :
                String.Format(@"SELECT
                    student_payment.id,
                    student_payment.student_id,
                    student.student_title,
                    student.student_name,
                    student.student_address,
                    student_payment.payment_amount,
                    student_payment.payment_date,
                    departments.department_name || ' Department'
                FROM
                    student_payment
                JOIN
                    student
                ON
                    student_payment.student_id = student.student_id
                JOIN
                    departments
                ON
                    student_payment.managed_by = departments.department_id
                WHERE
                    student_payment.student_id = {0}", this.studentID);
            cmd.CommandType = CommandType.Text;

            // Creating a new data table to store the data fetched from the database.
            DataTable studentDT = new DataTable("student");

            // Loading the data from the database into the data table
            using (OracleDataReader odr = cmd.ExecuteReader())
            {
                studentDT.Load(odr);
            }

            // Closing the connection to the database
            conn.Close();

            studentPaymentGV.DataSource = studentDT; // Setting teh data source of the grid view
            studentPaymentGV.DataBind(); // Binding the data to the grid view
        }

        protected void submitStudentPaymentBTN_Click(object sender, EventArgs e)
        {
            this.studentID = Int32.Parse(studentDropDown.SelectedItem.Value);
            this.BindData(filtered: true);
        }

        protected void resetBTN_Click(object sender, EventArgs e)
        {
            this.studentID = -1;
            this.BindData(filtered: false);
        }
    }
}