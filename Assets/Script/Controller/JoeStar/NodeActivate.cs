using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeActivate : MonoBehaviour
{
    public Node node;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.isTrigger && other.tag == "Character")
        {
            Debug.Log("enter");
            node.toolCollide = other;
            node.isClose = true;
            node.ChangeDetectionColor();
        }
    }
}
