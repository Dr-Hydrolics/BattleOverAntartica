using UnityEngine;
using System.Collections;

public class TariMain : movement {

	// Use this for initialization
    GameObject pro,camera;
	void Start () {
        base.Start();
        maxSpeed = 100;
        seekTarget = GameObject.FindGameObjectWithTag("cargoShip");
        pro = GameObject.FindGameObjectWithTag("promethus");
        camera = GameObject.FindGameObjectWithTag("MainCamera");
	
	}
	
	// Update is called once per frame
	void Update () {
        base.Update();
        if (pro.GetComponent<MovementPromethus>().propart == true)
        {
            pursuitEnabled = true;
           
           

        }
    }
        public virtual void placecameraTari(){
        camera.transform.position = transform.position + new Vector3(100, 0,0 );
        camera.GetComponent<Camera>().seekTarget = GameObject.FindGameObjectWithTag("MainTari");
        }
       
        
	
	}

