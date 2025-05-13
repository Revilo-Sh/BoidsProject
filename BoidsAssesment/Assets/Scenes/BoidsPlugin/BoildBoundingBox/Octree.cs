using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octree
{
    public OctreeNode rootRode;

    public Octree(GameObject[] worldObject, float minNodeSize)
    {
        Bounds bounds = new Bounds();

        foreach(GameObject go in worldObject)
        {
            bounds.Encapsulate(go.GetComponent<Collider>().bounds);
        }

        float maxSize = Mathf.Max(new float[] { bounds.size.x, bounds.size.y, bounds.size.z });
        Vector3 sizeVector = new Vector3(maxSize, maxSize, maxSize) * 0.5f;
        bounds.SetMinMax(bounds.center - sizeVector, bounds.center + sizeVector);
        rootRode = new OctreeNode(bounds, minNodeSize);
        Addobjects(worldObject);
    }

    public void Addobjects(GameObject[] worldObject)
    {
        foreach(GameObject go in worldObject)
        {
            rootRode.AddObject(go);
        }
    }
}
