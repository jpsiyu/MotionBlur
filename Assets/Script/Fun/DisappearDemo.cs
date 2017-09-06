using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearDemo : MonoBehaviour {

    private void Start() {
        GameObject prefab = Resources.Load("Prefab/Human") as GameObject;
        GameObject human = GameObject.Instantiate(prefab);

        human.AddComponent<CombineMeshes>();

        Object materialAsset = Resources.Load("Material/Disappear");
        Material material = Material.Instantiate(materialAsset) as Material;

        MeshRenderer humanMeshRenderer = human.GetComponent<MeshRenderer>();
        humanMeshRenderer.material = material;

    }
}
