using UnityEngine;
using System.Collections;


public class cargoShip : MonoBehaviour
{

    public float maxSpeed, mass;
    public Vector3 velocity, force, seekTarget, acceleration, direction;
    public bool seekEnabled, rotateEnabled, pursuitEnabled;
    Quaternion targetRotation;
    Camera camera;
   
    public Vector3 pos;
    // Use this for initialization
    void Start()
    {

       
        mass = 1;
        velocity = Vector3.zero;
        force = Vector3.zero;
        acceleration = Vector3.zero;
        maxSpeed = 500.0f;
        GameObject terrain = GameObject.FindGameObjectWithTag("terrain");
        seekTarget = new Vector3(terrain.transform.position.x + 5000, terrain.transform.position.y+500, terrain.transform.position.z + 5000);
        camera = gameObject.GetComponentInChildren<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        camera.transform.position = transform.position +new Vector3(-100,50,-100);
      
        force = Vector3.zero;
        pos = transform.position;
        if (seekEnabled)
        {

            force += Seek(seekTarget);

        }
        if (rotateEnabled)
        {
            rotate(seekTarget);
            rotateCamera(transform.position); 
        }
        if (pursuitEnabled)
        {
             force += Pursuit(seekTarget);
             
        }
        velocity += acceleration * Time.deltaTime;
        acceleration = force / mass;
        Vector3.ClampMagnitude(velocity, maxSpeed);
        transform.position += velocity * Time.deltaTime;



    }
    void rotate(Vector3 seekTarget)
    {
        direction = (seekTarget - transform.position);
        targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, (Time.deltaTime * 1));


    }
    void rotateCamera(Vector3 seekTarget)
    {
        direction = (seekTarget - transform.position);
        targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        camera.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, (Time.deltaTime * 1));


    }

    Vector3 Seek(Vector3 seekTarget)
    {
        Vector3 newvol = transform.position - seekTarget;
        newvol.Normalize();
        newvol = newvol * maxSpeed;
        return (newvol - velocity);
    }

    public Vector3 Arrive(Vector3 target)
    {
        Vector3 toTarget = target - transform.position;

        float slowingDistance = 8.0f;
        float distance = toTarget.magnitude;
        if (distance <1.0f)
        {
            return new Vector3(0,0,0);
            rotateEnabled = false;
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

        //target = target + (lookAhead * new Vector3(10, , 0));

        return Arrive(target);
    }
}
