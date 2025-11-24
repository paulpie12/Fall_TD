using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RangeCircle : MonoBehaviour
{
    private LineRenderer line;
    public int segments = 60;
    public float radius = 4f;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.useWorldSpace = false; // local space! circle is relative to tower
        line.loop = true;
        line.enabled = true; // hidden until tower is selected
        DrawCircle();
    }

    public void UpdateRadius(float newRadius)
    {
        radius = newRadius;
        DrawCircle();
    }

    private void DrawCircle()
    {
        if (line == null)
            line = GetComponent<LineRenderer>();

        line.positionCount = segments;
        float angleStep = 360f / segments;

        for (int i = 0; i < segments; i++)
        {
            float rad = angleStep * i * Mathf.Deg2Rad;
            float x = Mathf.Cos(rad) * radius;
            float z = Mathf.Sin(rad) * radius;

            // LOCAL SPACE: just x and z, Y slightly above ground
            line.SetPosition(i, new Vector3(x, 0.05f, z));
        }
    }

    public void ShowCircle(bool show)
    {
        if (line != null)
            line.enabled = show;
    }
}
