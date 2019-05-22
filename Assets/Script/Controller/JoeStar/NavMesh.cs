using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class NavMesh : MonoBehaviour
{
    public GameObject node;
    private Rect surface;
    public static int layermask;
    public float nodeDistance = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        layermask = LayerMask.GetMask("Node", "Wall");
        surface = GetComponent<RectTransform>().rect;
        InstantiateAllNode();
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void InstantiateAllNode()
    {
        float width = surface.width;
        float height = surface.height;
        float radiusCollider = (float)Math.Sqrt(Math.Pow(nodeDistance / 2, 2) * 2) / 2;
        List<Node> allNodes = new List<Node>();

        for (int y = 0; y < height / nodeDistance * 2; y++)
        {
            for (int x = 0; x < width / nodeDistance; x++)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(x * nodeDistance + ((y % 2 == 0) ? 0 : nodeDistance / 2), y * nodeDistance / 2), Vector2.zero, 0, layermask);
                if (!hit.collider)
                {
                    GameObject newNode = Instantiate(node, transform);
                    Node n = newNode.GetComponent(typeof(Node)) as Node;
                    n.transform.localPosition = new Vector2(x * nodeDistance + ((y % 2 == 0) ? 0 : nodeDistance / 2), y * nodeDistance / 2);
                    Node.distance = nodeDistance;
                    n.GetComponent<CircleCollider2D>().radius = radiusCollider;
                    n.transform.GetChild(0).GetComponent<CircleCollider2D>().radius = radiusCollider;
                    n.FindNeighBours();
                    allNodes.Add(n);

                    n.transform.gameObject.AddComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                    n.transform.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                }
            }
        }
        int i = 0;
        while (i < allNodes.Count)
        {

            if (allNodes[i].neighbours.Count == 0) Destroy(allNodes[i].transform.gameObject);
            ++i;
        }



    }
}
