using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OriginalState
{
    private SimpleController controller;
    protected Direction direction;
    protected string nextState = "";
    public float percentProgress = 0;

    public SimpleController Controller { get => controller; set => controller = value; }
    public Direction Direction { get => direction; set => direction = value; }


    public OriginalState(SimpleController pc, Direction direction = Direction.BOTTOM)
    {
        this.Direction = direction;
        controller = pc;
        

    }
    /*                                  MOVE                                    */
    /*--------------------------------------------------------------------------*/
    public virtual void Move()
    {
    }

    /*                                  HIT                                     */
    /*--------------------------------------------------------------------------*/
    public virtual void Hit()
    {
    }

    /*                               CHARGEHIT                                  */
    /*--------------------------------------------------------------------------*/
    public virtual void ChargeHit()
    {
    }

    /*                                 DASH                                     */
    /*--------------------------------------------------------------------------*/
    public virtual void Dash()
    {
    }

    /*                              CHARGEDASH                                  */
    /*--------------------------------------------------------------------------*/
    public virtual void ChargeDash()
    {
    }

    /*                                UPDATE                                    */
    /*--------------------------------------------------------------------------*/
    public virtual void Update()
    {
    }

    public virtual string GetName()
    {
        return "Void";
    }
}
