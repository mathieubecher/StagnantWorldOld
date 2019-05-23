using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum Direction { LEFT, RIGHT, TOP, BOTTOM, TOPLEFT, TOPRIGHT, BOTTOMLEFT, BOTTOMRIGHT }
public class SimpleController : MonoBehaviour
{

    public int LIFE;
    protected int life;

    private OriginalState currentState;

    protected Vector2 move;
    public Vector2 Move { get => move; set => move = value; }
     public int Life { get => life; set => life = value; }

    public Node nearNode;
    public float distance;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        life = LIFE;
        move = new Vector2(0, 0);
        currentState = new OriginalState(this);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        currentState.Move();
    }

    protected virtual void DetectMoveInput()
    {
        move = Vector2.zero;
    }
    protected virtual void Dead()
    {
        Destroy(this.gameObject);
        Destroy(this);
    }
}
