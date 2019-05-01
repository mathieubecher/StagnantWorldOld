using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputConfig
{
    private static KeyCode top = KeyCode.W;
    private static KeyCode left = KeyCode.A;
    private static KeyCode bottom = KeyCode.S;
    private static KeyCode right = KeyCode.D;

    public static KeyCode Top { get => top; set => top = value; }
    public static KeyCode Left { get => left; set => left = value; }
    public static KeyCode Bottom { get => bottom; set => bottom = value; }
    public static KeyCode Right { get => right; set => right = value; }
}
    