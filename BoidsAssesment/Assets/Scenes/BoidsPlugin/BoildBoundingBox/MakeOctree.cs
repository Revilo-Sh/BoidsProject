using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeOctree : MonoBehaviour
{
    public GameObject[] worldObjects;
    public int nodeMinSize = 5;
    Octree m_octree;

    // Start is called before the first frame update
    void Start()
    {
       m_octree = new Octree(worldObjects, nodeMinSize); 
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            m_octree.rootRode.Draw();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
