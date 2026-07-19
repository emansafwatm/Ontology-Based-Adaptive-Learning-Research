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
/// Summary description for OntologyClass
/// </summary>
public class OntologyClass
    {
        IOwlParser parser = new OwlXmlParser();
        IOwlGraph graph;
        
        public  Queue<string> ConceptsQueue = new Queue<string>();
        Queue<string> OntologyQueue = new Queue<string>();
        public string[,] strSetsLst;
        public string[,] strChilCount;
        public int intRwIndx;
        public int intClmnIndx;
        public int intRwIndx2;
        string strURI;
        string filel;
        Queue oldQueue = new Queue();
        Queue SelectedConcepts = new Queue();        
                
        public OntologyClass(string fileName)
        {
            filel = fileName;
            graph = parser.ParseOwl(filel);
            strSetsLst = new string[10, 17];        // to put concepts sets
            strChilCount = new string[50, 2];
        
        }
                
        public void setURI()
        {
            string strTemp;
            char delimiterChars = '#';
            //IOwlParser parser = new OwlXmlParser();
            //IOwlGraph graph = parser.ParseOwl(filel);

            IDictionaryEnumerator nEnumerator = (IDictionaryEnumerator)graph.Nodes.GetEnumerator();
            while (nEnumerator.MoveNext())
            {
                // Get the node from the graph
                OwlNode node = (OwlNode)graph.Nodes[(nEnumerator.Key).ToString()];
                // Cast the node to a OwlClass because we are looking for classes
                OwlClass clsNode = node as OwlClass;
                if ((clsNode != null) && (!clsNode.IsAnonymous())) OntologyQueue.Enqueue(clsNode.ID);

            }
            strTemp = OntologyQueue.Dequeue();
            string[] words = strTemp.Split(delimiterChars);
            strURI = words[0];
        }

        public string GetURI()
        {
            return strURI;
        }

        public void ReadOntology(string strSection)
        {
            try
            {
//                int intSetCount = 0;
                string strStartPoint = strURI + '#' + strSection;
                //strSetsLst = new string[5, 6];        // to put concepts sets
                intRwIndx = 0; intRwIndx2 = 0;    // to set clustring araay indicator to zero
                // Here we will retrieve the enumerator in order to get all the nodes from the file
                IDictionaryEnumerator nEnumerator = (IDictionaryEnumerator)graph.Nodes.GetEnumerator();
                while (nEnumerator.MoveNext())
                {
                    // Get the node from the graph
                    OwlNode node = (OwlNode)graph.Nodes[(nEnumerator.Key).ToString()];
                    // We will cast the node to a OwlClass because we are looking for classes
                    OwlClass clsNode = node as OwlClass;
                    // If clsNode is different from null, then we are dealing with an OwlClass -> OK
                    // If the clsNode is not anonymous, means that we have a class with a proper name -> OK
                    if ((clsNode != null) && (!clsNode.IsAnonymous()))
                    {
                        // So, now we have a good owl-class, we will look for any subClassOf relations (edges)
                        IOwlEdgeList subclassEdges = (IOwlEdgeList)node.ChildEdges["http://www.w3.org/2000/01/rdf-schema#subClassOf"];
                        if (subclassEdges != null)
                        {
                            // We will list all the edges and check if the target of the edge is the class we want to
                            // have as the superclass
                            foreach (OwlEdge s in subclassEdges)
                            {
                                //if (s.ChildNode.ID == "http://www.semanticweb.org/emy/ontologies/2014/7/Financial#الحسابات")
                                if (s.ChildNode.ID == strStartPoint)
                                {
                                    oldQueue.Enqueue(node.ID);
                                    intClmnIndx = 0;
                                    strSetsLst[intRwIndx, intClmnIndx] = node.ID;
                                    strChilCount[intRwIndx2, 0] = node.ID;
                                    strChilCount[intRwIndx2, 1] = "1";
                                    CountClassChildren(node);
                                    intRwIndx++; intRwIndx2++;
                                }
                            }
                        }
                    }
                }
                AdjustCounts(strSection);
                ConceptsSlection CncptSlctn = new ConceptsSlection(strSetsLst, strChilCount,oldQueue.Count);
                CncptSlctn.StartSettting();
                SelectedConcepts = CncptSlctn.QsQueue;
                //ConceptsSlection
                UpdateQueue();
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
               
            }
        }

        private void getClassesChildren(OwlNode SearchNode)
        {
            try
            {
                IDictionaryEnumerator nEnumerator = (IDictionaryEnumerator)graph.Nodes.GetEnumerator();
                while (nEnumerator.MoveNext())
                {
                    OwlNode node = (OwlNode)graph.Nodes[(nEnumerator.Key).ToString()];
                    OwlClass clsNode = node as OwlClass;
                    if ((clsNode != null) && (!clsNode.IsAnonymous()))
                    {
                        // So, now we have a good owl-class, we will look for any subClassOf relations (edges)
                        IOwlEdgeList subclassEdges = (IOwlEdgeList)node.ChildEdges["http://www.w3.org/2000/01/rdf-schema#subClassOf"];
                        if (subclassEdges != null)
                        {
                            // We will list all the edges and check if the target of the edge is the class we want to
                            // have as the superclass
                            foreach (OwlEdge s in subclassEdges)
                            {
                                if (s.ChildNode.ID == SearchNode.ID)
                                {
                                    oldQueue.Enqueue(node.ID);
                                    intClmnIndx++;
                                    strSetsLst[intRwIndx, intClmnIndx] = node.ID;
                                    getClassesChildren(node);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }
        }

        private void UpdateQueue()
        {
            char delimiterChars = '#';
            string strTemp;
                        
            //foreach (string StrConcept in oldQueue)
            foreach (string StrConcept in SelectedConcepts)
            {
                strTemp = StrConcept.Replace('_', ' ');
                string[] words = strTemp.Split(delimiterChars);
                ConceptsQueue.Enqueue(words[1]);
               // ConceptsQueue.Enqueue(StrConcept.Replace('_',' '));             
            }
        }

        public Queue<string> GetConceptsQueue()
        {
            return ConceptsQueue;
        }
        public void setCounts(string strSection)
        {
            try
            {
                string strStartPoint = strURI + '#' + strSection;
                //strSetsLst = new string[5, 6];        // to put concepts sets
                intRwIndx = 0; intRwIndx2 = 0;    // to set clustring araay indicator to zero
                // Here we will retrieve the enumerator in order to get all the nodes from the file
                IDictionaryEnumerator nEnumerator = (IDictionaryEnumerator)graph.Nodes.GetEnumerator();
                while (nEnumerator.MoveNext())
                {
                    // Get the node from the graph
                    OwlNode node = (OwlNode)graph.Nodes[(nEnumerator.Key).ToString()];
                    // We will cast the node to a OwlClass because we are looking for classes
                    OwlClass clsNode = node as OwlClass;
                    // If clsNode is different from null, then we are dealing with an OwlClass -> OK
                    // If the clsNode is not anonymous, means that we have a class with a proper name -> OK
                    if ((clsNode != null) && (!clsNode.IsAnonymous()))
                    {
                        // So, now we have a good owl-class, we will look for any subClassOf relations (edges)
                        IOwlEdgeList subclassEdges = (IOwlEdgeList)node.ChildEdges["http://www.w3.org/2000/01/rdf-schema#subClassOf"];
                        if (subclassEdges != null)
                        {
                            // We will list all the edges and check if the target of the edge is the class we want to
                            // have as the superclass
                            foreach (OwlEdge s in subclassEdges)
                            {
                                //if (s.ChildNode.ID == "http://www.semanticweb.org/emy/ontologies/2014/7/Financial#الحسابات")
                                if (s.ChildNode.ID == strStartPoint)
                                {
                                    oldQueue.Enqueue(node.ID);
                                    intClmnIndx = 0;
                                    strSetsLst[intRwIndx, intClmnIndx] = node.ID;
                                    strChilCount[intRwIndx2, 0] = node.ID;
                                    strChilCount[intRwIndx2, 1] = "1";
                                    CountClassChildren(node);
                                    intRwIndx++; intRwIndx2++;

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }
        }
        private void CountClassChildren(OwlNode SearchNode)
        {
            try
            {
                IDictionaryEnumerator nEnumerator = (IDictionaryEnumerator)graph.Nodes.GetEnumerator();
                while (nEnumerator.MoveNext())
                {
                    OwlNode node = (OwlNode)graph.Nodes[(nEnumerator.Key).ToString()];
                    OwlClass clsNode = node as OwlClass;
                    if ((clsNode != null) && (!clsNode.IsAnonymous()))
                    {
                        // So, now we have a good owl-class, we will look for any subClassOf relations (edges)
                        IOwlEdgeList subclassEdges = (IOwlEdgeList)node.ChildEdges["http://www.w3.org/2000/01/rdf-schema#subClassOf"];
                        if (subclassEdges != null)
                        {
                            // We will list all the edges and check if the target of the edge is the class we want to
                            // have as the superclass
                            foreach (OwlEdge s in subclassEdges)
                            {
                                if (s.ChildNode.ID == SearchNode.ID)
                                {
                                    int I = 0;
                                    oldQueue.Enqueue(node.ID);
                                    intClmnIndx++; intRwIndx2++;
                                    strSetsLst[intRwIndx, intClmnIndx] = node.ID;
                                    strChilCount[intRwIndx2, 0] = node.ID;
                                    strChilCount[intRwIndx2, 1] = "1";
                                    //foreach (string value in strChilCount)
                                    for (int N = 0; N < 50; N++)
                                    {
                                        //int x = 0;
                                        if (strChilCount[I, 0] == SearchNode.ID)
                                        {
                                            string strSuper = strChilCount[I, 1];
                                            int intSuper = Convert.ToInt32(strSuper);
                                            intSuper = intSuper + 1;
                                            strChilCount[I, 1] = intSuper.ToString();
                                        }
                                        I++;
                                    }
                                    CountClassChildren(node);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e.GetType());
                
            }
        }
        private void AdjustCounts(string strSection)
        {
            string strStartPoint = strURI + '#' + strSection;
            // Here we will retrieve the enumerator in order to get all the nodes from the file
            IDictionaryEnumerator nEnumerator = (IDictionaryEnumerator)graph.Nodes.GetEnumerator();
            while (nEnumerator.MoveNext())
            {
                // Get the node from the graph
                OwlNode node = (OwlNode)graph.Nodes[(nEnumerator.Key).ToString()];
                // We will cast the node to a OwlClass because we are looking for classes
                OwlClass clsNode = node as OwlClass;
                // If clsNode is different from null, then we are dealing with an OwlClass -> OK
                // If the clsNode is not anonymous, means that we have a class with a proper name -> OK
                if ((clsNode != null) && (!clsNode.IsAnonymous()))
                {
                    // So, now we have a good owl-class, we will look for any subClassOf relations (edges)
                    IOwlEdgeList subclassEdges = (IOwlEdgeList)node.ChildEdges["http://www.w3.org/2000/01/rdf-schema#subClassOf"];
                    if (subclassEdges != null)
                    {
                        // We will list all the edges and check if the target of the edge is the class we want to
                        // have as the superclass
                        foreach (OwlEdge s in subclassEdges)
                        {
                            if (s.ChildNode.ID == strStartPoint)
                            {
                                AdjustChildrenCount(node);
                            }
                         }
                    }
                }
            }
        }
        private void AdjustChildrenCount(OwlNode SearchNode)
        {
            try
            {
                int y = 0; int x = 0;
                IDictionaryEnumerator nEnumerator = (IDictionaryEnumerator)graph.Nodes.GetEnumerator();
                while (nEnumerator.MoveNext())
                {
                    OwlNode node = (OwlNode)graph.Nodes[(nEnumerator.Key).ToString()];
                    OwlClass clsNode = node as OwlClass;
                    if ((clsNode != null) && (!clsNode.IsAnonymous()))
                    {
                        // So, now we have a good owl-class, we will look for any subClassOf relations (edges)
                        IOwlEdgeList subclassEdges = (IOwlEdgeList)node.ChildEdges["http://www.w3.org/2000/01/rdf-schema#subClassOf"];
                        if (subclassEdges != null)
                        {
                            // We will list all the edges and check if the target of the edge is the class we want to
                            // have as the superclass
                            foreach (OwlEdge s in subclassEdges)
                            {
                                if (s.ChildNode.ID == SearchNode.ID)
                                {
                                    y = 0;
                                    for (int M = 0; M < 50; M++)
                                    {
                                        string strChild = strChilCount[y, 0];
                                        if (strChild == node.ID)
                                        {
                                            string strNo = strChilCount[y, 1];
                                            int intCldNo = Convert.ToInt32(strNo);
                                            if (intCldNo > 1)
                                            {
                                                x = 0;
                                                for (int N = 0; N < 50; N++)
                                                {
                                                    string strSuper = strChilCount[x, 0];
                                                    if (strSuper == SearchNode.ID)
                                                    {
                                                        string strSprNr = strChilCount[x, 1];
                                                        int intSprNo = Convert.ToInt32(strSprNr);
                                                        intSprNo = intSprNo + intCldNo - 1;
                                                        strChilCount[x, 1] = intSprNo.ToString();
                                                        goto Found;
                                                    }
                                                    x++;
                                                }
                                            }
                                        }
                                        y++;
                                    }
                                }
                            Found:
                                continue;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }
        }

    }
