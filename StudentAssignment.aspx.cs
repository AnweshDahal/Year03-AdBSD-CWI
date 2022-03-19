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
    public partial class WebForm8 : System.Web.UI.Page
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
                    student_assignment.id ""Submission Number"",
                    student_assignment.student_id ""Student ID"",
                    student.student_title ""Title"",
                    student.student_name ""Name"",
                    student_assignment.module_code ""Module Code"",
                    module.module_name ""Module Name"",
                    module.credit_hour ""Credit Hour"",
                    student_assignment.assignment_type ""Type"",
                    student_assignment.grade ""Grade"",
                    student_assignment.status ""Status"",
                    departments.department_name || ' Department' ""Managed By""
                FROM
                    student_assignment
                JOIN
                    student
                ON
                    student_assignment.student_id = student.student_id
                JOIN
                    module
                ON
                    student_assignment.module_code = module.module_code
                JOIN
                    departments
                ON
                    student_assignment.managed_by = departments.department_id" :
                String.Format(@"SELECT
                    student_assignment.id ""Submission Number"",
                    student_assignment.student_id ""Student ID"",
                    student.student_title ""Title"",
                    student.student_name ""Name"",
                    student_assignment.module_code ""Module Code"",
                    module.module_name ""Module Name"",
                    module.credit_hour ""Credit Hour"",
                    student_assignment.assignment_type ""Type"",
                    student_assignment.grade ""Grade"",
                    student_assignment.status ""Status"",
                    departments.department_name || ' Department' ""Managed By""
                FROM
                    student_assignment
                JOIN
                    student
                ON
                    student_assignment.student_id = student.student_id
                JOIN
                    module
                ON
                    student_assignment.module_code = module.module_code
                JOIN
                    departments
                ON
                    student_assignment.managed_by = departments.department_id
                WHERE
                    student_assignment.student_id = {0}", this.studentID);
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

            studentAssignmentGV.DataSource = studentDT; // Setting teh data source of the grid view
            studentAssignmentGV.DataBind(); // Binding the data to the grid view
        }

        protected void submitStudentAssignmentBTN_Click(object sender, EventArgs e)
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