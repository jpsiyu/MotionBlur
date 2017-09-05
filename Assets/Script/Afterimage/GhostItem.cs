using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostItem : MonoBehaviour {

    public float duration;
    public float deleteTime;
    public MeshRenderer meshRenderer;

    private void Update() {
        float tempTime = deleteTime - Time.time;
        if (tempTime <= 0)
            GameObject.Destroy(gameObject);
        else if (meshRenderer.material) {
            float rate = tempTime / duration;
            Color cal = meshRenderer.material.GetColor("_RimColor");
            cal.a *= rate;
            meshRenderer.material.SetColor("_RimColor", cal);
        }
    }



}
