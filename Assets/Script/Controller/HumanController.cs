using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : SimpleController
{
    private State currentState;

    public EquipmentPlace weapon;
    public EquipmentPlace leftWeapon;
    public bool left = false;


    public EquipmentPlace helmet;
    public EquipmentPlace pants;
    public EquipmentPlace mitt;
    public EquipmentPlace torso;

    public State CurrentState { get => currentState; set => currentState = value; }


    protected override void Start()
    {
        base.Start();
        currentState = new State(this);
    }

    public GameObject GetWeapon()
    {
        if (left) return (leftWeapon.equip != null)?(leftWeapon.equip as Weapon).hitweapon.gameObject : null;
        else return (weapon.equip != null) ? (weapon.equip as Weapon).hitweapon.gameObject:null;
    }
}
