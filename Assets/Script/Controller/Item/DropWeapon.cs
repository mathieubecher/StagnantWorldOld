using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropWeapon : DropItem
{
    public HitWeapon weapon;
    protected override void Start()
    {

        item = new Weapon(name, type, GetComponent<SpriteRenderer>().sprite, texture, weapon);
    }
    protected override void Update()
    {
        if (cc != null)
        {
            if (gameObject.GetComponent<Collider2D>().Distance(cc.gameObject.GetComponent<Collider2D>()).distance > 0)
            {
                cc = null;
            }
            if (Input.GetKeyDown(InputConfig.Interact) || Input.GetKeyDown(KeyCode.JoystickButton1) && first)
            {
                first = false;
                Weapon equip = item as Weapon;
                if (equip.type == ItemType.LEFT) cc.leftWeapon = equip;
                else if (equip.type == ItemType.RIGHT) cc.weapon = equip;
                (cc.gameObject.GetComponent(typeof(Animator)) as Animator).changeStateDir = true;
                Destroy(this.gameObject);
                Destroy(this);

            }
        }
    }
}
