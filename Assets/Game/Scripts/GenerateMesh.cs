using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMesh : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer.GetComponent<MeshFilter>().sharedMesh = SpriteToMesh(sprite);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Mesh SpriteToMesh(Sprite sprite)
    {
        Mesh mesh = new Mesh();
        mesh.vertices = Array.ConvertAll(sprite.vertices, i => (Vector3)i);
        mesh.uv = sprite.uv;
        mesh.triangles = Array.ConvertAll(sprite.triangles, i => (int)i);

        return mesh;
    }
}
