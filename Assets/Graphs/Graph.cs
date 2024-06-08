using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Graph 
{
    // each graph has List of edges
    List<Edge> edges = new List<Edge>();
    List<Node> nodes = new List<Node>();
    public List<Node> pathList = new List<Node>();//will be populated by the A* algorithm
    public Graph() {}

    //2 methods used to add edges and nodes
    public void AddNode(GameObject ID)
    {
        Node node = new Node(ID);
        nodes.Add(node);
    }
    //Astar search method
    public void AddEdge(GameObject fromNode, GameObject toNode) //Astar method
    {
        Node from = FindNode(fromNode); //
        Node to = FindNode(toNode);

        if(from != null && to != null)
        {
            Edge e = new Edge(from,to);
            edges.Add(e);
            from.edgeList.Add(e);
        }
    }
    public Node FindNode(GameObject ID)
    {
        foreach (Node n in nodes)
        {
            if(n.getID() == ID)
                return n;
        }
        return null;
    }

    public bool Astar(GameObject startID, GameObject endID)
    {
        if (startID == endID)
        {
            pathList.Clear();
            return false;
        }
        
        Node start = FindNode(startID);
        Node end = FindNode(endID);

        if (start == null || end == null)
        {
            return false;
        }
        List<Node> open = new List<Node>();
        List<Node> close = new List<Node>();
        
        float tentative_g_score = 0; //next position on the graph
        bool tentative_is_better;
        start.g = 0; //how far we have already come 
        start.h = distance(start,end); // the distance to the end
        start.f = start.h;

        open.Add(start);
        while(open.Count >0)
        {
            int i = lowestF(open);
            Node thisNode = open[i];
            if(thisNode.getID() == endID)
            {   // to find the path from the end to the beginning
                ReconstructPath(start,end);
                return true;
            }
            open.RemoveAt(i);
            close.Add(thisNode);
            Node neighbor; //start to explore new Nodes
            foreach(Edge e in thisNode.edgeList)
            {
                neighbor = e.endNode;

                if(close.IndexOf(neighbor) > -1)
                    continue; //continue to look for new node

                tentative_g_score = thisNode.g + distance(thisNode, neighbor);
                if (open.IndexOf(neighbor) == -1)
                {
                    open.Add(neighbor);
                    tentative_is_better = true;
                }
                else if (tentative_g_score < neighbor.g)
                {
                    tentative_is_better = true;
                }
                else
                {
                    tentative_is_better = false;
                }

                if (tentative_is_better)
                {
                    neighbor.cameFrom = thisNode; // plotted to the end node but now need to plot backward
                    neighbor.g = tentative_g_score;
                    neighbor.h = distance(thisNode, end);
                    neighbor.f = neighbor.g + neighbor.h;
                }
            }
        }
        return false; 

    }
    
    public void ReconstructPath(Node startID, Node endID)
    {
        pathList.Clear();
        pathList.Add(endID);

        var p = endID.cameFrom;
        while (p != startID && p != null)
        {
            pathList.Insert(0, p);
            p = p.cameFrom;
        }
        pathList.Insert(0,startID);
    }

    //heuristic method
    float distance(Node a, Node b)
    {
        return(Vector3.SqrMagnitude(a.getID().transform.position - b.getID().transform.position));
    }
    //need another algorithm to find the shortest distance 
    int lowestF(List<Node> l)
    {   
        float lowestf = 0;
        int count = 0;
        int iteratorCount = 0;

        lowestf = l[0].f;
        for (int i = 1; i < l.Count; i++)
        {   
            if (l[i].f < lowestf)
            {   
                lowestf = l[i].f;
                iteratorCount = count;
            }
            count++;
        }
        return iteratorCount; 
    }
}
