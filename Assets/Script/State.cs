using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class State
{
    private CharacterController character;
    private Direction direction;

    public State(CharacterController pc)
    {
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
        RotateWeapon();

    }

    public void RotateWeapon()
    {
        float rotate = 0;
        switch (direction)
        {
            case Direction.TOP:
                rotate = 0;
                break;
            case Direction.BOTTOM:
                rotate = 180;
                break;
            case Direction.LEFT:
                rotate = 90;
                break;
            case Direction.RIGHT:
                rotate = -90;
                break;
            case Direction.TOPLEFT:
                rotate = 45;
                break;
            case Direction.TOPRIGHT:
                rotate = -45;
                break;
            case Direction.BOTTOMLEFT:
                rotate = 135;
                break;
            case Direction.BOTTOMRIGHT:
                rotate = -135;
                break;
        }
        character.transform.GetChild(0).transform.eulerAngles = new Vector3(0, 0, rotate);
    }
}
