using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MobController : CharacterController
{
    private Vector3 target;
    public JoeStarState joestar;
    public bool isTarget = false;
    public float speed = 0.05f;
    protected override void Start()
    {
        base.Start();
        target = transform.position;
    }
    protected override void Update()
    {
        DetectMoveInput();
        CurrentState.Move();
        CurrentState.UpdateAnim();
        CurrentState.Update();
        if (Input.GetMouseButtonDown(0) && isTarget)
        {

            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = 0;
            joestar = JoeStar();
            joestar.Next();
            DrawLine.NewLine(target, joestar.node.transform.position, Color.red, 5);
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

        float distance = (float)Math.Sqrt(Math.Pow(move.x, 2) + Math.Pow(move.y, 2));

        if (distance < 0.01f)
        {
            if (joestar != null && joestar.parent != null && joestar.parent.parent == null) joestar = null;
            else if (joestar != null)
            {
                joestar.Next();
                DetectMoveInput();
            }
        }
        else if (distance < Node.distance/(float)10)
        {
            Debug.Log("less than max move");
            transform.position += new Vector3(move.x, move.y);
            move = Vector3.zero;

        }
        else move = move * speed / distance;
        Debug.Log(Node.distance / (float)10);
    }

    private JoeStarState JoeStar()
    {
        Vector3 goTo = target;

        List<JoeStarState> treated = new List<JoeStarState>();
        List<JoeStarState> toTreat = new List<JoeStarState>();
        toTreat.Add(new JoeStarState(nearNode, goTo));

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
            DrawLine.NewLine(state.node.transform.position, state.parent.node.transform.position, Color.red, 5);
            DrawRoute(state.parent);
        }
        else
        {
            DrawLine.NewLine(state.node.transform.position, transform.position, Color.red, 5);
        }
    }


}

