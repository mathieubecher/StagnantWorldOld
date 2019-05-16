using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSpritePos
{
    private string stateName;
    private int id;
    private Direction direction;
    private int begin;
    private int end;
    private int actualSprite;
    private float lastPercentProgress = 0;

    public string StateName { get => stateName; }
    public int Id { get => id; }
    public Direction Direction { get => direction; }
    public int End { get => end;  }
    public int Begin { get => begin; }
    public int ActualSprite { get => actualSprite; }

    public AnimSpritePos(string stateName, Direction direction, int id, int begin, int end)
    {
        this.stateName = stateName;
        this.direction = direction;
        this.id = id;
        this.begin = begin;
        this.end = end;
        actualSprite = begin;
    }
    public AnimSpritePos(AnimSpritePos anim)
    {
        this.stateName = anim.StateName;
        this.direction = anim.Direction;
        this.id = anim.Id;
        this.begin = anim.Begin;
        this.end = anim.End;
        actualSprite = begin;
    }
    public bool Next(float percentProgress)
    {
        int last = actualSprite;
        while (GetAnimProgress() < percentProgress && actualSprite + 1 < begin + end)
        {
            ++actualSprite;
        }
        if (percentProgress < lastPercentProgress) actualSprite = begin;
        lastPercentProgress = percentProgress;
        return actualSprite != last;
    }
    public float GetAnimProgress()
    {
        return (float)(actualSprite + 1 - begin) / (float)(end);
    }
}
