using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public int id = 0;
    private static int idinc = 0;
    public LayerMask nodeLayer;
    public List<Node> neighbours;
    public List<Collider2D> toolCollides;
    public bool isClose = false;
    public NodeActivate activate;
    public static float distance = 0.2f;
    private float time = 0;
    private bool finishReplace = false;


    void Update()
    {
        // Wait 5 seconds to destroy rigidbody and definitly freeze node
        if (time < 5 && !finishReplace)
        {
            time += Time.deltaTime;
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }

        else if(!finishReplace){
            Destroy(GetComponent<Rigidbody2D>());
            // Try to verify if its neighbours are allways connected to it
            VerifyNeighbours();
            finishReplace = true;
        }

        // If someone walk on the node
        if (isClose)
        {
            int i = 0;
            while (i < toolCollides.Count)
            {
                Collider2D toolCollide = toolCollides[i];
                // Distance between two collider
                float distance = activate.GetComponent<Collider2D>().Distance(toolCollide).distance;
                // character exit the node
                if (distance > 0)
                {
                    toolCollides.Remove(toolCollide);
                    
                }
                // character is on the node yet
                else
                {
                    SimpleController character = toolCollide.gameObject.GetComponent(typeof(SimpleController)) as SimpleController;
                    if (character.nearNode != null && character.nearNode.id == id) character.distance = distance;
                    else if (character.distance > distance)
                    {
                        character.distance = distance;
                        character.nearNode = this;
                    }
                    ++i;
                }
            }
            if(toolCollides.Count == 0)
            {
                isClose = false;
                ChangeDetectionColor();
            }
            
        }
        // If node is open, change his color (for debug)
        if(!isClose) ChangeDetectionColor();
    }

    // Call raycast for all direction to find neighbour
    public void FindNeighBours()
    {
        NavMesh parent = transform.parent.GetComponent(typeof(NavMesh)) as NavMesh;
        neighbours = new List<Node>();
        // Set id for this node (to compare it with other)
        id = ++idinc;
        
        // Desactivate his own collider (else raycast stoped on it)
        GetComponent<Collider2D>().enabled = false;
        // Findneighbour for all direction
        FindNeighBour(Vector2.up, parent.nodeDistance);
        FindNeighBour(Vector2.down, parent.nodeDistance);
        FindNeighBour(Vector2.left, parent.nodeDistance);
        FindNeighBour(Vector2.right, parent.nodeDistance);
        FindNeighBour(Vector2.up + Vector2.left, parent.nodeDistance);
        FindNeighBour(Vector2.up + Vector2.right, parent.nodeDistance);
        FindNeighBour(Vector2.down + Vector2.left, parent.nodeDistance);
        FindNeighBour(Vector2.down + Vector2.right, parent.nodeDistance);
        // Activate his own collider
        GetComponent<Collider2D>().enabled = true;

    }
    public void FindNeighBour(Vector2 direction, float distance)
    {
        // Create raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, NavMesh.layermask);
        direction.Normalize();
        // If collide
        if (hit.collider)
        {
            // Define if it's a node and add it to neighbour
            try
            {
                Node neighbour = hit.collider.gameObject.GetComponent(typeof(Node)) as Node;
                neighbour.AddNeighBour(this);
                AddNeighBour(neighbour);
            }
            catch
            {
                //Debug.Log("ce n'est pas un noeud "+hit.collider.tag);
            }
        }
    }
    public bool AddNeighBour(Node neighbour)
    {
        // if node isn't in neighbours yet, add it
        if (neighbour.id != this.id)
        {
            int i = 0;
            while (i < neighbours.Count && neighbours[i].id != neighbour.id) i++;

            if (i == neighbours.Count)
                neighbours.Add(neighbour);
            return i < neighbours.Count;
        }
        else
        {
            Debug.Log("node try to add itself");
            return false;
        }

    }
    // Swap color (for debug)
    public void ChangeDetectionColor()
    {
        if(isClose)
            GetComponent<SpriteRenderer>().color = Color.red;
        else GetComponent<SpriteRenderer>().color = Color.white;
    }

    // Verify if neighbours are still connected to it
    public void VerifyNeighbours()
    {
        int i = 0;
        int max = neighbours.Count;
        while (i<neighbours.Count)
        {
            Vector2 direction = neighbours[i].transform.position - transform.position;
            direction.Normalize();
            float dist = (neighbours[i].transform.position - transform.position).magnitude;
            RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(direction.y,-direction.x) * GetComponent<CircleCollider2D>().radius, direction, dist, NavMesh.layermask);
            RaycastHit2D hit2 = Physics2D.Raycast(transform.position - new Vector3(direction.y, -direction.x) * GetComponent<CircleCollider2D>().radius, direction, dist, NavMesh.layermask);
            if (hit.collider || hit2.collider)
            {
                if (hit.collider && hit.collider.gameObject.layer != LayerMask.NameToLayer("Node") || hit2.collider && hit2.collider.gameObject.layer != LayerMask.NameToLayer("Node"))
                {
                    if (!Remove(neighbours[i]))
                    {
                        //Debug.DrawLine(this.transform.position, neighbours[i].transform.position, Color.gray, 50);
                        i++;
                    }
                    else
                    {

                        //Debug.DrawLine(transform.position + new Vector3(direction.y, -direction.x) * GetComponent<CircleCollider2D>().radius, transform.position + new Vector3(direction.y, -direction.x) * GetComponent<CircleCollider2D>().radius * 0.9f + new Vector3(direction.x, direction.y) * dist, Color.red, 50);
                        //Debug.DrawLine(transform.position - new Vector3(direction.y, -direction.x) * GetComponent<CircleCollider2D>().radius, transform.position - new Vector3(direction.y, -direction.x) * GetComponent<CircleCollider2D>().radius * 0.9f + new Vector3(direction.x, direction.y) * dist, Color.blue, 50);
                    }
                }
                else
                {
                    //Debug.DrawLine(this.transform.position, neighbours[i].transform.position, Color.white, 50);
                    i++;
                }
                
            }
            else
            {
                //Debug.DrawLine(this.transform.position, neighbours[i].transform.position, Color.white, 50);
                i++;
            }
        }
    }
    // remove two relations with neighbour
    public bool Remove(Node neighbour)
    {
        if (neighbours.Contains(neighbour))
        {
            neighbours.Remove(neighbour);
            neighbour.Remove(this);
            return true;
        }
        else return false;
    }





}
