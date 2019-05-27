using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Equipment : Item
{
    
    public Texture2D texture;

    public Equipment(string name, ItemType type, Sprite sprite, Texture2D texture) : base(name, type, sprite)
    {
        this.texture = texture;
    }
}
