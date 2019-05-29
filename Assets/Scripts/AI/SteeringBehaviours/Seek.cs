using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : SteeringBehaviour
{
    public Transform target;
    public float stoppingDistance;
    public override Vector3 GetForce()
    {
        Vector3 force = Vector3.zero;

        if (target)
        {
            Vector3 desiredForce = target.position - transform.position;

            if (desiredForce.magnitude > stoppingDistance)
            {
                desiredForce = desiredForce.normalized * weighting;
                force = desiredForce - owner.velocity;
            }
        }
        return force;
    }
}
