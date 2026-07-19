using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text;
using System.IO;
using System.Collections;
using OwlDotNetApi;

using System.ComponentModel;
using System.Data;

/// <summary>
/// Summary description for ConceptsSlection
/// </summary>
public class ConceptsSlection
{
    public string[,] strSetsLst;
    public string[,] strChilCount;
    public string[,] strSelectionSet;
    public string[,] strWeight;
    public string[,] strMediatorArray;
    public int intOntolgyCount;
    public Queue QsQueue = new Queue();

    int intTotal;
    int intsetCount;
    int intSetQs; int intTotalQs;
    double dblSetQs; double dblTotalQs; double dblSetCount;
    double dblOntologyCount;
    double dblWeightSum;

    public ConceptsSlection(string[,] strSetsLst2, string[,] strChilCount2, int intOntolgyCount2)
	{
        strSetsLst = new string[10, 17];        // to put concepts sets
        strChilCount = new string[50, 2];
        strSelectionSet = new string[50, 2];
        strWeight = new string[50, 2];
        intOntolgyCount = intOntolgyCount2;
        intTotalQs = 10;
        dblOntologyCount = Convert.ToDouble(intOntolgyCount2);
        dblTotalQs = 10;
        //Array.Copy(source, target, 5);
        Array.Copy(strSetsLst2,strSetsLst,170);
        Array.Copy(strChilCount2, strChilCount, 100);
        Array.Copy(strChilCount, strWeight, 100);
        adjustWeight();
	}
    public void StartSettting()
    {
        string strTemp; double dblTemp = 0; dblWeightSum = 0.0;
        intTotal = 0; int intCounter = 0;

        for (int i=0; i<10;i++)
        {
            intsetCount = 0; dblSetCount = 0;
            intCounter = 0;
            dblWeightSum = 0;
            for (int m = 0;m<17;m++)
            {
                string strTmpConcept = strSetsLst[i, m];
                if (strTmpConcept != null)
                {
                    for (int n = 0; n < 50; n++)
                    {
                        string strSearch = strWeight[n, 0];
                        if (strTmpConcept == strSearch && strTmpConcept != null)
                        {
                            strTemp = strWeight[n, 1];
                            dblTemp = Convert.ToDouble(strTemp);
                            dblWeightSum = dblWeightSum + dblTemp;
                            strSelectionSet[intCounter, 0] = strTmpConcept;
                            strSelectionSet[intCounter, 1] = dblTemp.ToString();
                            intsetCount++; dblSetCount++;
                            intCounter++; break;
                        }
                    }
                }
                else
                    break;
            }
            strMediatorArray = new string[intsetCount, 2];
            Array.Copy(strSelectionSet, strMediatorArray, intsetCount * 2);
            StartSelection();
        }
        Finalize();
    }
    public void adjustWeight()
    {
        double dblWeight = 0.0;

        for (int ii = 0; ii < 50; ii++)
        {
            String strtemp = strWeight[ii, 1];
            if (strtemp != null)
            {
                double dbltemp = Convert.ToDouble(strtemp);
                dblWeight = 1 / dbltemp;
                strtemp = dblWeight.ToString();
                strWeight[ii, 1] = strtemp;
            }
            else
                break;
        }
    }

    public void setQuestionsSet()
    {
         double dblTemp = 0.0;
         dblTemp = Math.Round((Double)(dblTotalQs * dblSetCount / dblOntologyCount));
         intSetQs = Convert.ToInt32(dblTemp);
    }
    public void StartSelection()
    {
        setQuestionsSet();
        if (intsetCount != 1)
        {
            for (int x = 0; x < intSetQs; x++)
            {
                selectConcept();
            }
        }
        else 
        {
            QsQueue.Enqueue(strMediatorArray[0, 0]);
        }
        
    }
    private void selectConcept()
    {
        double dblMediator = 0; string strMediator;
        double dblTemp = 0; double dblPrc = 0.0;
        for (int n = 0; n < intsetCount - 1; n++)
        {
            strMediator = strMediatorArray[n, 1];
            dblTemp = Convert.ToDouble(strMediator);
            dblPrc = dblTemp / dblWeightSum;
            dblMediator = dblMediator + dblPrc;
            strMediatorArray[n, 1] = dblMediator.ToString();
        }
        Random rnd = new Random();
        double dblRoulette = rnd.NextDouble();
        double dblCheck = 0.0;
        for (int r = 0; r < intsetCount ; r++)
        {
            strMediator = strMediatorArray[r, 1];
            dblTemp = Convert.ToDouble(strMediator);
            if (dblRoulette > dblCheck && dblRoulette < dblTemp)
            {
                QsQueue.Enqueue(strMediatorArray[r, 0]); // Add concept to queue
                strMediatorArray[r, 1] = "0"; // Add concept to queue
                for (int x = 0; x < intsetCount - 1; x++)
                {
                    if (strWeight[x, 0] == strMediatorArray[r, 0])
                    {
                        double dblTemp2 = Convert.ToDouble(strWeight[x, 1]);
                        dblWeightSum = dblWeightSum - dblTemp2;
                        break;
                    }
                }
                RemoveConcept();
                break;
            }
            else
            {
                dblCheck = dblTemp;
            }
        }
    }
    public void RemoveConcept()
    {
        string strTemp;
        
        for (int m = 0; m < intsetCount - 1; m++)
        {
            if (strMediatorArray[m , 1] == "0")
            {
                strMediatorArray[m, 0] = strMediatorArray[m + 1, 0];
                strMediatorArray[m, 1] = strMediatorArray[m + 1, 1];
                strMediatorArray[m + 1, 1] = "0";
            }
        }
        for (int m = 0; m < intsetCount - 1; m++)
        {
            strTemp = strMediatorArray[m, 0];
            for (int x = 0; x < 50; x++)
            {
                if (strMediatorArray[m, 0] == strWeight[x, 0])
                {
                    strMediatorArray[m, 1] = strWeight[x, 1];
                    break;
                }
            }
         }
        intsetCount--;
    }
    private void Finalize()
    {
        int No = QsQueue.Count;
        if (intTotalQs < No)
        {
            int intDif = No - intTotalQs;
            for (int t = 0; t < intDif; t++)
            {
                QsQueue.Dequeue();
            }
        }
    }
}