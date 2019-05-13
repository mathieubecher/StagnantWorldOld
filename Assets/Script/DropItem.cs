using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    private CharacterController cc;
    public ItemType type;
    public Texture2D texture;
    private Equipment equip;

    void Start()
    {
        equip = new Equipment(type,texture);
    }
    void Update()
    {
        if(cc != null) {
            if (gameObject.GetComponent<Collider2D>().Distance(cc.gameObject.GetComponent<Collider2D>()).distance>0) {
                cc = null;
            }
            if (Input.GetKey(InputConfig.Interact) || Input.GetKey(KeyCode.JoystickButton1))
            { 
                if (equip.type == ItemType.HELMET) cc.helmet = equip;
                else if (equip.type == ItemType.PANTS) cc.pants = equip;
                else if (equip.type == ItemType.MITT) cc.mitt = equip;
                else if (equip.type == ItemType.TORSO) cc.torso = equip;
                (cc.gameObject.GetComponent(typeof(Animator)) as Animator).LoadNude() ;
                Destroy(this.gameObject);
                
            }
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Character" && !c.isTrigger)
        {
            cc = c.gameObject.GetComponent(typeof(CharacterController)) as CharacterController;

            
        }
    }
}
