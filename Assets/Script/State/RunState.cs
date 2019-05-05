using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RunState : State
{
    public float speed = 1.2f;
    private Vector3 moveDash;
    public RunState(SimpleController pc,Direction direction):base(pc,direction)
    {
        float actualSpeed = (float)Math.Sqrt(Math.Pow(character.Move.x, 2) + Math.Pow(character.Move.y, 2));
        this.moveDash = character.Move * speed / actualSpeed;
    }
    public override void Move() {
        character.GetComponent<Rigidbody2D>().velocity = new Vector3(moveDash.x, moveDash.y) * 1.5f;
    }
    public override void Dash()
    {
        character.CurrentState = new State(character, direction);
    }
    public override string GetName()
    {
        return "Run";
    }
}
