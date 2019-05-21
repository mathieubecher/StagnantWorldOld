using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JoeStarState : IEquatable<JoeStarState>
{
    public Node node;
    public float distance;
    public JoeStarState parent;

    public JoeStarState(JoeStarState parent, Node n,Vector3 goTo)
    {
        this.node = n;
        this.parent = parent;
        CalculDistance(goTo);
    }
    public JoeStarState(Node n, Vector3 goTo)
    {
        this.node = n;
        CalculDistance(goTo);
    }
    private void CalculDistance(Vector3 goTo)
    {
        goTo.z = node.transform.position.z;
        distance = (float)Math.Abs(Vector3.Distance(goTo, node.transform.position));
    }

    public bool Equals(JoeStarState other)
    {
        if (other == null)
            return false;

        if (this.node.id == other.node.id)
            return true;
        else
            return false;
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;

        JoeStarState joestar = obj as JoeStarState;
        if (joestar == null)
            return false;
        else
            return Equals(joestar);
    }
    public Node GetLastNode()
    {
        if (parent != null) return parent.GetLastNode();
        else return node;
    }
    public void Next()
    {
        if (parent != null && parent.parent != null) parent.Next();
        else if (parent != null && parent.parent == null) parent = null;
    }

}
