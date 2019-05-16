using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSpritePos
{
    private string stateName;
    private int id;
    private Direction direction;
    private int actualSprite;
    private float lastPercentProgress = 0;
    private int[] anim;

    public string StateName { get => stateName; }
    public int Id { get => id; }
    public Direction Direction { get => direction; }
    public int ActualSprite { get => anim[actualSprite]; }
    public int[] Anim { get => anim;  }

    public AnimSpritePos(string stateName, Direction direction, int id, int begin, int end)
    {
        this.stateName = stateName;
        this.direction = direction;
        this.id = id;
        actualSprite = 0;
        anim = new int[end];
        for (int i = 0; i < end; ++i) anim[i] = i + begin;
    }
    public AnimSpritePos(string stateName, Direction direction, int id, int[] anim)
    {
        this.stateName = stateName;
        this.direction = direction;
        this.id = id;
        actualSprite = 0;
        this.anim = anim;
    }
    public AnimSpritePos(AnimSpritePos anim)
    {
        this.stateName = anim.StateName;
        this.direction = anim.Direction;
        this.id = anim.Id;
        this.anim = anim.Anim;
        actualSprite = 0;
    }
    public bool Next(float percentProgress)
    {
        int last = actualSprite;
        while (GetAnimProgress() < percentProgress && actualSprite + 1 < anim.Length)
        {
            ++actualSprite;
        }
        if (percentProgress < lastPercentProgress) actualSprite = 0;
        lastPercentProgress = percentProgress;
        return actualSprite != last;
    }
    public float GetAnimProgress()
    {
        return (float)(actualSprite + 1) / (float)(anim.Length);
    }
}
