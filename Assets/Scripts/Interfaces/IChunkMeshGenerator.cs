using UnityEngine;

public interface IChunkMeshGenerator : IMeshGenerator
{
    public void GenerateMeshCollider(MeshCollider collider);
}