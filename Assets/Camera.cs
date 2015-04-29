using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

	// Use this for initialization
	Vector3 direction;
     Quaternion targetRotation;
     public GameObject seekTarget;
   
    void Start () {
        seekTarget = new GameObject();
        direction = new Vector3();  
        targetRotation = new Quaternion();
        
	}
	
	// Update is called once per frame
	void Update () {
        
        direction = (seekTarget.transform.position - transform.position);
        targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, (Time.deltaTime * 1));


    
	}
}
