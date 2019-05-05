using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class State
{
    protected SimpleController character;
    protected Direction direction;
    protected String nextState = "";

    public State(SimpleController pc, Direction direction = Direction.BOTTOM)
    {
        this.direction = direction;
        character = pc;
    }

    public virtual void Move()
    {
        character.GetComponent<Rigidbody2D>().velocity = new Vector3(character.Move.x, character.Move.y);
        UpdateDirection();

    }
    public Direction UpdateDirection()
    {
        if (character.Move.y > 0 && Math.Abs(Math.Abs(character.Move.y) - Math.Abs(character.Move.x)) > 0.4 && Math.Abs(character.Move.y) > Math.Abs(character.Move.x)) direction = Direction.TOP;
        else if (character.Move.y < 0 && Math.Abs(Math.Abs(character.Move.y) - Math.Abs(character.Move.x)) > 0.4 && Math.Abs(character.Move.y) > Math.Abs(character.Move.x)) direction = Direction.BOTTOM;
        else if (character.Move.x < 0 && Math.Abs(Math.Abs(character.Move.y) - Math.Abs(character.Move.x)) > 0.4 && Math.Abs(character.Move.x) > Math.Abs(character.Move.y)) direction = Direction.LEFT;
        else if (character.Move.x > 0 && Math.Abs(Math.Abs(character.Move.y) - Math.Abs(character.Move.x)) > 0.4 && Math.Abs(character.Move.x) > Math.Abs(character.Move.y)) direction = Direction.RIGHT;
        else if (character.Move.y > 0 && character.Move.x < 0) direction = Direction.TOPLEFT;
        else if (character.Move.y > 0 && character.Move.x > 0) direction = Direction.TOPRIGHT;
        else if (character.Move.y < 0 && character.Move.x < 0) direction = Direction.BOTTOMLEFT;
        else if (character.Move.y < 0 && character.Move.x > 0) direction = Direction.BOTTOMRIGHT;
        return direction;
    }
    public virtual void Hit()
    {
        HitWeapon hitweapon = character.GetWeapon().GetComponent(typeof(HitWeapon)) as HitWeapon;
        character.CurrentState = new HitState(character, direction, hitweapon.Hits[0]);
    }
    public virtual void ChargeHit()
    {
        character.CurrentState = new ChargeHitState(character, direction);
    }
    public virtual void Dash()
    {
        character.CurrentState = new DashState(character, direction);
    }

    public virtual void ChargeDash()
    {
        character.CurrentState = new RunState(character, direction);
    }

    protected virtual void NextState()
    {
        if (nextState == "dash")
            character.CurrentState = new DashState(character, direction);
        else if (nextState == "hit")
        {
            HitWeapon hitweapon = character.GetWeapon().GetComponent(typeof(HitWeapon)) as HitWeapon;
            character.CurrentState = new HitState(character, direction, hitweapon.Hits[0]);
        }
        else if (nextState == "chargehit")
        {
            character.CurrentState = new ChargeHitState(character, direction);
        }
        else character.CurrentState = new State(character, direction);
    }
    public virtual string GetName()
    {
        return "Normal";
    }

    public virtual void Update() { }
    public virtual void RotateWeapon() { }
}
