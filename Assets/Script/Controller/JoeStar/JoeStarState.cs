using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JoeStarState : IEquatable<JoeStarState>
{
    public Node node;
    public float distance;
    public JoeStarState parent = null;
    public int weight;

    // Constructor with parent
    public JoeStarState(JoeStarState parent, Node n,Vector3 goTo)
    {
        this.node = n;
        this.parent = parent;
        weight = parent.weight + 1;
        CalculDistance(goTo);
    }
    // Constructor without parent
    public JoeStarState(Node n, Vector3 goTo)
    {
        this.node = n;
        weight = 0;
        CalculDistance(goTo);
    }
    // Calcul distance between node and destination
    private void CalculDistance(Vector3 goTo)
    {
        goTo.z = node.transform.position.z;
        distance = (float)Math.Abs(Vector3.Distance(goTo, node.transform.position));
    }
    // Compare tow joestarstate
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
    // Recursivly find last node
    public Node GetLastNode()
    {
        if (parent== this) return node;
        else if (parent != null) return parent.GetLastNode();
        else return node;
    }
    // Delete last node
    public void Next()
    {
        if (parent != null && parent.parent != null) parent.Next();
        else if (parent != null && parent.parent == null) parent = null;
    }

    // Get queue length
    public int Length(int i)
    {
        if (parent == this) return 1;
        else if (parent != null) return parent.Length(i)+1;
        else return 1;
    }
}
