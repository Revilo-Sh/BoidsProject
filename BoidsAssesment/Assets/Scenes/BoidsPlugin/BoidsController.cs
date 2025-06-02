using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.Mathematics;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class BoidsController : MonoBehaviour
{
    //Debug
    public bool ShowGizmos = false;

    //Normal
    public float speed = 5f;
    public float maxforce = 0.5f;
    public float raycastDistance = 5f;
    public float avoidanceForce = 1.5f;

    private Vector3 velocity;


    // Boids 3 Laws Verables
    public BoidsController[] allboids;
    private BoidsController[] neighborsBoids;
    public float SeparationDistanceVal = 5;
    public float neighborsDistanceVal = 10;
    public float aliagmentdistance = 20;
    private float rotSpeed = 180f;
    public float alignmentWeight = 1f;
    public float cohesionWeight = 1f;


    bool ISneighbors;



    private void Start()
    {
        velocity = transform.forward * speed;
    }

    private void Update()
    {
        //WorldCollision();
        AddBoidsObjects();

        Vector3 separationForce = Separation();
        velocity += separationForce;


        Vector3 alignmentForce = Alignment();
        velocity += alignmentForce;

        Vector3 cohesionForce = Cohesion();
        velocity += cohesionForce * cohesionWeight;

        velocity = Vector3.ClampMagnitude(velocity, speed);
        transform.position += transform.forward * speed * Time.deltaTime;
        quaternion toRotation = Quaternion.LookRotation(velocity, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotSpeed * Time.deltaTime);
       
        //transform.position += velocity * speed * Time.deltaTime;

    }


    void WorldCollision()
    {
        Vector3 desiredDirection = transform.forward;

        Vector3[] rayDorections = new Vector3[]
        {
            transform.forward,
            Quaternion.AngleAxis(30, transform.up) * transform.forward,
            Quaternion.AngleAxis(-30, transform.up) * transform.forward,
            Quaternion.AngleAxis(30, transform.right) * transform.forward,
            Quaternion.AngleAxis(-30, transform.right) * transform.forward,
        };

        bool obstcaleDetected = false;

        foreach (var ray in rayDorections)
        {
            // Casting a Raycast fowards
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, raycastDistance))
            {
                desiredDirection += hit.normal * avoidanceForce;
                obstcaleDetected = true;

                if (ShowGizmos == true)
                {
                    Debug.DrawRay(transform.position, transform.forward * raycastDistance, Color.red);
                }

            }
            else
            {
                if (ShowGizmos == true)
                {
                    Debug.DrawRay(transform.position, transform.forward * raycastDistance, Color.green);
                }

            }
        }

        if (obstcaleDetected)
        {
            //Smoothly Moves Around Objects
            desiredDirection = desiredDirection.normalized;
            Quaternion TargetRotation = Quaternion.LookRotation(desiredDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, Time.deltaTime * avoidanceForce);
        }

    }




    void AddBoidsObjects()
    {
        allboids = GameObject.FindObjectsOfType<BoidsController>();
    }


    Vector3 Separation()
    {
        Vector3 separation = Vector3.zero;

        for (int i = 0; i < allboids.Length; i++)
        {
            float betweendistance = Vector3.Distance(allboids[i].transform.position, transform.position);

            if (betweendistance < neighborsDistanceVal)
            {
                Vector3 origin = transform.position;
                Vector3 direction = (allboids[i].transform.position - origin).normalized;
    


                // Now need to make it that once in the close distance the move the boids away
                Vector3 otherboidsTocurrBoid = transform.position - allboids[i].transform.position;

                if (betweendistance < SeparationDistanceVal)
                {
                    otherboidsTocurrBoid.Normalize();
                    otherboidsTocurrBoid /= SeparationDistanceVal;
                    separation += otherboidsTocurrBoid;
                }

              
                if (ShowGizmos == true)
                {
                    if (betweendistance < SeparationDistanceVal)
                    {
                        Debug.DrawLine(transform.position, allboids[i].transform.position, Color.red);
                    }
                    else
                    {
                        Debug.DrawLine(transform.position, allboids[i].transform.position, Color.yellow);
                    }

                }
            }
        }

        if (separation.magnitude > 0)
        {
            separation.Normalize();
            separation *= speed;
            separation -= velocity;
            separation = Vector3.ClampMagnitude(separation, maxforce);
        }
        return separation * avoidanceForce;
    }


    Vector3 Alignment()
    {
        Vector3 alignmentVelocity = Vector3.zero;
        int count = 0;
        for (int i = 0; i < allboids.Length; i++)
        {
            float betweendistance = Vector3.Distance(allboids[i].transform.position, transform.position);

            if (betweendistance < aliagmentdistance)
            {
                alignmentVelocity += allboids[i].GetComponent<BoidsController>().velocity;
                count++;
            }
        }

        if (count > 0)
        {
            alignmentVelocity /= count;
            alignmentVelocity.Normalize();
            alignmentVelocity *= speed;
            Vector3 steering = alignmentVelocity - velocity;
            steering = Vector3.ClampMagnitude(steering, maxforce);
            return steering * alignmentWeight;
        }
        return Vector3.zero;
    }


    Vector3 Cohesion()
    {
        Vector3 CohesionPoint = Vector3.zero; 
        int count = 0;

        for (int i = 0; i < allboids.Length; i++)
        {
            float betweendistance = Vector3.Distance(allboids[i].transform.position, transform.position);

            if (betweendistance < neighborsDistanceVal)
            {
                CohesionPoint += allboids[i].transform.position + (allboids[i].GetComponent<BoidsController>().velocity * 10f);
                count++;
            }
        }

        if (count > 0) {

            CohesionPoint /= (float)count;
            //CohesionVelocity -= transform.position;
            //CohesionPoint.Normalize();
            if (ShowGizmos == true)
            {
                Debug.DrawLine(transform.position, CohesionPoint * 10f, Color.red);
            }
            
            return (CohesionPoint - transform.position).normalized;

            //Vector3 cohesionDirection = CohesionVelocity - transform.position;
            //cohesionDirection.Normalize();
            //cohesionDirection *= speed;
            //Vector3 steering = cohesionDirection - velocity;
            //steering = Vector3.ClampMagnitude(steering, maxforce);
            

            //return steering * cohesionWeight;
        }
        return Vector3.zero;
    }

}
