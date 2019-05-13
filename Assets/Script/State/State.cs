using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class State
{
    protected SimpleController character;
    protected Direction direction;
    protected String nextState = "";
    public float percentProgress = 0;
    private float distance = 0;

    /*                              CONSTRUCTEUR                                */
    /*--------------------------------------------------------------------------*/

    public State(SimpleController pc, Direction direction = Direction.BOTTOM)
    {
        this.direction = direction;
        character = pc;
        
    }

    /*                                  MOVE                                    */
    /*--------------------------------------------------------------------------*/
    public virtual void Move()
    {
        character.GetComponent<Rigidbody2D>().velocity = new Vector3(character.Move.x, character.Move.y);
        UpdateDirection();
    }

    /*                                  HIT                                     */
    /*--------------------------------------------------------------------------*/
    public virtual void Hit()
    {
        HitWeapon hitweapon = character.GetWeapon().GetComponent(typeof(HitWeapon)) as HitWeapon;
        character.CurrentState = new HitState(character, direction, hitweapon.Hits[0]);
    }

    /*                               CHARGEHIT                                  */
    /*--------------------------------------------------------------------------*/
    public virtual void ChargeHit()
    {
        character.CurrentState = new ChargeHitState(character, direction);
    }

    /*                                 DASH                                     */
    /*--------------------------------------------------------------------------*/
    public virtual void Dash()
    {
        character.CurrentState = new DashState(character, direction);
    }

    /*                              CHARGEDASH                                  */
    /*--------------------------------------------------------------------------*/
    public virtual void ChargeDash()
    {
        character.CurrentState = new RunState(character, direction);
    }

    /*                                UPDATE                                    */
    /*--------------------------------------------------------------------------*/
    public virtual void Update() {
        distance += (float)Math.Sqrt(Math.Pow(character.Move.x, 2) + Math.Pow(character.Move.y, 2));
    }

    // Next state
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

    // Sort le nom de l'état
    public virtual string GetName()
    {
        return (Math.Abs(character.Move.x) + Math.Abs(character.Move.y) > 0)?"Iddle":"Move";
    }
    
    // Met à jour la direction du personnage à partir de ses mouvements
    public Direction UpdateDirection()
    {
        if (character.Move.y > 0 && Math.Abs(Math.Abs(character.Move.y) - Math.Abs(character.Move.x)) * 2 / (Math.Abs(character.Move.y) + Math.Abs(character.Move.x)) > 0.4 && Math.Abs(character.Move.y) > Math.Abs(character.Move.x))
        {
            direction = Direction.TOP;
        }
        else if (character.Move.y < 0 && Math.Abs(Math.Abs(character.Move.y) - Math.Abs(character.Move.x)) * 2 / (Math.Abs(character.Move.y) + Math.Abs(character.Move.x)) > 0.4 && Math.Abs(character.Move.y) > Math.Abs(character.Move.x))
        {
            direction = Direction.BOTTOM;
        }
        else if (character.Move.x < 0 && Math.Abs(Math.Abs(character.Move.y) - Math.Abs(character.Move.x)) * 2 / (Math.Abs(character.Move.y) + Math.Abs(character.Move.x)) > 0.4 && Math.Abs(character.Move.x) > Math.Abs(character.Move.y))
        {
            direction = Direction.LEFT;
        }
        else if (character.Move.x > 0 && Math.Abs(Math.Abs(character.Move.y) - Math.Abs(character.Move.x)) * 2 / (Math.Abs(character.Move.y) + Math.Abs(character.Move.x)) > 0.4 && Math.Abs(character.Move.x) > Math.Abs(character.Move.y))
        {
            direction = Direction.RIGHT;
        }
        else if (character.Move.y > 0 && character.Move.x < 0) direction = Direction.TOPLEFT;
        else if (character.Move.y > 0 && character.Move.x > 0) direction = Direction.TOPRIGHT;
        else if (character.Move.y < 0 && character.Move.x < 0) direction = Direction.BOTTOMLEFT;
        else if (character.Move.y < 0 && character.Move.x > 0) direction = Direction.BOTTOMRIGHT;

        return direction;
    }

    // Met à jour les animations du personnage en fonction de l'état et sa direction
    public virtual void UpdateAnim()
    {
        /*
        if (direction == Direction.TOP)
            character.anim.SetInteger("Direction", 2);
        else if (direction == Direction.BOTTOM)
            character.anim.SetInteger("Direction", 0);
        else if (direction == Direction.LEFT)
            character.anim.SetInteger("Direction", 1);
        else if (direction == Direction.RIGHT)
            character.anim.SetInteger("Direction", 3);

        character.anim.SetBool("Move", GetName() == "Move" && (Math.Abs(character.Move.x) + Math.Abs(character.Move.y) > 0));
        character.anim.SetBool("Run", GetName() == "Run");
        character.anim.SetBool("Dash", GetName() == "Dash");
        character.anim.SetBool("Hit", GetName() == "Hit");
        character.anim.SetBool("Iddle", !(GetName() == "Hit" || GetName() == "Move" && (Math.Abs(character.Move.x) + Math.Abs(character.Move.y) > 0) || GetName() == "Run" || GetName() == "Dash"));
        */
    }

    // Fait tourner l'arme
    public virtual void RotateWeapon() { }


    public virtual void AnimProgress() {
        distance = distance % 10;
        percentProgress = distance / 10;
    }
}
