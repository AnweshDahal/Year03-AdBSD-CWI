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
    public partial class WebForm6 : System.Web.UI.Page
    {
        private string connString = "ConnectionString_BCMS"; // Making the connection string a global variable
        private int teacherID = -1;

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
                    teacher_module.teacher_id, 
                    teacher.name, teacher.email, 
                    teacher_module.module_code, 
                    module.module_name, 
                    module.credit_hour 
                FROM 
                    teacher_module 
                JOIN 
                    module 
                ON 
                    teacher_module.module_code = module.module_code 
                JOIN 
                    teacher 
                ON 
                    teacher_module.teacher_id = teacher.id" :
                String.Format(@"SELECT 
                    teacher_module.teacher_id, 
                    teacher.name, teacher.email, 
                    teacher_module.module_code, 
                    module.module_name,
                    module.credit_hour 
                FROM 
                    teacher_module 
                JOIN 
                    module 
                ON 
                    teacher_module.module_code = module.module_code 
                JOIN 
                    teacher 
                ON 
                    teacher_module.teacher_id = teacher.id 
                WHERE teacher.id = {0}", this.teacherID);
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

            teacherModuleGV.DataSource = teacherDT; // Setting teh data source of the grid view
            teacherModuleGV.DataBind(); // Binding the data to the grid view
        }

        protected void submitTeacherModuleBTN_Click(object sender, EventArgs e)
        {
            this.teacherID =Int32.Parse(teacherDropDown.SelectedItem.Value);
            this.BindData(filtered:true);
        }

        protected void resetBTN_Click(object sender, EventArgs e)
        {
            this.teacherID = -1;
            this.BindData(filtered: false);
        }
    }
}