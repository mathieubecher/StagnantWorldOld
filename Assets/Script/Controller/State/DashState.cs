using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DashState : State
{
    private float speed;
    private float time;
    private float TIME;
    private float idleTime;
    private Vector3 moveDash;

    /*                              CONSTRUCTEUR                                */
    /*--------------------------------------------------------------------------*/
    public DashState(HumanController pc, Direction direction, float speed = 2.2f, float time = 0.25f, float idleTime = 0.15f) : base(pc, direction)
    {
        this.speed = speed;
        this.time = time;
        this.TIME = time;
        this.idleTime = idleTime;
        float actualSpeed = (float)Math.Sqrt(Math.Pow(character.Move.x, 2) + Math.Pow(character.Move.y, 2));
        this.moveDash = character.Move*speed/actualSpeed;
    }

    /*                                  MOVE                                    */
    /*--------------------------------------------------------------------------*/
    public override void Move() { character.GetComponent<Rigidbody2D>().velocity = new Vector3(moveDash.x, moveDash.y); }

    /*                                  HIT                                     */
    /*--------------------------------------------------------------------------*/
    public override void Hit() {
        if (time <= 0) base.Hit();
        else nextState = "hit";
    }

    /*                                 DASH                                     */
    /*--------------------------------------------------------------------------*/
    public override void Dash() { nextState = "dash"; }

    /*                                UPDATE                                    */
    /*--------------------------------------------------------------------------*/
    public override void Update()
    {
        time -= Time.deltaTime;
        if (time <= -idleTime)
        {
            base.UpdateDirection();
            NextState();
        }
        else if (time <= 0)
        {
            moveDash = Vector3.zero;
        }
        else if (time <= 0.2 * TIME)
        {
            character.gameObject.tag = "Character";
        }
        else if (time <= 0.95f * TIME)
        {
            character.gameObject.tag = "Invulnerable";
        }
    }

    // Sort le nom de l'état
    public override string GetName()
    {
        return "Dash";
    }
    public override void AnimProgress() {

    }

}
