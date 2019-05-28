using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class State : OriginalState
{
    protected HumanController character;
    private float distance = 0;
    private Vector3 lastPos;

    
    /*                              CONSTRUCTEUR                                */
    /*--------------------------------------------------------------------------*/

    public State(HumanController pc, Direction direction = Direction.BOTTOM):base(pc,direction)
    {
        character = pc;
        lastPos = pc.transform.localPosition;
    }

    /*                                  MOVE                                    */
    /*--------------------------------------------------------------------------*/
    public override void Move()
    {
        character.GetComponent<Rigidbody2D>().velocity = new Vector3(character.Move.x, character.Move.y);
        UpdateDirection();
    }

    /*                                  HIT                                     */
    /*--------------------------------------------------------------------------*/
    public override void Hit()
    {
        if(character.GetWeapon() != null) { 
            HitWeapon hitweapon = character.GetWeapon().GetComponent(typeof(HitWeapon)) as HitWeapon;
            character.CurrentState = new HitState(character, Direction, hitweapon.Hits[0]);
        }
    }

    /*                               CHARGEHIT                                  */
    /*--------------------------------------------------------------------------*/
    public override void ChargeHit()
    {
        if (character.GetWeapon() != null)
        {
            character.CurrentState = new ChargeHitState(character, Direction);
        }
    }

    /*                                 DASH                                     */
    /*--------------------------------------------------------------------------*/
    public override void Dash()
    {
        character.CurrentState = new DashState(character, Direction);
    }

    /*                              CHARGEDASH                                  */
    /*--------------------------------------------------------------------------*/
    public override void ChargeDash()
    {
        character.CurrentState = new RunState(character, Direction);
    }

    /*                                UPDATE                                    */
    /*--------------------------------------------------------------------------*/
    public override void Update() {

        Vector3 movementSpeed = character.transform.localPosition - lastPos;
        distance += (float)Math.Sqrt(Math.Pow(movementSpeed.x, 2) + Math.Pow(movementSpeed.y, 2));
        lastPos = character.transform.localPosition;
    }

    // Next state
    protected virtual void NextState()
    {
        if (nextState == "dash")
            character.CurrentState = new DashState(character, Direction);
        else if (nextState == "hit")
        {
            HitWeapon hitweapon = character.GetWeapon().GetComponent(typeof(HitWeapon)) as HitWeapon;
            character.CurrentState = new HitState(character, Direction, hitweapon.Hits[0]);
        }
        else if (nextState == "chargehit")
        {
            character.CurrentState = new ChargeHitState(character, Direction);
        }
        else character.CurrentState = new State(character, Direction);
    }

    // Sort le nom de l'état
    public override string GetName()
    {
        return (Math.Abs(character.Move.x) + Math.Abs(character.Move.y) > 0)?"Move":"Iddle";
    }
    
    // Met à jour la direction du personnage à partir de ses mouvements
    public Direction UpdateDirection()
    {
        Direction last = direction;
        if (character.Move.y > 0 && Math.Abs(Math.Abs(character.Move.y) - Math.Abs(character.Move.x)) * 2 / (Math.Abs(character.Move.y) + Math.Abs(character.Move.x)) > 0.4 && Math.Abs(character.Move.y) > Math.Abs(character.Move.x))
        {
            Direction = Direction.TOP;
        }
        else if (character.Move.y < 0 && Math.Abs(Math.Abs(character.Move.y) - Math.Abs(character.Move.x)) * 2 / (Math.Abs(character.Move.y) + Math.Abs(character.Move.x)) > 0.4 && Math.Abs(character.Move.y) > Math.Abs(character.Move.x))
        {
            Direction = Direction.BOTTOM;
        }
        else if (character.Move.x < 0 && Math.Abs(Math.Abs(character.Move.y) - Math.Abs(character.Move.x)) * 2 / (Math.Abs(character.Move.y) + Math.Abs(character.Move.x)) > 0.4 && Math.Abs(character.Move.x) > Math.Abs(character.Move.y))
        {
            Direction = Direction.LEFT;
        }
        else if (character.Move.x > 0 && Math.Abs(Math.Abs(character.Move.y) - Math.Abs(character.Move.x)) * 2 / (Math.Abs(character.Move.y) + Math.Abs(character.Move.x)) > 0.4 && Math.Abs(character.Move.x) > Math.Abs(character.Move.y))
        {
            Direction = Direction.RIGHT;
        }
        else if (character.Move.y > 0 && character.Move.x < 0) Direction = Direction.TOPLEFT;
        else if (character.Move.y > 0 && character.Move.x > 0) Direction = Direction.TOPRIGHT;
        else if (character.Move.y < 0 && character.Move.x < 0) Direction = Direction.BOTTOMLEFT;
        else if (character.Move.y < 0 && character.Move.x > 0) Direction = Direction.BOTTOMRIGHT;

        if(last != direction) distance = 0;

        return Direction;
    }

    // Fait tourner l'arme
    public virtual void RotateWeapon() { }


    public virtual void AnimProgress() {
        if (GetName() == "Iddle")
        {
            percentProgress += Time.deltaTime;
            percentProgress = percentProgress % 1;
        }
        else
        {

            distance = distance % 0.5f;
            percentProgress = distance /0.5f;
        }
        //Debug.Log(percentProgress);
    }
}
