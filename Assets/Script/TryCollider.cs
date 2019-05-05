using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryCollider : MonoBehaviour
{
    private float time = 0;
    public Sprite activesprite;
    public Sprite nactivesprite;
    private bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= 0.5 && active)
        {
            active = false;
            GetComponent<SpriteRenderer>().sprite = nactivesprite;
        }
    }
    
    public void activeHitbox()
    {
        GetComponent<SpriteRenderer>().sprite = activesprite;
        active = true;
        time = 0;
    }
}
