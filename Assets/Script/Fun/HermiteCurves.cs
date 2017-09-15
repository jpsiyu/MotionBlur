using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermiteCurves : MonoBehaviour {

    public GameObject start, startTangent, end, endTangent;
    public Color color = Color.white;
    public float width = 0.2f;
    public int numberOfPoints = 20;
    private LineRenderer lineRenderer;

    private void Start() {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
    }

    private void Update() {
        if (start == null || startTangent == null || end == null || endTangent == null)
            return;

        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;

        if (numberOfPoints > 0)
            lineRenderer.positionCount = numberOfPoints;

        Vector3 p0 = start.transform.position;
        Vector3 p1 = end.transform.position;
        Vector3 m0 = startTangent.transform.position;
        Vector3 m1 = endTangent.transform.position;
        float t;
        Vector3 position;

        for (int i = 0; i < numberOfPoints; i++) {
            t = 1 / (numberOfPoints - 1.0f);
            position = (2.0f * t * t * t - 3.0f * t * t + 1.0f) * p0 + (t * t * t - 2.0f * t * t + t) * m0 + (-2.0f * t * t * t + 3.0f * t * t) * p1 + (t * t * t - t * t) * m1;
            lineRenderer.SetPosition(i, position);
        }

    }
}
