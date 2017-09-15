using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderToyHelper : MonoBehaviour {

    private Material _material = null;
    private bool _isDragging = false;

    private void Start() {
        Renderer render = GetComponent<Renderer>();
        if (render != null) {
            _material = render.material;
        }
        _isDragging = false;
    }

    private void Update() {
        Vector3 mousePosition = Vector3.zero;
        if (_isDragging) {
            mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f);
        }
        else {
            mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f);
        }
    }

    private void OnMouseDown() {
        _isDragging = true;
    }

    private void OnMouseUp() {
        _isDragging = false;
    }
}
