using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public int id = 0;
    private static int idinc = 0;
    public LayerMask nodeLayer;
    public List<Node> neighbours;
    public Collider2D toolCollide;
    public bool isClose = false;
    public NodeActivate activate;
    public static float distance = 0.2f;
    private float time = 0;
    private bool finishReplace = false;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (time < 5 && !finishReplace)
        {
            time += Time.deltaTime;
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
        else if(!finishReplace){
            Destroy(GetComponent<Rigidbody2D>());
            VerifyNeighbours();
            finishReplace = true;
        }

        if (isClose)
        {
             
            float distance = activate.GetComponent<Collider2D>().Distance(toolCollide).distance;


            if (distance > 0)
            {
                toolCollide = null;
                isClose = false;
                ChangeDetectionColor();
            }
            else
            {
                SimpleController character = toolCollide.gameObject.GetComponent(typeof(SimpleController)) as SimpleController;
                if (character.nearNode != null && character.nearNode.id == id) character.distance = distance;
                else if (character.distance > distance)
                {
                    character.distance = distance;
                    character.nearNode = this;
                }
            }
        }
        if(!isClose) ChangeDetectionColor();
    }

    public void FindNeighBours()
    {
        NavMesh parent = transform.parent.GetComponent(typeof(NavMesh)) as NavMesh;
        neighbours = new List<Node>();
        id = ++idinc;
        
        GetComponent<Collider2D>().enabled = false;

        FindNeighBour(Vector2.up, parent.nodeDistance);
        FindNeighBour(Vector2.down, parent.nodeDistance);
        FindNeighBour(Vector2.left, parent.nodeDistance);
        FindNeighBour(Vector2.right, parent.nodeDistance);
        FindNeighBour(Vector2.up + Vector2.left, parent.nodeDistance);
        FindNeighBour(Vector2.up + Vector2.right, parent.nodeDistance);
        FindNeighBour(Vector2.down + Vector2.left, parent.nodeDistance);
        FindNeighBour(Vector2.down + Vector2.right, parent.nodeDistance);
        
        GetComponent<Collider2D>().enabled = true;

    }
    public void FindNeighBour(Vector2 direction, float distance)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, NavMesh.layermask);
        direction.Normalize();
        //Debug.DrawLine(transform.position, new Vector2(transform.position.x,transform.position.y)+direction*distance, Color.red, 10);
        if (hit.collider)
        {
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
            Debug.Log("C'est toi");
            return false;
        }

    }
    public void ChangeDetectionColor()
    {
        if(isClose)
            GetComponent<SpriteRenderer>().color = Color.red;
        else GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void VerifyNeighbours()
    {
        int i = 0;
        int nboccur = 0;
        int max = neighbours.Count;
        while (i<neighbours.Count && nboccur<max)
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
                        Debug.DrawLine(this.transform.position, neighbours[i].transform.position, Color.white, 10);
                        i++;
                    }
                }
                else
                {
                    Debug.DrawLine(this.transform.position, neighbours[i].transform.position, Color.white, 10);
                    i++;
                }
                if (hit.collider && hit.collider.gameObject.layer != LayerMask.NameToLayer("Node"))
                    Debug.DrawLine(transform.position + new Vector3(direction.y, -direction.x) * GetComponent<CircleCollider2D>().radius, transform.position + new Vector3(direction.y, -direction.x) * GetComponent<CircleCollider2D>().radius * 0.9f + new Vector3(direction.x, direction.y) * dist, Color.red, 10);
                if (hit2.collider && hit2.collider.gameObject.layer != LayerMask.NameToLayer("Node"))
                    Debug.DrawLine(transform.position - new Vector3(direction.y, -direction.x) * GetComponent<CircleCollider2D>().radius, transform.position - new Vector3(direction.y, -direction.x) * GetComponent<CircleCollider2D>().radius * 0.9f + new Vector3(direction.x, direction.y) * dist, Color.blue, 10);
            }
            else
            {
                i++;
            }
            ++nboccur;
        }
    }
    public bool Remove(Node neighbour)
    {
        if (neighbours.Contains(neighbour))
        {
            neighbours.Remove(neighbour);
            return true;
        }
        else return false;
    }





}
