using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWayPoints : MonoBehaviour //Must be Mono b/c Unity will run it
{
    Transform goal;
    float speed = 5.0f;
    float accuracy = 2.0f;
    float rotationSpeed =  2.0f;

    public GameObject WPManager; 
    GameObject[] wps;
    GameObject currentNode;
    int currentWP = 0;
    Graph g;

    // Start is called before the first frame update
    void Start()
    {
        wps = WPManager.GetComponent<WPManager>().waypoints;
        g = WPManager.GetComponent<WPManager>().graph;
        currentNode = wps[0]; // where the tank is starting from 
        // commented out when UI button was added must be to use buttons
        //Invoke ("GoToFactory", 2); // give time to populate the waypoints within the manager - the last point is where it will stop

    }
    //directives for tank
    public void GoToRock()
    {
        g.Astar(currentNode,wps[0]);
        currentWP = 0; ////resets to a new path so start at 0 when arrive at new wp -counting/pointing the which node you are currently moving toward
    }
    public void GoToBarracks()
    {
        g.Astar(currentNode,wps[1]);
        currentWP = 0; //resets to a new path so start at 0 when arrive at new wp
    }
    public void GoToRuin()
    {
        g.Astar(currentNode,wps[2]);
        currentWP = 0; //resets to a new path so start at 0 when arrive at new wp
    }
    public void GoToOilField()
    {
        g.Astar(currentNode,wps[3]);
        currentWP = 0; //resets to a new path so start at 0 when arrive at new wp
    }
    public void GoToFactory()
    {
        g.Astar(currentNode,wps[5]);
        currentWP = 0; //resets to a new path so start at 0 when arrive at new wp
    }
    public void GoToHelipad()
    {
        g.Astar(currentNode,wps[7]);
        currentWP = 0; //resets to a new path so start at 0 when arrive at new wp
    }
    public void GoToStart()
    {
        g.Astar(currentNode,wps[8]);
        currentWP = 0; //resets to a new path so start at 0 when arrive at new wp
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if(g.pathList.Count == 0 || currentWP == g.pathList.Count) 
            return; 

        if(Vector3.Distance(g.pathList[currentWP].getID().transform.position, this.transform.position) < accuracy)
        {
            currentNode = g.pathList[currentWP].getID(); //current node updated and sets the starting path
            currentWP++;
        }
        if (currentWP < g.pathList.Count)
        {
            goal = g.pathList[currentWP].getID().transform;
            Vector3 lookAtGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z); // keep gameobject level
            Vector3 direction = lookAtGoal - this.transform.position;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);
            this.transform.Translate(0,0, speed * Time.deltaTime);
        }
    }
}
