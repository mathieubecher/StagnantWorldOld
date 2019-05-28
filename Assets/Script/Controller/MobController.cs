using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MobController : HumanController
{
    private Vector3 target;
    public JoeStarState joestar;
    public bool isTarget = false;
    public float speed = 0.1f;
    public Material SHADER;
    public bool chargehit = true;
    
    protected override void Start()
    {
        base.Start();
        target = transform.position;
        left = false;
    }
    protected override void Update()
    {
        DetectMoveInput();
        CurrentState.Move();
        if (chargehit)
        {
            CurrentState.ChargeHit();
        }
        else
        {
            CurrentState.Hit();
        }
        CurrentState.Update();
        if (Input.GetMouseButtonDown(0) && isTarget)
        {

            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = 0;
            joestar = JoeStar();
            
            if (joestar != null)
            {
                //CreateLine(SHADER, target, joestar.node.transform.position);
                //DrawRoute(joestar);
                joestar.Next();
            }

        }
    }

    // Détection du déplacement
    protected override void DetectMoveInput()
    {
        
        if (joestar != null)
        {
            Node n = joestar.GetLastNode();
            move = n.transform.position - transform.position;
        }
        else move = target - transform.position;

        float distance = move.magnitude;
        if (joestar != null || distance > 0.01f)
        {
            if (distance < 0.01f)
            {
                if (joestar != null && joestar.parent != null && joestar.parent.parent == null) joestar = null;
                else if (joestar != null && joestar.parent != null)
                {
                    
                    joestar = JoeStar(joestar.GetLastNode());
                    if (joestar != null)
                    {
                        //CreateLine(SHADER, target, joestar.node.transform.position);
                        //DrawRoute(joestar);
                        joestar.Next();
                    }
                    
                    DetectMoveInput();
                    
                }
                else joestar = null;
            }
            else if (distance < speed * Time.deltaTime)
            {
                transform.position += new Vector3(move.x, move.y);
                move = Vector3.zero;

            }
            else move = move * speed / distance;
        }
        else
        {
            target = transform.position;
        }
    }

    private JoeStarState JoeStar(Node lastnode = null)
    {
        Vector3 goTo = target;

        RaycastHit2D hit = Physics2D.Raycast(Vector3.Lerp(transform.position, goTo, 0.2f / (goTo - transform.position).magnitude), goTo - transform.position, (goTo - transform.position).magnitude, NavMesh.obstacle);
        //Debug.DrawLine(Vector3.Lerp(transform.position, goTo, 0.2f / (goTo - transform.position).magnitude), goTo, Color.white, 0.5f);
        if (!hit.collider)
        {
            return null;
        }
        List<JoeStarState> treated = new List<JoeStarState>();
        List<JoeStarState> toTreat = new List<JoeStarState>();
        toTreat.Add(new JoeStarState(nearNode, goTo));
        if (lastnode != null) treated.Add(new JoeStarState(lastnode, goTo));

        while (toTreat.Count > 0)
        {
            
            JoeStarState min = toTreat[0];
            toTreat.Remove(min);
            treated.Add(min);

            // ATTENTION A VIRER DES QUE LE A* MARCHE VRAIMENT. ENERGIEVORE, MAIS TELLEMENT COOL
            // if (min.parent !=null) Debug.DrawLine(min.node.transform.position, min.parent.node.transform.position, Color.white, 0.5f);

            foreach (Node n in min.node.neighbours)
            {
                JoeStarState next = new JoeStarState(min, n, goTo);
                if (next.distance < Node.distance)
                {
                    return next;
                }
                else
                {
                    if (next.node.isClose && !treated.Contains(next) && !toTreat.Contains(next))
                    {
                        treated.Add(next);
                    }
                    else if (!treated.Contains(next) && !toTreat.Contains(next) )
                    {
                        toTreat.Add(next);
                    }
                    
                    else if (toTreat.Contains(next) && toTreat.Find(x => x.node.id == next.node.id).GetEuristic() > next.GetEuristic() && !next.node.isClose)
                    {
                        toTreat.Remove(next);
                        toTreat.Add(next);
                    }
                    else if (treated.Contains(next) && treated.Find(x => x.node.id == next.node.id).weight > next.weight && !next.node.isClose)
                    {
                        treated.Remove(next);
                        toTreat.Add(next);
                    }
                    
                    
                }
            }
            toTreat.Sort(new JoeStarStateComparer());
        }
        return new JoeStarState(nearNode, goTo);
    }




    public void DrawRoute(JoeStarState state)
    {

        if (state.parent != null)
        {
            CreateLine(SHADER,state.node.transform.position, state.parent.node.transform.position);
            DrawRoute(state.parent);
        }
        else
        {
            CreateLine(SHADER, state.node.transform.position, transform.position);
        }
    }

    public void CreateLine(Material shader,Vector3 start, Vector3 end)
    {
        float lineWidth = 0.05f;
        Vector3[] positions = new Vector3[2];
        positions[0] = start;
        positions[1] = end;

        GameObject line = new GameObject();
        //myLine.transform.position = start;
        line.AddComponent<LineRenderer>();
        LineRenderer lineRender = line.GetComponent<LineRenderer>();
        lineRender.material.color = Color.white;
        lineRender.SetColors(Color.white, Color.white);
        lineRender.SetWidth(lineWidth, lineWidth);
        lineRender.SetPositions(positions);
        lineRender.material = shader;
        GameObject.Destroy(lineRender, 1f);
    }
}

