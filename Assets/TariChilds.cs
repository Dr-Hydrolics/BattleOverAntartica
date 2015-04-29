using UnityEngine;
using System.Collections;

public class TariChilds :movement
{
    GameObject pro;
    GameObject[] other;
    bool started;
    // Use this for initialization
    void Start()
    {
        maxSpeed = 150;
        started = false;
        base.Start();
        pro = GameObject.FindGameObjectWithTag("promethus");
        seekTarget = GameObject.FindGameObjectWithTag("MainTari");
        other = GameObject.FindGameObjectsWithTag("tari");


       
    }

    // Update is called once per frame
    void Update(){
        base.Update();
        
        float smallestDist = float.MaxValue;
        foreach (GameObject ship in other)
        {
            if ((Vector3.Distance(transform.position ,ship.transform.position)<smallestDist)){
                    smallestDist = Vector3.Distance(transform.position ,ship.transform.position);
                    nearest = ship;
                   
                
            }
        }
        if (Vector3.Distance(transform.position ,nearest.transform.position)>10)
        {
            evadeEnabled = true;
            pursuitEnabled = false;
        }
        else {
            evadeEnabled = false;
            pursuitEnabled = true;
        }
        if ((pro.GetComponent<MovementPromethus>().propart == true)&& (started == false)) {
            pursuitEnabled = true;
            started = true;
           
        }

    }
       
        

    }

