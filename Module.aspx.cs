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
    public partial class WebForm5 : System.Web.UI.Page
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
            cmd.CommandText = @"SELECT module_code, module_name, credit_hour FROM module";
            cmd.CommandType = CommandType.Text;

            // Creating a new data table to store the data fetched from the database.
            DataTable moduleDT = new DataTable("module");

            // Loading the data from the database into the data table
            using (OracleDataReader odr = cmd.ExecuteReader())
            {
                moduleDT.Load(odr);
            }

            // Closing the connection to the database
            conn.Close();

            moduleGV.DataSource = moduleDT; // Setting teh data source of the grid view
            moduleGV.DataBind(); // Binding the data to the grid view

        }

        protected void submitModuleBTN_Click(object sender, EventArgs e)
        {
            // Getting the data to submit
            string id = idTB.Text;
            string moduleName = moduleNameTB.Text;
            int creditHour = Int32.Parse(creditHourTB.Text);

            // Setting up the connection string
            string connstr = ConfigurationManager.ConnectionStrings[this.connString].ConnectionString;
            OracleConnection oCon = new OracleConnection(connstr);

            // If the Button Says Submit
            if (submitModuleBTN.Text == "Submit")
            {
                // Initializing the Insertion Query
                OracleCommand oCom = new OracleCommand(String.Format("INSERT INTO module (module_code, module_name, credit_hour) VALUES ('{0}', '{1}', {2})", id, moduleName, creditHour));
                oCom.Connection = oCon;
                oCon.Open();
                oCom.ExecuteNonQuery();
                oCon.Close();

                // Clearing the fields
                idTB.Text = "";
                moduleNameTB.Text = "";
                creditHourTB.Text = "";
            }
            else if (submitModuleBTN.Text == "Update")
            {
                // if the button says Update
                OracleCommand oCom = new OracleCommand(String.Format("UPDATE module SET module_name = '{0}', credit_hour = {1} WHERE module_code = '{2}'", moduleName, creditHour, id));
                oCom.Connection = oCon;
                oCon.Open();
                oCom.ExecuteNonQuery();
                oCon.Close();

                // Resetting the modifications made for update
                idTB.Enabled = true;
                submitModuleBTN.Text = "Submit";
                moduleGV.EditIndex = -1;

                // Clearing the fields
                idTB.Text = "";
                moduleNameTB.Text = "";
                creditHourTB.Text = "";
            }


            // Setting up the connection for the insert command
            this.BindData();
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != moduleGV.EditIndex)
            {
                (e.Row.Cells[0].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this row?');";


            }
            moduleGV.EditIndex = -1;

        }

        protected void OnRowCancelEditing(object sender, EventArgs e)
        {
            this.BindData();
            moduleGV.EditIndex = -1;
        }

        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string ID = moduleGV.DataKeys[e.RowIndex].Values[0].ToString();
            string constr = ConfigurationManager.ConnectionStrings[this.connString].ConnectionString;

            using (OracleConnection oCon = new OracleConnection(constr))
            {
                using (OracleCommand oCmd = new OracleCommand(String.Format("DELETE FROM module WHERE module_code = '{0}'", ID)))
                {
                    oCmd.Connection = oCon;
                    oCon.Open();
                    oCmd.ExecuteNonQuery();
                    oCon.Close();
                }
            }

            this.BindData();
            moduleGV.EditIndex = -1;
        }

        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            // get id for data update
            idTB.Text = this.moduleGV.Rows[e.NewEditIndex].Cells[1].Text;
            idTB.Enabled = false;
            moduleNameTB.Text = this.moduleGV.Rows[e.NewEditIndex].Cells[2].Text.ToString().TrimStart().TrimEnd(); // (row.Cells[2].Controls[0] as TextBox).Text;
            creditHourTB.Text = this.moduleGV.Rows[e.NewEditIndex].Cells[3].Text.ToString().TrimStart().TrimEnd();
            submitModuleBTN.Text = "Update";
        }
    }
}
