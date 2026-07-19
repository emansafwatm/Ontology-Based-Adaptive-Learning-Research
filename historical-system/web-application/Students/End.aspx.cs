using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Windows.Forms;

public partial class Students_End : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string strUserID = User.Identity.Name;
            string strSqlQuery = "SELECT * FROM V_Grade where UserName = '" + strUserID + "' and Module_ID = " + "'1'";
            string connetionString = null;
            SqlConnection cnn;
            connetionString = "Data Source=YOUR_SERVER;Initial Catalog=ExaminationSystem;Integrated Security=True";
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();

                SqlCommand command = new SqlCommand(strSqlQuery, cnn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            TxtQResult.Text = reader["Grade"].ToString();
                        }
                    }
                }
                cnn.Close();

                string strState = (string)(Session["QState"]);

                if (strState == "Improve")
                {
                    CmdImpove.Visible = false;
                    TextBox1.Visible = false;
                    CmdCancel.Text = "End";
                }
                else
                {
                    CmdImpove.Visible = true;
                    TextBox1.Visible = true;
                    CmdCancel.Text = "Cancel";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! Error : " + ex);
            }
            
        }
    }
    protected void CmdImprove_Click(object sender, EventArgs e)
    {
        
        if (Response.IsClientConnected)
        {
            // If still connected, redirect
            // to another page. 
            Session["QState"] = "Improve";
            Response.Redirect("QustionContent.aspx", false);
        }
        else
        {
            // If the browser is not connected
            // stop all response processing.
            Response.End();
        }
    }
    protected void TxtQResult_TextChanged(object sender, EventArgs e)
    {

    }
    protected void CmdCancel_Click(object sender, EventArgs e)
    {
        if (Response.IsClientConnected)
        {
            // If still connected, redirect
            // to another page. 
            Session["QState"] = "Improve";
            Response.Redirect("~/Default.aspx", false);
        }
        else
        {
            // If the browser is not connected
            // stop all response processing.
            Response.End();
        }

    }
}