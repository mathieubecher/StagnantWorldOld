using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class State
{
    protected CharacterController character;
    protected Direction direction;
    protected bool hasHand = false;

    public State(CharacterController pc,Direction direction = Direction.BOTTOM)
    {
        this.direction = direction;
        character = pc;
    }
    
    public void Move()
    {
        character.GetComponent<Rigidbody2D>().velocity = new Vector3(character.Move.x, character.Move.y);

        if (character.Move.y > 0 && Math.Abs(Math.Abs(character.Move.y) - Math.Abs(character.Move.x)) > 0.4 && Math.Abs(character.Move.y)> Math.Abs(character.Move.x)) direction = Direction.TOP;
        else if (character.Move.y < 0 && Math.Abs(Math.Abs(character.Move.y) - Math.Abs(character.Move.x)) > 0.4 && Math.Abs(character.Move.y) > Math.Abs(character.Move.x)) direction = Direction.BOTTOM;
        else if (character.Move.x < 0 && Math.Abs(Math.Abs(character.Move.y) - Math.Abs(character.Move.x)) > 0.4 && Math.Abs(character.Move.x) > Math.Abs(character.Move.y)) direction = Direction.LEFT;
        else if (character.Move.x > 0 && Math.Abs(Math.Abs(character.Move.y) - Math.Abs(character.Move.x)) > 0.4 && Math.Abs(character.Move.x) > Math.Abs(character.Move.y)) direction = Direction.RIGHT;
        else if (character.Move.y > 0 && character.Move.x < 0) direction = Direction.TOPLEFT;
        else if (character.Move.y > 0 && character.Move.x > 0) direction = Direction.TOPRIGHT;
        else if (character.Move.y < 0 && character.Move.x < 0) direction = Direction.BOTTOMLEFT;
        else if (character.Move.y < 0 && character.Move.x > 0) direction = Direction.BOTTOMRIGHT;
    }

    public void Hit()
    {
        character.CurrentState = new HitState(character,direction);
    }

    public bool HasHand()
    {
        return hasHand;
    }

    public virtual void HandUpdate() { }
}
