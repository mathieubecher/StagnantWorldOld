using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimMap
{
    private List<AnimSpritePos> allAnim;
    public CharacterAnimMap()
    {
        allAnim = new List<AnimSpritePos>();
        Add(new AnimSpritePos("Iddle", Direction.BOTTOM, 0, 0, 2));
        Add(new AnimSpritePos("Iddle", Direction.TOP, 0, 12, 2));
        Add(new AnimSpritePos("Iddle", Direction.RIGHT, 0, 24, 2));
        Add(new AnimSpritePos("Iddle", Direction.LEFT, 0, 36, 2));

        Add(new AnimSpritePos("Move", Direction.BOTTOM, 0, 2, 4));
        Add(new AnimSpritePos("Move", Direction.TOP, 0, 14, 4));
        Add(new AnimSpritePos("Move", Direction.RIGHT, 0, 26, 4));
        Add(new AnimSpritePos("Move", Direction.LEFT, 0, 38, 4));

        Add(new AnimSpritePos("Run", Direction.BOTTOM, 0, 2, 4));
        Add(new AnimSpritePos("Run", Direction.TOP, 0, 14, 4));
        Add(new AnimSpritePos("Run", Direction.RIGHT, 0, 26, 4));
        Add(new AnimSpritePos("Run", Direction.LEFT, 0, 38, 4));

        Add(new AnimSpritePos("Hit", Direction.BOTTOM, 0, 6, 4));
        Add(new AnimSpritePos("Hit", Direction.BOTTOM, 1, new int[] {9, 10, 11, 6}));
        Add(new AnimSpritePos("Hit", Direction.BOTTOM, 2, 6, 4));
    }
    public bool Add(AnimSpritePos anim)
    {
        for (int i = 0; i < allAnim.Count; i++)
        {
            if (allAnim[i].StateName == anim.StateName && allAnim[i].Direction == anim.Direction && allAnim[i].Id == anim.Id) return false;
        }
        allAnim.Add(anim);
        return true;
    }
    public AnimSpritePos Get(string stateName, Direction direction = Direction.BOTTOM, int id = 0)
    {
        for(int i = 0; i < allAnim.Count; i++)
        {
            if (allAnim[i].StateName == stateName && allAnim[i].Direction == direction && allAnim[i].Id == id) return new AnimSpritePos(allAnim[i]);
        }
        return new AnimSpritePos(allAnim[0]);
    }
}
