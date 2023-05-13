using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.AI;

public class GenerateMesh : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Sprite sprite;

    [Button]
    public void CreateMeshFromSprite()
    {
        meshRenderer.GetComponent<MeshFilter>().sharedMesh = SpriteToMesh(sprite);
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
