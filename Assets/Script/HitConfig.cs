using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HitType
{
    CIRCULAR, THRUST
}

public class HitConfig 
{
    public float time = 0.2f;
    public float beginRotation = -70;
    public float endRotation = 70;
    public HitType type = HitType.CIRCULAR;
    
    public HitConfig()
    {

    }
}
