using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HitType
{
    CIRCULAR, THRUST
}

public class HitConfig 
{
    public string name = "default";
    public float time = 0;
    public float beginRotation = -70;   
    public float endRotation = 70;
    public Vector3 beginPosition = new Vector3(0, 0.12f,0);
    public Vector3 endPosition = new Vector3(0, 0.12f, 0);
    public Vector3 beginScale = new Vector3(0.5f, 1, 1);
    public Vector3 endScale = new Vector3(0.5f, 1, 1);
    public Vector3 endPositionCharacter = Vector3.zero;
    public float beginTime = 0;
    public float endTime = 0.2f;
    public HitConfig next = null;

    public HitType type = HitType.CIRCULAR;
    
    public HitConfig(string name, float time = 0.2f, float beginRotation = -70, float endRotation = 70, float beginTime = 0, float endTime = 0.1f)
    {
        this.name = name;
        this.time = time;
        this.beginRotation = beginRotation;
        this.endRotation = endRotation;
        this.beginTime = beginTime;
        this.endTime = endTime;
    }
    public HitConfig(string name, float time, float beginRotation, float endRotation, Vector3 beginPosition, Vector3 endPosition, Vector3 beginScale, Vector3 endScale, Vector3 endPositionCharacter, float beginTime = 0,  float endTime = 0.1f)
    {
        this.name = name;
        this.time = time;
        this.beginRotation = beginRotation;
        this.endRotation = endRotation;
        this.beginPosition = beginPosition;
        this.endPosition = endPosition;
        this.beginScale = beginScale;
        this.endScale = endScale;
        this.endPositionCharacter = endPositionCharacter;
        this.beginTime = beginTime;
        this.endTime = endTime;
    }
}
