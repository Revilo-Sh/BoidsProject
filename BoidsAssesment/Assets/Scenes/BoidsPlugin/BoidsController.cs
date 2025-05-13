using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidsController : MonoBehaviour
{
    // Start is called before the first frame update

    public float movementSpeed = 1f;
    public GameObject m_Target;
    Vector3 ForwardsNormal;
  
    Vector3 TargetPostionNormal;

    public Transform FirePoint;

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        RotMove();
        ForwardsMove();
        Raycasting();
    }

    void SetTargetGoal(GameObject other) {
        m_Target = other;
    }


    void ForwardsMove() {

        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        Vector3 FN = transform.forward * movementSpeed * Time.deltaTime;
        ForwardsNormal = FN;
       
    }

    void RotMove() {

        transform.LookAt(m_Target.transform);
       // TargetPostionNormal = new Vector3(m_Target.transform.position.x, m_Target.transform.position.y, m_Target.transform.position.z);
      // transform.Rotate(TargetPostionNormal);
    }

    void Raycasting()
    {
        RaycastHit hit;
       
        if  (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 50f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 20f, Color.green);
        }
    }
}
