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

    public ChunkMesh mesh;

    public Mesh chunkMesh;
    public Chunk()
    {
        mesh = new ChunkMesh();
    }
}
