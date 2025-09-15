using UnityEngine;


public static class Utils
{
    public static void DrawCircle(Vector3 position, float radius, Color color, float duration = 1f)
    {
#if UNITY_EDITOR
        UnityEditor.Handles.color = color;
        UnityEditor.Handles.DrawWireDisc(position, Vector3.forward, radius);
#endif
    }
}