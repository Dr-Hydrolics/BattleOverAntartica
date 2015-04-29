using UnityEngine;
using System.Collections;


public class cargoShip : movement
{

   
    GameObject camera;
    GameObject temp;
    // Use this for initialization
    
     void Start()
    {
        base.Start();
        temp = new GameObject();

        maxSpeed = 200f;
        GameObject terrain = GameObject.FindGameObjectWithTag("terrain");
        temp.transform.position = new Vector3(terrain.transform.position.x + 5000, terrain.transform.position.y+500, terrain.transform.position.z + 5000);
        seekTarget = temp;
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        rotateEnabled = true;
        arriveEnabled = true;
	

    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (arriveEnabled) {
            camera.transform.position = transform.position + new Vector3(-100, 50, -100);
            camera.GetComponent<Camera>().seekTarget = GameObject.FindGameObjectWithTag("cargoShip");
        }
      


    }
    void rotate(Vector3 seekTarget)
    {
        base.rotate(seekTarget);
       


    }
  

}
