using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidsController : MonoBehaviour
{
    // Start is called before the first frame update

    public float movementSpeed = 1f;
    public GameObject m_Target;
    Vector3 ForwardsMovement;
    Vector3 absoluteForwards;
    Vector3 TargetPostionNormal;


    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        RotMove();
        ForwardsMove();
    }

    void SetTargetGoal(GameObject other) {
        m_Target = other;
    }


    void ForwardsMove() {
        Vector3 FM = transform.forward * movementSpeed * Time.deltaTime;
        transform.position += FM;
        ForwardsMovement = FM;

        Vector3 AF = Vector3.forward * movementSpeed * Time.deltaTime;
        transform.position += AF;
        absoluteForwards = AF;
      
    }

    void RotMove() {

        TargetPostionNormal = new Vector3(m_Target.transform.position.x, m_Target.transform.position.y, m_Target.transform.position.z);
        transform.Rotate(TargetPostionNormal);
    }
}
