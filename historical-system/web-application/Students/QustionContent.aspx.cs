using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;


using System.Windows.Forms;

public partial class Students_QustionContent : System.Web.UI.Page
{
    QuestionsList QList; 
    int QNo;
    protected void Page_Load(object sender, EventArgs e)
    {
        string QBody = null;
        string ChoiceA = null;
        string ChoiceB = null;
        string ChoiceC = null;
        string ChoiceD = null;
        string Solution = null;
        if (!IsPostBack)
        {
            string strState = (string)(Session["QState"]);

            if (strState == "New")
            {
                QList = new QuestionsList();
                QList.getQuestion();
            }
            else if (strState == "Improve")
            {
                QList = (QuestionsList)(Session["QList"]);
                QList.getadaptiveQuestion();
            }

            QNo = QList.intQNo;
            QBody = QList.strQBode;
            ChoiceA = QList.strA;
            ChoiceB = QList.strB;
            ChoiceC = QList.strC;
            ChoiceD = QList.strD;
            Solution = QList.strSolution;
            lblSolution.Visible = true;
            lblSolution.Text = QList.strSolution;
            
            TxtQBody.Text = QNo + "- " + QBody;
            ChoicesList.Items[0].Text = ChoiceA;
            ChoicesList.Items[1].Text = ChoiceB;
            ChoicesList.Items[2].Text = ChoiceC;
            ChoicesList.Items[3].Text = ChoiceD;
            // HttpContext.Current.Items.Add("QList", QList);
            Session["QList"] = QList;
            
        }
    }

    protected void CmdNext_Click(object sender, EventArgs e)
    {
        string strAnswer;
        if (ChoicesList.SelectedIndex >= 0)
        {
            ListItem mylist = ChoicesList.SelectedItem;
            strAnswer = mylist.Value;

            QuestionsList QList = (QuestionsList)(Session["QList"]);
            string strState = (string)(Session["QState"]);


            // Store Student Data
            string strUserID = User.Identity.Name;
            MembershipUser mu = Membership.GetUser(strUserID);
            string userId = mu.ProviderUserKey.ToString();

            if (strState == "New")
            {
                QList.StoreResults(strAnswer, userId);
                QList.AdjustLevel(strAnswer);
                if (QList.Concepts.Count == 0)
                {
                    if (Response.IsClientConnected)
                    {
                        // If still connected, redirect to End Exam page.
                        int inttemp = QList.getintRwLstIndx();
                        lblSolution.Visible = true;
                        lblSolution.Text = inttemp.ToString();
                        MessageBox.Show(inttemp.ToString(), "Solution", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        Response.Redirect("End.aspx", false);

                    }
                    else
                    {
                        // If the browser is not connected stop all response processing.
                        Response.End();
                    }
                }
                else
                {
                    QList.NextQuestion(strAnswer);
                    //QNo = QList.intQNo;
                    //string QBody = QList.strQBode;
                    //string ChoiceA = QList.strA;
                    //string ChoiceB = QList.strB;
                    //string ChoiceC = QList.strC;
                    //string ChoiceD = QList.strD;
                    //string Solution = QList.strSolution;

                    //TxtQBody.Text = QNo + "- " + QBody;

                    //ChoicesList.Items[0].Text = ChoiceA;
                    //ChoicesList.Items[1].Text = ChoiceB;
                    //ChoicesList.Items[2].Text = ChoiceC;
                    //ChoicesList.Items[3].Text = ChoiceD;
                }
            }
            else if (strState == "Improve")
            {
                QList.StoreAdaptiveResults(strAnswer, userId);
                if( QList.strSolution == strAnswer)
                {
                    QList.AddToList(strAnswer);
                }
                if (QList.intLstCntr == 0)
                {
                    if (Response.IsClientConnected)
                    {
                        // If still connected, redirect to End Exam page. 
                        Response.Redirect("End.aspx", false);
                    }
                    else
                    {
                        // If the browser is not connected stop all response processing.
                        Response.End();
                    }
                }
                else
                {
                    //QList.AddToList(strAnswer);
                    QList.getadaptiveQuestion();
                }
            }
            QNo = QList.intQNo;
            string QBody = QList.strQBode;
            string ChoiceA = QList.strA;
            string ChoiceB = QList.strB;
            string ChoiceC = QList.strC;
            string ChoiceD = QList.strD;
            string Solution = QList.strSolution;
            lblSolution.Visible = true;
            lblSolution.Text = QList.strSolution;
            TxtQBody.Text = QNo + "- " + QBody;

            ChoicesList.Items[0].Text = ChoiceA;
            ChoicesList.Items[1].Text = ChoiceB;
            ChoicesList.Items[2].Text = ChoiceC;
            ChoicesList.Items[3].Text = ChoiceD;

            ChoicesList.ClearSelection();

        }
        else
        {
            return; //exit this event
        }
    }

}