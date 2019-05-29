using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public float maxVelocity = 5f, maxDistance = 5f;
    protected NavMeshAgent agent;
    public Vector3 velocity;
    protected SteeringBehaviour[] behaviours;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        behaviours = GetComponents<SteeringBehaviour>();
    }

    private void Update()
    {
        CalculateForce();
    }
    public virtual Vector3 CalculateForce()
    {
        //create a result Vector3
        //set force to zero
        Vector3 force = Vector3.zero;

        //loop through all behaviours
        foreach (var behaviour in behaviours)
        {
            //apply force to behaviour
            force += behaviour.GetForce() * behaviour.weighting;

            if (force.magnitude > maxVelocity)
            {

                force = force.normalized * maxVelocity;
                //exits loop
                break;
            }
        }

        velocity += force * Time.deltaTime;
        //limit the total velocity to our max velocity if it exceeds
        if (velocity.magnitude > maxVelocity)
        {
            velocity = velocity.normalized * maxVelocity;
        }
        //set velocity to velocity normalized x max velocity

        //sample destination for NavMeshAgent

        if (velocity.magnitude > 0)
        {
            Vector3 pos = transform.position + velocity * Time.deltaTime;
            NavMeshHit hit;

            if (NavMesh.SamplePosition(pos, out hit, maxDistance, -1))
            {
                agent.SetDestination(hit.position);
            }
            // SET pos to current (position) + velocity x delta
            // IF NavMesh SamplePosition within NavMesh
            //   SET agent destination to hit position
        }
        //return force
        return force;
    }
}

