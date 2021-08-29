using UnityEngine;

public static class Directions
{
    public enum Direction { left, leftUp, up, rightUp, right, rightDown, down, leftDown };

    public static Vector2[] directions =
        {
        Vector2.left,
        Vector2.left + Vector2.up,
        Vector2.up,
        Vector2.up + Vector2.right,
        Vector2.right,
        Vector2.right + Vector2.down,
        Vector2.down,
        Vector2.down + Vector2.left,
        Vector2.left //this is here to be able to use "Direction.leftdown + 1" in a useful way
    };

    public static Vector2 GetDir(Direction dir)
    {
        return directions[(int)dir];
    }
}