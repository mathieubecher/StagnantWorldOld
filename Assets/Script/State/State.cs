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
        if (character.Move.y > 0 && Math.Abs(Math.Abs(character.Move.y) - Math.Abs(character.Move.x)) > 0.4 && Math.Abs(character.Move.y) > Math.Abs(character.Move.x))
        {
            direction = Direction.TOP;
            character.anim.SetInteger("Direction",2);
        }
        else if (character.Move.y < 0 && Math.Abs(Math.Abs(character.Move.y) - Math.Abs(character.Move.x)) > 0.4 && Math.Abs(character.Move.y) > Math.Abs(character.Move.x))
        {
            direction = Direction.BOTTOM;
            character.anim.SetInteger("Direction", 0);
        }
        else if (character.Move.x < 0 && Math.Abs(Math.Abs(character.Move.y) - Math.Abs(character.Move.x)) > 0.4 && Math.Abs(character.Move.x) > Math.Abs(character.Move.y))
        {
            direction = Direction.LEFT;
            character.anim.SetInteger("Direction", 1);
        }
        else if (character.Move.x > 0 && Math.Abs(Math.Abs(character.Move.y) - Math.Abs(character.Move.x)) > 0.4 && Math.Abs(character.Move.x) > Math.Abs(character.Move.y))
        {
            direction = Direction.RIGHT;
            character.anim.SetInteger("Direction", 3);
        }
        else if (character.Move.y > 0 && character.Move.x < 0) direction = Direction.TOPLEFT;
        else if (character.Move.y > 0 && character.Move.x > 0) direction = Direction.TOPRIGHT;
        else if (character.Move.y < 0 && character.Move.x < 0) direction = Direction.BOTTOMLEFT;
        else if (character.Move.y < 0 && character.Move.x > 0) direction = Direction.BOTTOMRIGHT;

        UpdateAnim((Math.Abs(character.Move.x) + Math.Abs(character.Move.y) > 0), false, false, false);
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
        return "Move";
    }

    public virtual void Update() { }

    public virtual void RotateWeapon() { }

    public virtual void UpdateAnim(bool move, bool run, bool dash, bool hit)
    {
        if ( direction == Direction.TOP)
            character.anim.SetInteger("Direction", 2);
        else if (direction == Direction.BOTTOM)
            character.anim.SetInteger("Direction", 0);
        else if (direction == Direction.LEFT)
            character.anim.SetInteger("Direction", 1);
        else if (direction == Direction.RIGHT)
            character.anim.SetInteger("Direction", 3);

        character.anim.SetBool("Move", move);
        character.anim.SetBool("Run", run);
        character.anim.SetBool("Dash", dash);
        character.anim.SetBool("Hit", hit);
        character.anim.SetBool("Iddle", !(hit || move || run || dash));

    }
}
