using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : SimpleController
{
    private State currentState;

    public GameObject weapon;
    public GameObject leftWeapon;
    public bool left = false;


    public Equipment helmet;
    public Equipment pants;
    public Equipment mitt;
    public Equipment torso;

    public State CurrentState { get => currentState; set => currentState = value; }


    protected override void Start()
    {
        base.Start();
        currentState = new State(this);
    }

    public GameObject GetWeapon()
    {
        if (left) return leftWeapon;
        else return weapon;
    }
}
