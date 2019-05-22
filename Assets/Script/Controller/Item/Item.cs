using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    HELMET, PANTS, MITT, TORSO
}

public class Item
{
    public ItemType type;
    public Item(ItemType type)
    {
        this.type = type;
    }
}
