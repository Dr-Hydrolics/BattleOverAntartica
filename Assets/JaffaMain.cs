using UnityEngine;
using System.Collections;

public class JaffaMain : movement {
    GameObject pro, camera,tariMain;
	// Use this for initialization
	void Start () {
        base.Start();
        maxSpeed = 50;
        seekTarget = GameObject.FindGameObjectWithTag("cargoShip");
        pro = GameObject.FindGameObjectWithTag("promethus");
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        tariMain = GameObject.FindGameObjectWithTag("MainTari");
	}
    float clock;
	// Update is called once per frame
	void Update () {
        base.Update();
       
        if (pro.GetComponent<MovementPromethus>().propart == true)
        {
            clock += Time.deltaTime;
            pursuitEnabled = true;

            if (clock > 10 || clock>20)
                placecameraTari();
            else
                placecameraJaffa();            
        }
	
	}
    void placecameraJaffa()
    {
        camera.transform.position = transform.position + new Vector3(-100, 0, 0);
        camera.GetComponent<Camera>().seekTarget = GameObject.FindGameObjectWithTag("mainJaffa");
    }
    void placecameraTari()
    {
        camera.transform.position = tariMain.transform.position + new Vector3(0, 0, 100);
        camera.GetComponent<Camera>().seekTarget = tariMain;
    }
}
