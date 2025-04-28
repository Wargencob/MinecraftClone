using UnityEngine;

public class ChunkRemasher : IChunkRemasher
{
    internal Mesh chunkMesh;
    internal MeshCollider chunkCollider;

    public ChunkRemasher(Mesh chunkMesh)
    {
        this.chunkMesh = chunkMesh;
    }
    public void AddBlockToChunk(RaycastHit hit)
    {
        chunkMesh = GetMeshFromTheHit(hit);
        chunkCollider = GetColliderFromTheHit(hit);

        Vector3 blockPosition = GetBlockPositionFromTheHit(hit) + hit.normal;

        Debug.Log(blockPosition);

        chunkMesh.Clear();
        BlockType[,,] blocks = GetBlocksFromTheHit(hit);

        blocks[(int)blockPosition.x, (int)blockPosition.y, (int)blockPosition.z] = BlockType.Dirt;

        ChunkMeshGenerator chunkMeshGenerator = new ChunkMeshGenerator(blocks, chunkMesh);

        chunkMeshGenerator.GenerateMesh();
        chunkMeshGenerator.GenerateMeshCollider(chunkCollider);

        chunkMesh = chunkMeshGenerator.GetMesh();

        RecalculateMesh();
    }
    public void RemoveBlockFromChunk(RaycastHit hit)
    {
        chunkMesh = GetMeshFromTheHit(hit);
        chunkCollider = GetColliderFromTheHit(hit);

        Vector3 blockPosition = GetBlockPositionFromTheHit(hit);

        chunkMesh.Clear();
        BlockType[,,] blocks = GetBlocksFromTheHit(hit);

        blocks[(int)blockPosition.x, (int)blockPosition.y, (int)blockPosition.z] = BlockType.Air;

        ChunkMeshGenerator chunkMeshGenerator = new ChunkMeshGenerator(blocks, chunkMesh);

        chunkMeshGenerator.GenerateMesh();
        chunkMeshGenerator.GenerateMeshCollider(chunkCollider);

        chunkMesh = chunkMeshGenerator.GetMesh();

        RecalculateMesh();
    }
    private static BlockType[,,] GetBlocksFromTheHit(RaycastHit hit)
    {
        return hit.collider.GetComponent<ChunkInfoContainer>().ChunkInfo.Blocks;
    }
    public Mesh GetMeshFromTheHit(RaycastHit hit)
    {
        return hit.collider.GetComponent<MeshFilter>().mesh;
    }
    private MeshCollider GetColliderFromTheHit(RaycastHit hit)
    {
        return hit.collider.GetComponent<MeshCollider>();
    }
    private static Vector3 GetBlockPositionFromTheHit(RaycastHit hit)
    {
        Vector3 localHitPoint = hit.transform.InverseTransformPoint(hit.point);
        return ((Vector3)(Vector3Int.FloorToInt(localHitPoint) + Vector3Int.CeilToInt(localHitPoint)) / 2
                    + (-0.5f) * hit.normal - new Vector3(0.5f, 0.5f, 0.5f));
    }
    private void RecalculateMesh()
    {
        chunkMesh.RecalculateBounds();
        chunkMesh.RecalculateNormals();
    }
    public Mesh GetMesh()
    {
        return chunkMesh;
    }
}
