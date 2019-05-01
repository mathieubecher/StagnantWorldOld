using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FPS : MonoBehaviour
{
    private Text t;
    // Start is called before the first frame update
    void Start()
    {
        t = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
        t.text = "" + Math.Floor(1 / Time.deltaTime);
    }
}
    