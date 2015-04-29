using UnityEngine;
using System.Collections;

public class TariChildren : movement {
    GameObject pro;
	// Use this for initialization
	void Start () {
        base.Start();
        pro = GameObject.FindGameObjectWithTag("promethus");
        seekTarget = this.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        base.Update();
        if (pro.GetComponent<MovementPromethus>().propart == true)
        {
            pursuitEnabled = true;
        }
	    
	}
}
