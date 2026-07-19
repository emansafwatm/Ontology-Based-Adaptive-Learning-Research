using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Data.SqlClient;

//using System.Data.SqlClient.SqlConnection;

/// <summary>
/// Summary description for QuestionsList
/// </summary>
public class QuestionsList
{
    public int intQLvl, intQNo;
    public Queue<string> Concepts = new Queue<string>();
    public string strQID, strQBode, strA, strB, strC, strD, strSolution;
    public string strSqlQuery;
    public string connetionString;
    public string[,] strAdptvLst;
    public int intRwLstIndx;
    public int intClmLstIndx;
    public int intLstCntr;
    string strConcept;
    SqlConnection cnn;

    public QuestionsList()
	{
        intQLvl = 2;
        intQNo = 1;
        OntologyClass ont = new OntologyClass("PATH_TO_ONTOLOGY\EnglishFinancilaAccounting3.owl");
        //OntologyClass ont = new OntologyClass("PATH_TO_ONTOLOGY\EnglishFinancilaAccounting3.owl");
        ont.setURI();
        //ont.ReadOntology("الحسابات");
        //ont.setCounts("Accounts");                //test                                
        /// ont.ReadOntology("Accounts");       /// Set start point 
        ont.ReadOntology("Financial_Accounting");       /// Set start point 
        Concepts = ont.ConceptsQueue;
        strAdptvLst = new string[Concepts.Count*2, 4 ];
        intRwLstIndx = 0;
        intClmLstIndx = 0;
        intLstCntr = 0;
        connetionString = null;

        //if (strState == "New")
        //{
        //    getQuestion();
        //}
        //else if (strState == "Improve")
        //{
        //    getadaptiveQuestion();
        //}
        
    }
    public void NextQuestion(string strAnsewr)
    {
        //AdjustLevel(strAnsewr);
       intQNo++;
       if (Concepts.Count == 0)
       { 
       }
       else
       {
           getQuestion();
       }
       
    }
    public void AdjustLevel(string strAnsewr)
    {
        if (strAnsewr == strSolution)
        {
            if (intQLvl == 3)
            {
            }
            else
            {
                intQLvl++;
                strAdptvLst[intRwLstIndx, 0] = intQNo.ToString();
                strAdptvLst[intRwLstIndx, 1] = intQLvl.ToString();
                strAdptvLst[intRwLstIndx, 2] = strConcept;
                strAdptvLst[intRwLstIndx, 3] = (intQLvl - 1).ToString();
                intRwLstIndx++;
                intLstCntr++;
            }
        }
        else
        {
            if (intQLvl == 1)
            {

            }
            else
            {
                intQLvl--;
                strAdptvLst[intRwLstIndx, 0] = intQNo.ToString();
                strAdptvLst[intRwLstIndx, 1] = intQLvl.ToString();
                strAdptvLst[intRwLstIndx, 2] = strConcept;
                strAdptvLst[intRwLstIndx, 3] = (intQLvl + 1).ToString();
                intRwLstIndx++;
                intLstCntr++;
            }
        }
    }
    public void getQuestion()
    {
        strConcept = Concepts.Dequeue();
        //string strSqlQuery = "SELECT * FROM v_Question where Concept = '" + strConcept + "' and Level_ID = " + intQLvl;
        //string connetionString = null;
        strSqlQuery = "SELECT * FROM v_Question where Concept = '" + strConcept + "' and Level_ID = " + intQLvl;
        
        //SqlConnection cnn;
        connetionString = "Data Source=YOUR_SERVER;Initial Catalog=ExaminationSystem;Integrated Security=True";
        cnn = new SqlConnection(connetionString);
        try
        {
            cnn.Open();

            //     MessageBox.Show ("Connection Open ! ");
            SqlCommand command = new SqlCommand(strSqlQuery, cnn);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        strQID = reader["Q_ID"].ToString();
                        strQBode = reader["Body"].ToString();
                        strA = reader["A"].ToString();
                        strB = reader["B"].ToString();
                        strC = reader["C"].ToString();
                        strD = reader["D"].ToString();
                        strSolution = reader["Solution"].ToString();
                 //       MessageBox.Show(strSolution, "Solution", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    }
                }
                else
                {
                    MessageBox.Show("No Record selected", "Warning",MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
            cnn.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Can not open connection ! Error : " + ex);
        }
    }
    public void getadaptiveQuestion()
    {
        intQNo = Convert.ToInt32(strAdptvLst[intClmLstIndx, 0]);
        intQLvl = Convert.ToInt32(strAdptvLst[intClmLstIndx, 1]);
        strConcept = strAdptvLst[intClmLstIndx, 2];
        
        strSqlQuery = "SELECT * FROM v_Question where Concept = '" + strConcept + "' and Level_ID = " + intQLvl;

        connetionString = "Data Source=YOUR_SERVER;Initial Catalog=ExaminationSystem;Integrated Security=True";
        cnn = new SqlConnection(connetionString);
        try
        {
            cnn.Open();

            //     MessageBox.Show ("Connection Open ! ");
            SqlCommand command = new SqlCommand(strSqlQuery, cnn);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        strQID = reader["Q_ID"].ToString();
                        strQBode = reader["Body"].ToString();
                        strA = reader["A"].ToString();
                        strB = reader["B"].ToString();
                        strC = reader["C"].ToString();
                        strD = reader["D"].ToString();
                        strSolution = reader["Solution"].ToString();
                        
                        //MessageBox.Show(strSolution, "Solution", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    }
                }
            }
            intClmLstIndx++;
            intLstCntr--;
            cnn.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Can not open connection ! Error : " + ex);
        }
    }
    public void StoreResults(string strAnswer, string strUserID)
        {
        int intGrade = 0;
        string strModule = "1";
        if (strAnswer == strSolution)
        {
            intGrade = 1 * intQLvl;
        }
        else
        {
            intGrade = 0;
        }
        //SqlConnection cnn;
        connetionString = "Data Source=YOUR_SERVER;Initial Catalog=ExaminationSystem;Integrated Security=True";
        cnn = new SqlConnection(connetionString);

        strSqlQuery = "INSERT INTO Behavior VALUES ('" + strModule + "','" + strQID + "','" + strAnswer + "','" + DateTime.Now + "'," + intGrade + ",'" + strUserID + "'," + intQNo + ")";
                        
        try
        {
            cnn.Open();

            SqlCommand command = new SqlCommand(strSqlQuery, cnn);
            command.ExecuteNonQuery();

            strSqlQuery = "INSERT INTO Result VALUES ('" + strModule + "','" + DateTime.Now + "'," + intGrade + ",'" + DateTime.Now + "','" + strUserID + "'," + intQNo + ")";
            SqlCommand command2 = new SqlCommand(strSqlQuery, cnn);
            command2.ExecuteNonQuery();

            cnn.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Can not open connection ! Error : " + ex);
        }

    }

    public void StoreAdaptiveResults(string strAnswer, string strUserID)
    {
        int intGrade = 0;
        string strModule = "1";
        if (strAnswer == strSolution)
        {
            intGrade = 1 * intQLvl;
        }
        else
        {
            intGrade = 0;
        }
        //SqlConnection cnn;
        connetionString = "Data Source=YOUR_SERVER;Initial Catalog=ExaminationSystem;Integrated Security=True";
        cnn = new SqlConnection(connetionString);

        strSqlQuery = "INSERT INTO Behavior VALUES ('" + strModule + "','" + strQID + "','" + strAnswer + "','" + DateTime.Now + "'," + intGrade + ",'" + strUserID + "'," + intQNo + ")";

        try
        {
            cnn.Open();

            SqlCommand command = new SqlCommand(strSqlQuery, cnn);
            command.ExecuteNonQuery();

            strSqlQuery = "UPDATE Result SET  Grade = " + intGrade + "Where Module_ID = '" + strModule + "' and UserId = '" + strUserID + "' and QNo = " + intQNo ;
            SqlCommand command2 = new SqlCommand(strSqlQuery, cnn);
            command2.ExecuteNonQuery();

            cnn.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Can not open connection ! Error : " + ex);
        }

    }
    public void AddToList(string strAnsewr)
    {
        int intOldLvl = Convert.ToInt32(strAdptvLst[intClmLstIndx-1, 3]);
        if (strAnsewr == strSolution)
        {
            if (intQLvl == 3)
            {
            }
            else
            {
                intQLvl++;
                if (intOldLvl == intQLvl)
                {
                }
                else
                {
                    strAdptvLst[intRwLstIndx, 0] = intQNo.ToString();
                    strAdptvLst[intRwLstIndx, 1] = intQLvl.ToString();
                    strAdptvLst[intRwLstIndx, 2] = strConcept;
                    strAdptvLst[intRwLstIndx, 3] = (intQLvl - 1).ToString();
                    intRwLstIndx++;
                    intLstCntr++;
                }
            }
        }
        else
        {
            if (intQLvl == 1)
            {

            }
            else
            {
                intQLvl--;
                if (intOldLvl == intQLvl)
                {
                }
                else
                {
                    strAdptvLst[intRwLstIndx, 0] = intQNo.ToString();
                    strAdptvLst[intRwLstIndx, 1] = intQLvl.ToString();
                    strAdptvLst[intRwLstIndx, 2] = strConcept;
                    strAdptvLst[intRwLstIndx, 3] = (intQLvl + 1).ToString();
                    intRwLstIndx++;
                    intLstCntr++;
                }
            }
        }
    }
    public int getintRwLstIndx()
    {
        return intRwLstIndx;
    }
}