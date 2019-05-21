using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBarGestor : MonoBehaviour
{
    public GameObject character;
    public GameObject lifebar;
    public GameObject background;
    private float maxWidth;
    // Start is called before the first frame update
    void Start()
    {
        maxWidth = lifebar.transform.localScale.x;
        hide();
    }

    // Update is called once per frame
    void Update()
    {
        SimpleController pc = character.GetComponent(typeof(SimpleController)) as SimpleController;
        if (pc.LIFE > pc.Life && pc.Life > 0) show();
        else hide();    
        lifebar.transform.localScale = new Vector3((maxWidth / pc.LIFE)*pc.Life, lifebar.transform.localScale.y, lifebar.transform.localScale.z);
    }
    void show()
    {
        background.SetActive(true);
        lifebar.SetActive(true);
    }
    void hide()
    {
        background.SetActive(false);
        lifebar.SetActive(false);
    }
}
