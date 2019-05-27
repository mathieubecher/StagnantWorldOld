using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class NavMesh : MonoBehaviour
{
    // Node prefab
    public GameObject node;
    // Size of the navmesh
    private Rect surface;
    // Mask for raycast
    public static int layermask;
    public static int obstacle;
    // Distance between node
    public float nodeDistance = 0.2f;

    void Start()
    {
        // Create layer mask for raycast
        layermask = LayerMask.GetMask("Node", "Wall");
        obstacle = LayerMask.GetMask("Wall", "Character","Player");
        // Get size of the navmesh
        surface = GetComponent<RectTransform>().rect;
        // Create all node in surface
        InstantiateAllNode();
    }
    
    void Update()
    {
    }

    private void InstantiateAllNode()
    {
        // Get size
        float width = surface.width;
        float height = surface.height;
        // Get radius
        float radiusCollider = (float)Math.Sqrt(Math.Pow(nodeDistance / 2, 2) * 2) / 2;

        // List of all node create in this function
        List<Node> allNodes = new List<Node>();

        // Grid
        for (int y = 0; y < height / nodeDistance * 2; y++)
        {
            for (int x = 0; x < width / nodeDistance; x++)
            {
                // If there aren't wall or node on this place
                RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(x * nodeDistance + ((y % 2 == 0) ? 0 : nodeDistance / 2), y * nodeDistance / 2), Vector2.zero, 0, layermask);
                if (!hit.collider)
                {
                    // Create node inside navmesh
                    GameObject newNode = Instantiate(node, transform);
                    Node n = newNode.GetComponent(typeof(Node)) as Node;
                    // Set node position on good place
                    n.transform.localPosition = new Vector2(x * nodeDistance + ((y % 2 == 0) ? 0 : nodeDistance / 2), y * nodeDistance / 2);
                    // Set dimension between node
                    Node.distance = nodeDistance;
                    n.GetComponent<CircleCollider2D>().radius = radiusCollider;
                    n.transform.GetChild(0).GetComponent<CircleCollider2D>().radius = radiusCollider;
                    // Get all neighbours with raycast
                    n.FindNeighBours();
                    // Add this new node into list 
                    allNodes.Add(n);

                    // Add freeze rigidbody component
                    n.transform.gameObject.AddComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                    n.transform.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                }
            }
        }
        // Find all nodes which have any neighbour
        int i = 0;
        while (i < allNodes.Count)
        {

            if (allNodes[i].neighbours.Count == 0) Destroy(allNodes[i].transform.gameObject);
            ++i;
        }



    }
}
