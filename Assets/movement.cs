using UnityEngine;
using System.Collections;


public class movement : MonoBehaviour
{

    public float maxSpeed, mass;
    public Vector3 velocity, force,  acceleration, direction,randomWalkTarget;
    public GameObject seekTarget, nearest;
    public bool seekEnabled, rotateEnabled, pursuitEnabled,arriveEnabled,end,walkEnabled,evadeEnabled;
    public Quaternion targetRotation;
    public Vector3 pos;
    public float dis;
    // Use this for initialization
    public virtual void Start()
    {
        end = false;
        mass = 1;
        velocity = Vector3.zero;
        force = Vector3.zero;
        acceleration = Vector3.zero;
        maxSpeed = 100.0f;
        pos = transform.position;
       

    }

    // Update is called once per frame
    
    public virtual void Update()
    {
        force = Vector3.zero;
       
        if (seekEnabled)
        {
            force += Seek(seekTarget.transform.position);

        }
        if (rotateEnabled)
        {
            rotate(seekTarget.transform.position);
        }
        if (walkEnabled){
            force = RandomWalk();
        }
        if (pursuitEnabled)
        {
            force += Pursuit(seekTarget.transform.position);
        }
        if (arriveEnabled) {
            force += Arrive(seekTarget.transform.position);
        }
        if (evadeEnabled) {
            force += Evade()*10;
        }
        velocity += acceleration * Time.deltaTime;
        acceleration = force / mass;
        Vector3.ClampMagnitude(velocity, maxSpeed);
        transform.position += (velocity * Time.deltaTime);



    }

    public virtual void rotate(Vector3 seekTarget)
    {
        direction = (seekTarget - transform.position);
        targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, (Time.deltaTime * 1));


    }

    public virtual Vector3 Seek(Vector3 seekTarget)
    {
        Vector3 newvol = transform.position - seekTarget;
        newvol.Normalize();
        newvol = newvol * maxSpeed;
        return (newvol - velocity);
    }

    public virtual Vector3 Arrive(Vector3 target)
    {
        Vector3 toTarget = target - transform.position;

        float slowingDistance = 8.0f;
        float distance = toTarget.magnitude;
        dis = distance;

        if (distance < 100f)
        {
            
            rotateEnabled = false;
            arriveEnabled = false;
            end= true;
            velocity = Vector3.zero;
            return Vector3.zero;
        }
        const float DecelerationTweaker = 10.3f;
        float ramped = maxSpeed * (distance / (slowingDistance * DecelerationTweaker));

        float clamped = Mathf.Min(ramped, maxSpeed);
        Vector3 desired = clamped * (toTarget / distance);
        
        return desired - velocity;
    }
    public virtual Vector3 Pursuit(Vector3 target)
    {

        float dist = (target - transform.position).magnitude;

        float lookAhead = (dist / maxSpeed);

        target = target + (lookAhead * new Vector3(10, 0, 0));

        return Arrive(target);
    }
    public virtual Vector3 RandomWalk()
    {
        float dist = (transform.position - randomWalkTarget).magnitude;
        float range = 50;
        if (dist < 20)
        {
            randomWalkTarget.x = UnityEngine.Random.Range(-range, range);
            randomWalkTarget.y = UnityEngine.Random.Range(0, range / 2.0f);
            randomWalkTarget.z = UnityEngine.Random.Range(-range, range);
        }
        return Seek(randomWalkTarget);
    }
    public virtual Vector3 Evade()
        {
            float dist = (nearest.transform.position - transform.position).magnitude;
            float lookAhead = maxSpeed;

            Vector3 targetPos = nearest.transform.position + (lookAhead * nearest.GetComponent<movement>().velocity);
            return Flee(targetPos);
        }
    Vector3 Flee(Vector3 targetPos)
    {
        float panicDistance = 100.0f;
        Vector3 desiredVelocity;
        desiredVelocity = transform.position - targetPos;
        if (desiredVelocity.magnitude > panicDistance)
        {
            //return Vector3.zero;
        }
        desiredVelocity.Normalize();
        desiredVelocity *= maxSpeed;
        return (desiredVelocity - velocity);
    }
    
}
