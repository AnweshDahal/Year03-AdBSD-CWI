using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace ADbSD_Coursework_I
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        private string connString = "ConnectionString_BCMS"; // Making the connection string a global variable

        protected void Page_Load(object sender, EventArgs e)
        {
            // Rendering the data in the grid view
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
            cmd.CommandText = @"SELECT department_id, department_name, department_head FROM departments";
            cmd.CommandType = CommandType.Text;

            // Creating a new data table to store the data fetched from the database.
            DataTable departmentDT = new DataTable("departments");

            // Loading the data from the database into the data table
            using (OracleDataReader odr = cmd.ExecuteReader())
            {
                departmentDT.Load(odr);
            }

            // Closing the connection to the database
            conn.Close();

            departmentGV.DataSource = departmentDT; // Setting teh data source of the grid view
            departmentGV.DataBind(); // Binding the data to the grid view

        }

        protected void submitDepartmentsBTN_Click(object sender, EventArgs e)
        {
            // Getting the data to submit
            int id = Int32.Parse(idTB.Text);
            string departmentName = departmentNameTB.Text;
            string departmentHead = departmentHeadTXT.Text;

            // Setting up the connection string
            string connstr = ConfigurationManager.ConnectionStrings[this.connString].ConnectionString;
            OracleConnection oCon = new OracleConnection(connstr);

            // If the Button Says Submit
            if(submitDepartmentsBTN.Text == "Submit")
            {
                // Initializing the Insertion Query
                OracleCommand oCom = new OracleCommand(String.Format("INSERT INTO departments (department_id, department_name, department_head) VALUES ({0}, '{1}', '{2}')", id, departmentName, departmentHead));
                oCom.Connection = oCon;
                oCon.Open();
                oCom.ExecuteNonQuery();
                oCon.Close();

                // Clearing the fields
                idTB.Text = "";
                departmentNameTB.Text = "";
                departmentHeadTXT.Text = "";
            } else if (submitDepartmentsBTN.Text == "Update")
            {
                // if the button says Update
                OracleCommand oCom = new OracleCommand(String.Format("UPDATE departments SET department_name = '{0}', department_head = '{1}' WHERE department_id = {2}", departmentName, departmentHead, id));
                oCom.Connection = oCon;
                oCon.Open();
                oCom.ExecuteNonQuery();
                oCon.Close();

                // Resetting the modifications made for update
                idTB.Enabled = true;
                submitDepartmentsBTN.Text = "Submit";
                departmentGV.EditIndex = -1;

                // Clearing the fields
                idTB.Text = "";
                departmentNameTB.Text = "";
                departmentHeadTXT.Text = "";
            }
            

            // Setting up the connection for the insert command
            

            this.BindData();
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != departmentGV.EditIndex)
            {
                (e.Row.Cells[0].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this row?');";


            }
            departmentGV.EditIndex = -1;

        }

        protected void OnRowCancelEditing(object sender, EventArgs e)
        {
            this.BindData();
            departmentGV.EditIndex = -1;
        }

        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = Convert.ToInt32(departmentGV.DataKeys[e.RowIndex].Values[0]);
            string constr = ConfigurationManager.ConnectionStrings[this.connString].ConnectionString;

            using (OracleConnection oCon = new OracleConnection(constr))
            {
                using (OracleCommand oCmd = new OracleCommand(String.Format("DELETE FROM departments WHERE department_id = {0}", ID)))
                {
                    oCmd.Connection = oCon;
                    oCon.Open();
                    oCmd.ExecuteNonQuery();
                    oCon.Close();
                }
            }

            this.BindData();
            departmentGV.EditIndex = -1;

        }

        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {

            // get id for data update
            idTB.Text = this.departmentGV.Rows[e.NewEditIndex].Cells[1].Text;
            idTB.Enabled = false;
            departmentNameTB.Text = this.departmentGV.Rows[e.NewEditIndex].Cells[2].Text.ToString().TrimStart().TrimEnd(); // (row.Cells[2].Controls[0] as TextBox).Text;
            departmentHeadTXT.Text = this.departmentGV.Rows[e.NewEditIndex].Cells[3].Text;
            submitDepartmentsBTN.Text = "Update";
        }
    }
}