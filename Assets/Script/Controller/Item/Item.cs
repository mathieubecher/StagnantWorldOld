using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    HELMET, PANTS, MITT, TORSO
}

public class Item
{
    public string name;
    public ItemType type;
    public Sprite sprite;

    public Item(string name, ItemType type, Sprite sprite)
    {
        this.name = name;
        this.sprite = sprite;
        this.type = type;
    }
}
