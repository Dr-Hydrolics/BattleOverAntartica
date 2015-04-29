using UnityEngine;
using System.Collections;


public class MovementPromethus : movement {
    
    public GameObject cargoShip;
    public bool propart = false;
    bool changed = false;
    GameObject camera,temp;
    // Use this for initialization
	void Start () {
        base.Start();
        
        maxSpeed = 500;
        temp = new GameObject();
        cargoShip = GameObject.FindGameObjectWithTag("cargoShip");
        seekTarget = temp;
       
      
	}
	
	// Update is called once per frame
	void Update () {
        base.Update();
        
        
        if ((cargoShip.GetComponent<cargoShip>().end== true&& (end == false))) {
            camera = GameObject.FindGameObjectWithTag("MainCamera");
            temp.transform.position = GameObject.FindGameObjectWithTag("cargoShip").transform.position + new Vector3(0,100,0) ;
            camera.GetComponent<Camera>().seekTarget = GameObject.FindGameObjectWithTag("promethus");
            camera.transform.position = transform.position + new Vector3(-100, 100, 200);
            arriveEnabled = true;
            
            rotateEnabled = true;
            changed = true;
            
        }
         if (end)
        {
           
            propart = true;
        }
       
	}
    void rotateCamera(Vector3 seekTarget)
    {
        direction = (seekTarget - transform.position);
        targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        camera.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, (Time.deltaTime * 1));


    }
    void rotate(Vector3 seekTarget)
    {
        base.rotate(seekTarget);
        rotateCamera(seekTarget);

        transform.rotation = new Quaternion(180, 180, 0, 0);
       
    
    } 


}
