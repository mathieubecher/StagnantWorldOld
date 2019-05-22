using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public float damage = 1;
    public GameObject weapon;
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
        if(c.tag == "Character" && !c.isTrigger) {
            SimpleController cc = c.gameObject.GetComponent(typeof(SimpleController)) as SimpleController;
            cc.Life -= 1;
            Debug.Log(cc.Life + "/" + cc.LIFE);
        }
    }
}
