using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spline : MonoBehaviour {

    public List<GameObject> controlPoints = new List<GameObject>();
    public Color color = Color.white;
    public float width = 0.2f;
    public int numberOfPoints = 20;
    LineRenderer lineRenderer;

    private void Start() {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
    }

    private void Update() {
        if (null == lineRenderer || controlPoints == null || controlPoints.Count < 3)
            return;

        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;

        if (numberOfPoints < 2)
            numberOfPoints = 2;

        lineRenderer.positionCount = numberOfPoints * (controlPoints.Count - 2);

        Vector3 p0, p1, p2;
        for (int j = 0; j < controlPoints.Count - 2; j++) {
            if (controlPoints[j] == null || controlPoints[j + 1] == null || controlPoints[j + 2] == null)
                return;

            p0 = 0.5f * (controlPoints[j].transform.position + controlPoints[j + 1].transform.position);
            p1 = controlPoints[j + 1].transform.position;
            p2 = 0.5f * (controlPoints[j + 1].transform.position + controlPoints[j + 2].transform.position);

            Vector3 position;
            float t;
            float pointStep = 1.0f / numberOfPoints;
            if (j == controlPoints.Count - 3)
                pointStep = 1.0f / (numberOfPoints - 1.0f);
            for (int i = 0; i < numberOfPoints; i++) {
                t = i * pointStep;
                position = (1.0f - t) * (1.0f - t) * p0 + 2.0f * (1.0f - t) * t * p1 + t * t * p2;
                lineRenderer.SetPosition(i + j * numberOfPoints, position);
            }
        }
    }

}
