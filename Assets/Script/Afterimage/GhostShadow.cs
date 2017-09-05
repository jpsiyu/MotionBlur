using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostShadow : MonoBehaviour {

    private float duration = 0.5f;
    private float interval = 0.3f;
    private float lastTime = 0f;
    private Vector3 lastPos = Vector3.zero;

    [Range(-1, 2)]
    public float intension = 1;

    private MeshRenderer[] meshRender;
    private Shader ghostShader;

    private void Start() {
        meshRender = gameObject.GetComponentsInChildren<MeshRenderer>();
        ghostShader = Shader.Find("Custom/RimColor");
    }

    private void Update() {
        if (lastPos == transform.position)
            return;
        lastPos = transform.position;

        if (Time.time - lastTime < interval)
            return;

        lastTime = Time.time;
        if (meshRender == null) return;
        for (int i = 0; i < meshRender.Length; i++)
        {
            MeshFilter source = meshRender[i].gameObject.GetComponent<MeshFilter>();
            Mesh copyMesh = CopyMesh(source.mesh);

            GameObject go = CopyGameObject(meshRender[i].gameObject);
            GenGhostLifeCycle(go, copyMesh, meshRender[i].material);
        }
    }

    private Mesh CopyMesh(Mesh source) {
        Mesh mesh = new Mesh();
        mesh.vertices = source.vertices;
        mesh.uv = source.uv;
        mesh.triangles = source.triangles;
        return mesh;
    }

    private GameObject CopyGameObject(GameObject source) {
        GameObject go = new GameObject();
        go.transform.localScale = source.transform.localScale;
        go.transform.position = source.transform.position;
        go.transform.rotation = source.transform.rotation;
        go.hideFlags = HideFlags.HideAndDontSave;
        return go;
    }

    private void GenGhostLifeCycle(GameObject ghost, Mesh copyMesh, Material source) {
        GhostItem item = ghost.AddComponent<GhostItem>();
        item.deleteTime = Time.time + duration;
        MeshFilter filter = ghost.AddComponent<MeshFilter>();
        filter.mesh = copyMesh;
        MeshRenderer render = ghost.AddComponent<MeshRenderer>();
        render.material = source;
        render.material.shader = ghostShader;
        render.material.SetFloat("_Intension", intension);
        item.meshRenderer = render;
    }
}
