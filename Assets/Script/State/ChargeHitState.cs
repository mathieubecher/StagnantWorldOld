using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeHitState : State
{
    public float time;
    public GameObject weapon;
    public float rotate = 0;

    /*                              CONSTRUCTEUR                                */
    /*--------------------------------------------------------------------------*/
    public ChargeHitState(SimpleController controller, Direction direction) : base(controller, direction)
    {
        HitWeapon hitweapon = character.GetWeapon().GetComponent(typeof(HitWeapon)) as HitWeapon;
        hitweapon.LoadCharge(character,this);
    }

    /*                                  MOVE                                    */
    /*--------------------------------------------------------------------------*/
    public override void Move()
    {
        HitWeapon hitWeapon = character.GetWeapon().GetComponent(typeof(HitWeapon)) as HitWeapon;
        character.GetComponent<Rigidbody2D>().velocity = new Vector3(character.Move.x* hitWeapon.chargeMove, character.Move.y* hitWeapon.chargeMove);
        UpdateDirection();
    }

    /*                                 DASH                                     */
    /*--------------------------------------------------------------------------*/
    public override void Dash(){ }

    /*                                  HIT                                     */
    /*--------------------------------------------------------------------------*/
    public override void Hit()
    {
        HitWeapon hitweapon = character.GetWeapon().GetComponent(typeof(HitWeapon)) as HitWeapon;
        hitweapon.ChargeHitEnd(character);
        if (hitweapon.ChargeHit != null)
            character.CurrentState = new HitState(character, direction, hitweapon.ChargeHit);
        else
            character.CurrentState = new State(character,direction );
    }

    /*                              CHARGEDASH                                  */
    /*--------------------------------------------------------------------------*/
    public override void ChargeDash(){ }

    /*                               CHARGEHIT                                  */
    /*--------------------------------------------------------------------------*/
    public override void ChargeHit(){ }

    /*                                UPDATE                                    */
    /*--------------------------------------------------------------------------*/
    public override void Update()
    {
        HitWeapon hitweapon = character.GetWeapon().GetComponent(typeof(HitWeapon)) as HitWeapon;
        hitweapon.ChargeHitUpdate(character);
    }

    // Sort le nom de l'état
    public override string GetName()
    {
        return "ChargeHit";
    }

    // Fait tourner l'arme
    public override void RotateWeapon()
    {
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
        if (weapon != null)
        {
            weapon.transform.eulerAngles = new Vector3(0, 0, rotate);
        }
    }
}
