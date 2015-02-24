using UnityEngine;
using System.Collections;


public class MovementPromethus : MonoBehaviour {
    
    public float maxSpeed, mass;
    public Vector3 velocity, force, seekTarget,acceleration,direction;
    public bool seekEnabled, rotateEnabled,pursuitEnabled;
    Quaternion targetRotation;
    public GameObject cargoShip;
    public Vector3 pos;
    Camera camera;
    // Use this for initialization
	void Start () {
        
        cargoShip = GameObject.FindGameObjectWithTag("cargoShip");
        mass = 1;
        velocity = Vector3.zero;
        force = Vector3.zero;
        acceleration = Vector3.zero;
        maxSpeed = 500.0f;
        transform.position = new Vector3(0, 1000, 0);
        camera = gameObject.GetComponentInChildren<Camera>();
	
	}
	
	// Update is called once per frame
	void Update () {
        camera.transform.position = transform.position + new Vector3(-100, 100, -100);
        
        force = Vector3.zero;
        pos = transform.position;
        if (seekEnabled)
        {
            
            force += Seek(cargoShip.transform.position);
            
        }
        if (rotateEnabled) {
            rotate(cargoShip.transform.position);
            rotateCamera(transform.position); 
        }
        if (pursuitEnabled) {
            seekTarget = cargoShip.transform.position + new Vector3(0, 100, 0);
            force += Pursuit(seekTarget);
        }
        velocity += acceleration * Time.deltaTime;
        acceleration = force / mass;
        Vector3.ClampMagnitude(velocity, maxSpeed);
        transform.position += velocity * Time.deltaTime;
      
       
       
	}
    void rotateCamera(Vector3 seekTarget)
    {
        direction = (seekTarget - transform.position);
        targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        camera.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, (Time.deltaTime * 1));


    }
    void rotate(Vector3 seekTarget)
    {
        direction = (seekTarget - transform.position);
        targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, (Time.deltaTime * 1));
       
    
    } 

    Vector3 Seek(Vector3 seekTarget)
    {
        Vector3 newvol = transform.position - seekTarget;
        newvol.Normalize();
        newvol = newvol* maxSpeed;
        return (newvol - velocity);
    }

    public Vector3 Arrive(Vector3 target)
    {
        Vector3 toTarget = target - transform.position;

        float slowingDistance = 8.0f;
        float distance = toTarget.magnitude;
        if (distance == 0.0f)
        {
            return Vector3.zero;
        }
        const float DecelerationTweaker = 10.3f;
        float ramped = maxSpeed * (distance / (slowingDistance * DecelerationTweaker));

        float clamped = Mathf.Min(ramped, maxSpeed);
        Vector3 desired = clamped * (toTarget / distance);
        return desired - velocity;
    }
    Vector3 Pursuit(Vector3 target)
    {

        float dist = (target - transform.position).magnitude;

        float lookAhead = (dist / maxSpeed);

        target = target + (lookAhead *new Vector3(10,0,0));

        return Arrive(target);
    }
}
