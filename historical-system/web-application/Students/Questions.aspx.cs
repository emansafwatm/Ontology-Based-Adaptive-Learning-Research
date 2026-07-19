using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Students_Questions : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }



    protected void LinkButton1_Click(object sender, EventArgs e)
    {

    }
    protected void StartCmd_Click(object sender, EventArgs e)
    {

        if (Response.IsClientConnected)
        {
            // If still connected, redirect
            // to another page. 
            Session["QState"] = "New";
            Response.Redirect("QustionContent.aspx", false);
           // Session["AdpQN"] = 0;
        }
        else
        {
            // If the browser is not connected
            // stop all response processing.
            Response.End();
        }
    }
}