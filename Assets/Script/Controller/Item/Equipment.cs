using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Equipment : Item
{
    
    public Texture2D texture;

    public Equipment(ItemType type, Texture2D texture) : base(type)
    {
        this.texture = texture;
    }
}
