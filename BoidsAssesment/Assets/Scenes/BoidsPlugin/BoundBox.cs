using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundBox : MonoBehaviour
{
    public Vector3 boxCenter = Vector3.zero;
    public Vector3 boxSize = new Vector3(20f, 20f, 20f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        // X Axis
        if (pos.x > boxCenter.x + boxSize.x / 2)
            pos.x = boxCenter.x - boxSize.x / 2;
        else if (pos.x < boxCenter.x - boxSize.x / 2)
            pos.x = boxCenter.x + boxSize.x / 2;

        // Y Axis (optional — comment out if you don't want looping vertically)
        if (pos.y > boxCenter.y + boxSize.y / 2)
            pos.y = boxCenter.y - boxSize.y / 2;
        else if (pos.y < boxCenter.y - boxSize.y / 2)
            pos.y = boxCenter.y + boxSize.y / 2;

        // Z Axis
        if (pos.z > boxCenter.z + boxSize.z / 2)
            pos.z = boxCenter.z - boxSize.z / 2;
        else if (pos.z < boxCenter.z - boxSize.z / 2)
            pos.z = boxCenter.z + boxSize.z / 2;

        transform.position = pos;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(boxCenter, boxSize);
    }
}
