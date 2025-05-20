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
    List<Collider> objects;

    public OctreeNode(Bounds b, float minNodeSize)
    {
        NodeBounds = b;
        minSize = minNodeSize;

        objects = new List<Collider>();
        
    }

    public void AddObject(GameObject go)
    {
        //Tell the gameObject what node it's in
        //Inside the gameObject, have a script that checks collisions with the node
        //     When that object is no longer colliding with the node, delete the node, go up to the parent node and attempt to divide again

        DivideAndAdd(go);
    }

    public void CreateChildNodes()
    {
        float quarter = NodeBounds.size.y / 4.0f;
        float childLength = NodeBounds.size.y / 2.0f;
        Vector3 childsize = new Vector3(childLength, childLength, childLength);

        children = new OctreeNode[8];
        children[0] = new OctreeNode(new Bounds(NodeBounds.center + new Vector3(-quarter, quarter, -quarter), childsize), minSize);
        children[1] = new OctreeNode(new Bounds(NodeBounds.center + new Vector3(quarter, quarter, -quarter), childsize), minSize);
        children[2] = new OctreeNode(new Bounds(NodeBounds.center + new Vector3(-quarter, quarter, quarter), childsize), minSize);
        children[3] = new OctreeNode(new Bounds(NodeBounds.center + new Vector3(quarter, quarter, quarter), childsize), minSize);
        children[4] = new OctreeNode(new Bounds(NodeBounds.center + new Vector3(quarter, -quarter, -quarter), childsize), minSize);
        children[5] = new OctreeNode(new Bounds(NodeBounds.center + new Vector3(-quarter, -quarter, -quarter), childsize), minSize);
        children[6] = new OctreeNode(new Bounds(NodeBounds.center + new Vector3(quarter, -quarter, quarter), childsize), minSize);
        children[7] = new OctreeNode(new Bounds(NodeBounds.center + new Vector3(-quarter, -quarter, quarter), childsize), minSize);
    }

    public void DivideAndAdd(GameObject go)
    {
        if (children != null)
        {
            
            Disperse(go);
            return;
        }

        
        if (children == null)
        {
            //If collides with this node, add to objects list
            if (NodeBounds.Intersects(go.GetComponent<Collider>().bounds))
            {
                objects.Add(go.GetComponent<Collider>());
                    //If objects list is full, create child nodes and add objects to them.
                    if (objects.Count >= 4)
                    {
                        CreateChildNodes();

                        foreach (Collider col in objects)
                        {
                            Disperse(col.gameObject);
                        }
                        objects.Clear();
                    }

                    
            }
        }
        
        
        
    }
    
    void Disperse(GameObject go)
    {
        for (int i = 0; i < 8; i++) 
            {
                if (children[i].NodeBounds.Intersects(go.GetComponent<Collider>().bounds)) 
                { 
                    children[i].DivideAndAdd(go);
                }
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
