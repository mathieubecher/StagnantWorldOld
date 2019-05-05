using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputConfig
{
    private static KeyCode top = KeyCode.Z;
    private static KeyCode left = KeyCode.Q;
    private static KeyCode bottom = KeyCode.S;
    private static KeyCode right = KeyCode.D;
    private static KeyCode hit = KeyCode.Space;
    private static KeyCode hitLeft = KeyCode.F;
    private static KeyCode dash = KeyCode.LeftShift;

    public static KeyCode Top { get => top; set => top = value; }
    public static KeyCode Left { get => left; set => left = value; }
    public static KeyCode Bottom { get => bottom; set => bottom = value; }
    public static KeyCode Right { get => right; set => right = value; }
    public static KeyCode Hit { get => hit; set => hit = value; }
    public static KeyCode Dash { get => dash; set => dash = value; }
    public static KeyCode HitLeft { get => hitLeft; set => hitLeft = value; }
}
    