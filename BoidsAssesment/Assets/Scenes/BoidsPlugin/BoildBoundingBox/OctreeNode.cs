using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class OctreeNode
{
    Bounds NodeBounds;
    float minSize;
    Bounds[] childBounds;
    OctreeNode[] children = null;

    public OctreeNode(Bounds b, float minNodeSize)
    {
        NodeBounds = b;
        minSize = minNodeSize;

        float quarter = NodeBounds.size.y / 4.0f;
        float childLength = NodeBounds.size.y / 2;
        Vector3 childsize = new Vector3(childLength, childLength, childLength);
        childBounds = new Bounds[8];
        childBounds[0] = new Bounds(NodeBounds.center + new Vector3(-quarter, quarter, -quarter), childsize);
        childBounds[1] = new Bounds(NodeBounds.center + new Vector3(quarter, quarter, -quarter), childsize);
        childBounds[2] = new Bounds(NodeBounds.center + new Vector3(-quarter, quarter, quarter), childsize);
        childBounds[3] = new Bounds(NodeBounds.center + new Vector3(quarter, quarter, quarter), childsize);
        childBounds[4] = new Bounds(NodeBounds.center + new Vector3(quarter, -quarter, -quarter), childsize);
        childBounds[5] = new Bounds(NodeBounds.center + new Vector3(-quarter, -quarter, -quarter), childsize);
        childBounds[6] = new Bounds(NodeBounds.center + new Vector3(quarter, -quarter, quarter), childsize);
        childBounds[7] = new Bounds(NodeBounds.center + new Vector3(-quarter, -quarter, quarter), childsize);
        
    }

    public void AddObject(GameObject go)
    {
        //Tell the gameObject what node it's in
        //Inside the gameObject, have a script that checks collisions with the node
        //     When that object is no longer colliding with the node, delete the node, go up to the parent node and attempt to divide again

        DivideAndAdd(go);
    }

    public void DivideAndAdd(GameObject go)
    {
        if (NodeBounds.size.y <= minSize)
        {
            return;
        }
        if (children == null)
        {
            children = new OctreeNode[8];
        }

        bool dividing = false;
        for (int i = 0; i < 8; i++) {
            if (children[i] == null) {
                children[i] = new OctreeNode(childBounds[i], minSize);
            }
            if (childBounds[i].Intersects(go.GetComponent<Collider>().bounds)) { 
                dividing = true;
                children[i].DivideAndAdd(go);
                
            }
        }
        if (dividing == false)
        {
            children = null;
        }
    }

    public void Draw()
    {
        Gizmos.color = new Color(0, 1, 0, 1);
        Gizmos.DrawWireCube(NodeBounds.center, NodeBounds.size);
        if (children != null)
        {
            for (int i = 0; i < 8; i++)
            {
                if (children[i] != null)
                {
                    children[i].Draw();
                }
            }
        }
    }
}
