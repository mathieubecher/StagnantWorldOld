using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum Direction { LEFT, RIGHT, TOP, BOTTOM, TOPLEFT, TOPRIGHT, BOTTOMLEFT, BOTTOMRIGHT }
public class SimpleController : MonoBehaviour
{
    private State currentState;
    public Animator anim;
    public GameObject weapon;
    public GameObject leftWeapon;
    public int LIFE;
    protected int life;
    public bool left = false;

    protected Vector2 move;
    public Vector2 Move { get => move; set => move = value; }
    public State CurrentState { get => currentState; set => currentState = value; }
    public int Life { get => life; set => life = value; }

    public GameObject GetWeapon()
    {
        if (left) return leftWeapon;
        else return weapon;
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        life = LIFE;
        move = new Vector2(0, 0);
        CurrentState = new State(this);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        CurrentState.Move();
    }

    protected virtual void DetectMoveInput()
    {
        move = Vector2.zero;
    }
}
