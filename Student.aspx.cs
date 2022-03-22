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
    public partial class WebForm1 : System.Web.UI.Page
    {
        private string connString = "ConnectionString_BCMS"; // Making the connection string a global variable

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindData();
            }
        }

        public void BindData()
        {
            // Defining the connection string for the database
            string constr = ConfigurationManager.ConnectionStrings[this.connString].ConnectionString;

            // Setting up the object for Oracle Command and Oracle connection 
            OracleCommand cmd = new OracleCommand();
            OracleConnection conn = new OracleConnection(constr);

            conn.Open(); // Opeaning a connection with the dB
            cmd.Connection = conn;
            // Query to select everything from the table
            cmd.CommandText = @"SELECT student_id, student_title, student_name, student_address FROM student WHERE is_deleted IS NULL";
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

            studentsGV.DataSource = studentDT; // Setting teh data source of the grid view
            studentsGV.DataBind(); // Binding the data to the grid view

        }

        protected void submitStudentBTN_Click(object sender, EventArgs e)
        {
            // Getting the data to submit
            int id = Int32.Parse(idTB.Text);
            string studentName = studentNameTB.Text;
            string studentAddress = studentAddressTB.Text;
            string studentDesignation = designationSelect.SelectedItem.Value;

            // Setting up the connection string
            string connstr = ConfigurationManager.ConnectionStrings[this.connString].ConnectionString;
            OracleConnection oCon = new OracleConnection(connstr);

            // If the Button Says Submit
            if (submitStudentBTN.Text == "Submit")
            {
                // Initializing the Insertion Query
                OracleCommand oCom = new OracleCommand(String.Format("INSERT INTO student (student_id, student_name, student_address, student_title) VALUES ({0}, '{1}', '{2}', '{3}')", id, studentName, studentAddress, studentDesignation));
                oCom.Connection = oCon;
                oCon.Open();
                oCom.ExecuteNonQuery();
                oCon.Close();

                // Clearing the fields
                idTB.Text = "";
                studentNameTB.Text = "";
                studentAddressTB.Text = "";
            }
            else if (submitStudentBTN.Text == "Update")
            {
                // if the button says Update
                OracleCommand oCom = new OracleCommand(String.Format("UPDATE student SET student_name = '{0}', student_address = '{1}', student_title = '{2}' WHERE student_id = {3}", studentName, studentAddress, studentDesignation, id));
                oCom.Connection = oCon;
                oCon.Open();
                oCom.ExecuteNonQuery();
                oCon.Close();

                // Resetting the modifications made for update
                idTB.Enabled = true;
                submitStudentBTN.Text = "Submit";
                studentsGV.EditIndex = -1;

                // Clearing the fields
                idTB.Text = "";
                studentNameTB.Text = "";
                studentAddressTB.Text = "";
            }


            // Setting up the connection for the insert command


            this.BindData();
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != studentsGV.EditIndex)
            {
                (e.Row.Cells[0].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this row?');";


            }
            studentsGV.EditIndex = -1;

        }

        protected void OnRowCancelEditing(object sender, EventArgs e)
        {
            this.BindData();
            studentsGV.EditIndex = -1;
        }

        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = Convert.ToInt32(studentsGV.DataKeys[e.RowIndex].Values[0]);
            string constr = ConfigurationManager.ConnectionStrings[this.connString].ConnectionString;

            using (OracleConnection oCon = new OracleConnection(constr))
            {
                using (OracleCommand oCmd = new OracleCommand(String.Format("UPDATE student SET is_deleted = 1 WHERE student_id = {0}", ID)))
                {
                    oCmd.Connection = oCon;
                    oCon.Open();
                    oCmd.ExecuteNonQuery();
                    oCon.Close();
                }
            }

            this.BindData();
            studentsGV.EditIndex = -1;

        }

        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {

            // get id for data update
            idTB.Text = this.studentsGV.Rows[e.NewEditIndex].Cells[1].Text;
            idTB.Enabled = false;
            designationSelect.SelectedValue = this.studentsGV.Rows[e.NewEditIndex].Cells[2].Text.ToString().TrimStart().TrimEnd();
            studentNameTB.Text = this.studentsGV.Rows[e.NewEditIndex].Cells[3].Text.ToString().TrimStart().TrimEnd(); // (row.Cells[2].Controls[0] as TextBox).Text;
            studentAddressTB.Text = this.studentsGV.Rows[e.NewEditIndex].Cells[4].Text;
            submitStudentBTN.Text = "Update";
        }
    }
}
