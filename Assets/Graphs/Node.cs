using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
    public List<Edge> edgeList = new List<Edge>();
    public Node path = null;
    GameObject ID; //the gameobject at the path location
    //public float xPosition; //commented out after A* added to Graph.cs
    //public float yPosition;
    //public float zPosition;

    public float f,g,h;
    public Node cameFrom; 
    public Node(GameObject i)
    {   
        ID = i;
       /* xPosition = i.transform.position.x; //commented out after A* added to Graph.cs
        yPosition = i.transform.position.y;
        zPosition = i.transform.position.z;*/
        path = null;
    }
    public GameObject getID() //used to compare against other nodes later on
    {
        return ID;
    }
}
