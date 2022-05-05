using UnityEngine;
using System.Collections.Generic;

public class GoldenRay : MonoBehaviour
{
    public List<Vector2> linePoints = new List<Vector2>();

    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider2d;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        edgeCollider2d = GetComponent<EdgeCollider2D>();
    }

    private void Update()
    {
        linePoints[0] = lineRenderer.GetPosition(0);
        linePoints[1] = lineRenderer.GetPosition(1);

        edgeCollider2d.SetPoints(linePoints.ConvertAll(p => (Vector2)transform.InverseTransformPoint(p)));
    }
}
