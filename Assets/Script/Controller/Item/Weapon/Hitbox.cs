using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public int damage = 0;
    public HitWeapon weapon;
    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D c)
    {
        if ((c.tag == "Character" || c.tag == "IA") && !c.isTrigger)
        {
            SimpleController cc = c.gameObject.GetComponent(typeof(SimpleController)) as SimpleController;
            Debug.Log("touché " + damage);
            cc.Life -= damage;
        }
    }
}
