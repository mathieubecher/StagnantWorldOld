using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    private HumanController cc;
    public string name;
    public ItemType type;
    public Texture2D texture;
    public Item item;

    void Start()
    {

        item = new Equipment(name,type,GetComponent<SpriteRenderer>().sprite, texture);
    }
    void Update()
    {
        if(cc != null) {
            if (gameObject.GetComponent<Collider2D>().Distance(cc.gameObject.GetComponent<Collider2D>()).distance>0) {
                cc = null;
            }
            if (Input.GetKeyDown(InputConfig.Interact) || Input.GetKeyDown(KeyCode.JoystickButton1))
            { 
                if(item is Equipment) {
                    Equipment equip = item as Equipment;
                    if (equip.type == ItemType.HELMET) cc.helmet.equip = equip;
                    else if (equip.type == ItemType.PANTS) cc.pants.equip = equip;
                    else if (equip.type == ItemType.MITT) cc.mitt.equip = equip;
                    else if (equip.type == ItemType.TORSO) cc.torso.equip = equip;
                    (cc.gameObject.GetComponent(typeof(Animator)) as Animator).changeStateDir = true;
                }
                Destroy(this.gameObject);
                Destroy(this);
                
            }
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Character" && !c.isTrigger)
        {
            cc = c.gameObject.GetComponent(typeof(HumanController)) as HumanController;

            
        }
    }
}
