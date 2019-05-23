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
    protected override void Start()
    {
        base.Start();
        target = transform.position;
    }
    protected override void Update()
    {
        DetectMoveInput();
        CurrentState.Move();
        CurrentState.Update();
        if (Input.GetMouseButtonDown(0) && isTarget)
        {

            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = 0;
            joestar = JoeStar();
            joestar.Next();
            CreateLine(SHADER, target, joestar.node.transform.position);
            DrawRoute(joestar);

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
                    CreateLine(SHADER, target, joestar.node.transform.position);
                    DrawRoute(joestar);
                    joestar.Next();
                    DetectMoveInput();
                }
                else joestar = null;
            }
            else if (distance < speed/50)
            {
                //Debug.Log("less than max move");
                transform.position += new Vector3(move.x, move.y);
                move = Vector3.zero;

            }
            else move = move * speed / distance;
            //Debug.Log(Node.distance / (float)10);
        }
        else
        {
            target = transform.position;
        }
    }

    private JoeStarState JoeStar(Node lastnode = null)
    {
        Vector3 goTo = target;

        List<JoeStarState> treated = new List<JoeStarState>();
        List<JoeStarState> toTreat = new List<JoeStarState>();
        toTreat.Add(new JoeStarState(nearNode, goTo));
        if (lastnode != null) treated.Add(new JoeStarState(lastnode, goTo));

        while (toTreat.Count > 0)
        {

            JoeStarState min = toTreat[0];
            toTreat.Remove(min);
            treated.Add(min);

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
                    if (!treated.Contains(next) && !toTreat.Contains(next))
                    {
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

