using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.Mathematics;
using UnityEngine;

public class BoidsController : MonoBehaviour
{
    //Debug
    public bool ShowGizmos = false;

    //Normal
    public float speed = 5f;
    public float raycastDistance = 5f;
    public float avoidanceForce = 5f;

    private Vector3 velocity;


    // Boids 3 Laws Verables
    public BoidsController[] allboids;
    private BoidsController[] neighborsBoids;
    private float SeparationDistanceVal = 5;
    
    

    private void Start()
    {
        velocity = transform.forward * speed;
    }

    private void Update()
    {
        //WorldCollision();
        AddBoidsObjects();
        transform.position += transform.forward * speed * Time.deltaTime;
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


    void Separation()
    {
        for (int i = 0; i < allboids.Length; i++)
        {
            float betweendistance = Vector3.Distance(allboids[i].transform.position, transform.position);

            if (betweendistance > SeparationDistanceVal)
            {
                // shot a raycast to the object
                // gets its rot nomral 
                // rotate the op dec
                RaycastHit hit;
                

            }

        }
    }

    void Alignment()
    {

    }

    void Cohesion()
    {

    }



    // Start is called before the first frame update

    //public float movementSpeed = 1f;
    //public GameObject m_Target;
    //Vector3 ForwardsNormal;

    //Vector3 TargetPostionNormal;

    //public Transform FirePoint;

    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    RotMove();
    //    ForwardsMove();
    //    Raycasting();
    //}

    //void SetTargetGoal(GameObject other) {
    //    m_Target = other;
    //}


    //void ForwardsMove() {

    //    transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
    //    Vector3 FN = transform.forward * movementSpeed * Time.deltaTime;
    //    ForwardsNormal = FN;

    //}

    //void RotMove() {

    //    transform.LookAt(m_Target.transform);
    //   // TargetPostionNormal = new Vector3(m_Target.transform.position.x, m_Target.transform.position.y, m_Target.transform.position.z);
    //  // transform.Rotate(TargetPostionNormal);
    //}

    //void Raycasting()
    //{
    //    RaycastHit hit;

    //    if  (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 50f))
    //    {
    //        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 20f, Color.green);
    //    }
    //}
}
