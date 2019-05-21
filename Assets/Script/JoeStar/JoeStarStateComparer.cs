using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoeStarStateComparer : IComparer<JoeStarState>
{
    public int Compare(JoeStarState x, JoeStarState y)
    {
        if (x.distance < y.distance) return -1;
        else if (x.distance > y.distance) return 1;
        else return 0;

    }
}
