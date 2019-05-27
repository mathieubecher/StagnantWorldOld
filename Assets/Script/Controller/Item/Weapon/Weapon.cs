using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Equipment
{
    public HitWeapon hitweapon;
    public Weapon(string name, ItemType type, Sprite sprite, Texture2D texture, HitWeapon hitweapon) : base(name, type, sprite, texture)
    {
        this.hitweapon = hitweapon;
    }
}
