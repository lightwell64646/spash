using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class groundNav : MonoBehaviour
{
    public Mesh navMesh;
    protected List<navNode> navNodes;
    
    // Start is called before the first frame update
    void Start()
    {
        if (navMesh != null) navMesh = GetComponent<MeshFilter>().mesh;
        buildNavNodes();
    }

    void buildNavNodes()
    {
        for (int tri = 0; tri < navMesh.triangles.Length/3; tri += 3)
        {
            navNode newNode = new navNode();
            int ind0 = navMesh.triangles[tri + 0];
            int ind1 = navMesh.triangles[tri + 1];
            int ind2 = navMesh.triangles[tri + 2];
            Vector3 vert0 = navMesh.vertices[ind0];
            Vector3 vert1 = navMesh.vertices[ind1];
            Vector3 vert2 = navMesh.vertices[ind2];
            newNode.center = (vert0 + vert1 + vert2) / 3;
            newNode.verticies = new Vector3[] {vert0, vert1, vert2};
            newNode.edges = new edge[] { new edge(ind0, ind1), new edge(ind1, ind2), new edge(ind2, ind0) };
            foreach (navNode oldNode in navNodes)
            {
                for (int i = 0; i < 3; i+=1)
                {
                    edge oldEdge = oldNode.edges[i];
                    for (int j = 0; j < 3; j += 1)
                    {
                        edge newEdge = newNode.edges[j];
                        if (oldEdge == newEdge)
                        {
                            newNode.children[j] = oldNode;
                            oldNode.children[i] = newNode;
                            float cost = (newNode.center - oldNode.center).magnitude;
                            newNode.costs[j] = cost;
                            oldNode.costs[i] = cost;
                        }
                    }
                }
            }
            navNodes.Add(newNode);
        }
    }

    public navNode getNodeForPoint(Vector2 p)
    {
        foreach (navNode node in navNodes)
        {
            Vector2 bari = p - (Vector2)node.verticies[0];
            Vector2 u = node.verticies[1] - node.verticies[0];
            Vector2 v = node.verticies[2] - node.verticies[0];
            float uvDet = 1 / (u.x * v.y - v.x * u.y);
            float umag = (v.y * bari.x - v.x * bari.y) * uvDet;
            float vmag = (u.x * bari.y - u.y * bari.x) * uvDet;
            if (umag + vmag > 1 || umag < 0 || vmag < 0)
                return node;
        }
        return null;
    }

    public List<navNode> getPath(Vector2 from, Vector2 to)
    {
        navNode start = getNodeForPoint(from);
        navNode end = getNodeForPoint(to);
        foreach (navNode node in navNodes) {
            node.bestCost = float.PositiveInfinity;
            node.estimatedCost = ((Vector2)node.center - to).magnitude + ((Vector2)node.center - from).magnitude;
        }

        PriorityQueue<navNode> nextBest = new PriorityQueue<navNode>();
        start.bestCost = 0f;
        start.parent = null;
        nextBest.Enqueue(start);
        float searchRadius = start.bestCost;
        while (end.bestCost == float.PositiveInfinity)
        {
            navNode next = nextBest.Dequeue();
            if (next.bestCost < searchRadius) continue; //costs should be strictly increasing. Need to catch old references to verticies
            for (int i = 0; i < 3; i+=1)
            {
                float offeredCost = next.bestCost + next.costs[i];
                navNode child = next.children[i];
                if (offeredCost < child.bestCost)
                {
                    child.bestCost = offeredCost;
                    child.estimatedCost = offeredCost + ((Vector2)child.center - to).magnitude;
                    child.parent = next;
                    nextBest.Enqueue(child);
                }
            }
        }

        List<navNode> solutionPath = new List<navNode>();
        navNode solution = end;
        while (solution.parent != null)
        {
            solutionPath.Add(solution);
            solution = solution.parent;
        }

        return solutionPath;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}


public class navNode : IComparable
{
    public navNode[] children;
    public float[] costs;
    public edge[] edges;
    public Vector3[] verticies;
    public Vector3 center;
    public navNode parent;
    public float bestCost;
    public float estimatedCost;

    public int CompareTo(object obj)
    {
        if (obj == null) return 1;
        navNode navobj = obj as navNode;
        return (int)(estimatedCost - navobj.estimatedCost);
    }
}

public class edge
{
    public int top, bottom;
    public edge(int t, int b)
    {
        top = t;
        bottom = b;
    }

    public static bool operator ==(edge e1, edge e2)
    {
        if (e1.top == e2.top)
        {
            return e1.bottom == e2.bottom;
        }
        return (e1.bottom == e2.top) && (e2.bottom == e1.top);
    }
    public static bool operator !=(edge e1, edge e2)
    {
        if (e1.top == e2.top)
        {
            return e1.bottom != e2.bottom;
        }
        return (e1.bottom != e2.top) || (e2.bottom != e1.top);
    }
}
