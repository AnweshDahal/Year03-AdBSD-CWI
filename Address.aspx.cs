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
    public partial class WebForm4 : System.Web.UI.Page
    {

        private string connString = "ConnectionString_BCMS"; // Making the connection string a global variable

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Addresses";
            // Rendering the data in the grid view
            if (!this.IsPostBack)
            {
                this.BindData();
            }
        }

        protected void submitAddressBTN_Click(object sender, EventArgs e)
        {
            // Getting the data to submit
            int id = Int32.Parse(idTB.Text);
            string address = addressTB.Text;

            // Setting up the connection string
            string connstr = ConfigurationManager.ConnectionStrings[this.connString].ConnectionString;
            OracleConnection oCon = new OracleConnection(connstr);

            // If the Button Says Submit
            if (submitAddressBTN.Text == "Submit")
            {
                // Initializing the Insertion Query
                OracleCommand oCom = new OracleCommand(String.Format("INSERT INTO address (address_id, address) VALUES ({0}, '{1}')", id, address));
                oCom.Connection = oCon;
                oCon.Open();
                oCom.ExecuteNonQuery();
                oCon.Close();

                // Clearing the fields
                idTB.Text = "";
                addressTB.Text = "";
            }
            else if (submitAddressBTN.Text == "Update")
            {
                // if the button says Update
                OracleCommand oCom = new OracleCommand(String.Format("UPDATE address SET address = '{0}' WHERE address_id = {1}", address, id));
                oCom.Connection = oCon;
                oCon.Open();
                oCom.ExecuteNonQuery();
                oCon.Close();

                // Resetting the modifications made for update
                idTB.Enabled = true;
                submitAddressBTN.Text = "Submit";
                addressGV.EditIndex = -1;

                // Clearing the fields
                idTB.Text = "";
                addressTB.Text = "";
            }


            // Setting up the connection for the insert command


            this.BindData();
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
            cmd.CommandText = @"SELECT address_id, address FROM address WHERE is_deleted IS NULL";
            cmd.CommandType = CommandType.Text;

            // Creating a new data table to store the data fetched from the database.
            DataTable addressDT = new DataTable("address");

            // Loading the data from the database into the data table
            using (OracleDataReader odr = cmd.ExecuteReader())
            {
                addressDT.Load(odr);
            }

            // Closing the connection to the database
            conn.Close();

            addressGV.DataSource = addressDT; // Setting teh data source of the grid view
            addressGV.DataBind(); // Binding the data to the grid view

        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex != addressGV.EditIndex)
            {
                (e.Row.Cells[0].Controls[2] as LinkButton).Attributes["onclick"] = "return confirm('Do you want to delete this row?');";


            }
            addressGV.EditIndex = -1;

        }

        protected void OnRowCancelEditing(object sender, EventArgs e)
        {
            this.BindData();
            addressGV.EditIndex = -1;
        }

        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = Convert.ToInt32(addressGV.DataKeys[e.RowIndex].Values[0]);
            string constr = ConfigurationManager.ConnectionStrings[this.connString].ConnectionString;

            using (OracleConnection oCon = new OracleConnection(constr))
            {
                using (OracleCommand oCmd = new OracleCommand(String.Format("UPDATE address SET is_deleted = 1 WHERE address_id = {0}", ID)))
                {
                    oCmd.Connection = oCon;
                    oCon.Open();
                    oCmd.ExecuteNonQuery();
                    oCon.Close();
                }
            }

            this.BindData();
            addressGV.EditIndex = -1;

        }

        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {

            // get id for data update
            idTB.Text = this.addressGV.Rows[e.NewEditIndex].Cells[1].Text;
            idTB.Enabled = false;
            addressTB.Text = this.addressGV.Rows[e.NewEditIndex].Cells[2].Text.ToString().TrimStart().TrimEnd(); // (row.Cells[2].Controls[0] as TextBox).Text;
            submitAddressBTN.Text = "Update";
        }
    }
}