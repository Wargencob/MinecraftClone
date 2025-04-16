using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(MeshFilter)), RequireComponent (typeof(MeshRenderer)), RequireComponent (typeof(MeshCollider))]
public class Chunk : MonoBehaviour
{
    public const int chunkHeight = 16;
    public const int chunkWidth = 16;
    public const int chunkDepth = 128;

    public int id;
    public int x;
    public int z;

    public List<Vector2> blockUVSList;

    public ChunkMesh mesh;
    public Chunk()
    {
        mesh = new ChunkMesh();
        blockUVSList = new List<Vector2>();
    }
}
