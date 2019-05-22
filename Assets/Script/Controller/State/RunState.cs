using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RunState : State
{

    /*                              CONSTRUCTEUR                                */
    /*--------------------------------------------------------------------------*/
    public RunState(SimpleController pc,Direction direction):base(pc,direction)
    {
    }

    /*                                  MOVE                                    */
    /*--------------------------------------------------------------------------*/
    public override void Move() {
        character.GetComponent<Rigidbody2D>().velocity = character.Move * 1.5f;
        UpdateDirection();
    }
    /*                                  HIT                                     */
    /*--------------------------------------------------------------------------*/
    public override void Hit()
    { }

    /*                               CHARGEHIT                                  */
    /*--------------------------------------------------------------------------*/
    public override void ChargeHit()
    { }

    /*                                 DASH                                     */
    /*--------------------------------------------------------------------------*/
    public override void Dash()
    {
        character.CurrentState = new State(character, direction);
    }
    
    /*                              CHARGEDASH                                  */
    /*--------------------------------------------------------------------------*/
    public override void ChargeDash()
    { }


    // Sort le nom de l'état
    public override string GetName()
    {
        return "Run";
    }
}
