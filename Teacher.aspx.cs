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
    public partial class WebForm3 : System.Web.UI.Page
    {
        private string connString = "ConnectionString_BCMS"; // Making the connection string a global variable

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindData();
            }
        }

        protected void BindData()
        {
            // Defining the connection string for the database
            string constr = ConfigurationManager.ConnectionStrings[this.connString].ConnectionString;

            // Setting up the object for Oracle Command and Oracle connection 
            OracleCommand cmd = new OracleCommand();
            OracleConnection conn = new OracleConnection(constr);

            conn.Open(); // Opeaning a connection with the dB
            cmd.Connection = conn;
            // Query to select everything from the table
            cmd.CommandText = @"SELECT id, name, email FROM teacher WHERE is_deleted IS NULL";
            cmd.CommandType = CommandType.Text;

            // Creating a new data table to store the data fetched from the database.
            DataTable teacherDT = new DataTable("teacher");

            // Loading the data from the database into the data table
            using (OracleDataReader odr = cmd.ExecuteReader())
            {
                teacherDT.Load(odr);
            }

            // Closing the connection to the database
            conn.Close();

            teacherGV.DataSource = teacherDT; // Setting teh data source of the grid view
            teacherGV.DataBind(); // Binding the data to the grid view
        }

        protected void submitTeacherBTN_Click(object sender, EventArgs e)
        {
            // Getting the data to submit
            int id = Int32.Parse(idTB.Text);
            string teacherName = teacherNameTB.Text;
            string teacherEmail = teacherEmailTB.Text;

            // Setting up the connection string
            string connstr = ConfigurationManager.ConnectionStrings[this.connString].ConnectionString;
            OracleConnection oCon = new OracleConnection(connstr);

            // If the Button Says Submit
            if (submitTeacherBTN.Text == "Submit")
            {
                // Initializing the Insertion Query
                OracleCommand oCom = new OracleCommand(String.Format("INSERT INTO teacher (id, name, email) VALUES ({0}, '{1}', '{2}')", id, teacherName, teacherEmail));
                oCom.Connection = oCon;
                oCon.Open();
                oCom.ExecuteNonQuery();
                oCon.Close();

                // Clearing the fields
                idTB.Text = "";
                teacherNameTB.Text = "";
                teacherEmailTB.Text = "";
            }
            else if (submitTeacherBTN.Text == "Update")
            {
                // if the button says Update
                OracleCommand oCom = new OracleCommand(String.Format("UPDATE teacher SET name = '{0}', email = '{1}' WHERE id = {2}", teacherName, teacherEmail, id));
                oCom.Connection = oCon;
                oCon.Open();
                oCom.ExecuteNonQuery();
                oCon.Close();

                // Resetting the modifications made for update
                idTB.Enabled = true;
                submitTeacherBTN.Text = "Submit";
                teacherGV.EditIndex = -1;

                // Clearing the fields
                idTB.Text = "";
                teacherNameTB.Text = "";
                teacherEmailTB.Text = "";
            }


            this.BindData();
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != teacherGV.EditIndex)
            {
                (e.Row.Cells[0].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this row?');";


            }
            teacherGV.EditIndex = -1;

        }

        protected void OnRowCancelEditing(object sender, EventArgs e)
        {
            this.BindData();
            teacherGV.EditIndex = -1;
        }

        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = Convert.ToInt32(teacherGV.DataKeys[e.RowIndex].Values[0]);
            string constr = ConfigurationManager.ConnectionStrings[this.connString].ConnectionString;

            using (OracleConnection oCon = new OracleConnection(constr))
            {
                using (OracleCommand oCmd = new OracleCommand(String.Format("UPDATE teacher SET is_deleted = 1 WHERE id = {0}", ID)))
                {
                    oCmd.Connection = oCon;
                    oCon.Open();
                    oCmd.ExecuteNonQuery();
                    oCon.Close();
                }
            }

            this.BindData();
            teacherGV.EditIndex = -1;

        }

        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {

            // get id for data update
            idTB.Text = this.teacherGV.Rows[e.NewEditIndex].Cells[1].Text;
            idTB.Enabled = false;
            teacherNameTB.Text = this.teacherGV.Rows[e.NewEditIndex].Cells[2].Text.ToString().TrimStart().TrimEnd(); // (row.Cells[2].Controls[0] as TextBox).Text;
            teacherEmailTB.Text = this.teacherGV.Rows[e.NewEditIndex].Cells[3].Text;
            submitTeacherBTN.Text = "Update";
        }
    }
}