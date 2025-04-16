using BlockGen;
using ChunkGen;
using System.Linq;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class ChunkReMasher
{
    private readonly BlockGenerator blockGenerator = new();

    public Mesh AddBlockToChunkMeshByHit(RaycastHit hit)
    {
        var chunk = hit.collider.GetComponent<Chunk>();
        var blockList = chunk.mesh.meshBlocks;

        Vector3 localPoint = hit.collider.transform.InverseTransformPoint(hit.point);
        Vector3 blockCenter = CalculateBlockCenter(localPoint, hit.normal);
        Vector3 blockPosition = blockCenter + hit.normal - Vector3.one * 0.5f;

        Block newBlock = blockGenerator.GenerateOneBlock(
            Mathf.FloorToInt(blockPosition.x),
            Mathf.FloorToInt(blockPosition.y),
        Mathf.FloorToInt(blockPosition.z)
        );

        newBlock.uvs.Add(new Vector2(0 * 0.5f, 1 * 0.5f));
        newBlock.uvs.Add(new Vector2(0 * 0.5f, 2 * 0.5f));
        newBlock.uvs.Add(new Vector2(1 * 0.5f, 1 * 0.5f));
        newBlock.uvs.Add(new Vector2(1 * 0.5f, 2 * 0.5f));

        newBlock.blockCenter = blockCenter;
        newBlock.blockPosition = blockPosition;

        chunk.blockUVSList.AddRange(newBlock.uvs);

        blockList.Add(newBlock);
        return UpdateChunkMesh(chunk);
    }

    public Mesh RemoveBlockFromTheMeshByHit(RaycastHit hit)
    {
        var chunk = hit.collider.GetComponent<Chunk>();
        var blockList = chunk.mesh.meshBlocks;

        Vector3 localPoint = hit.collider.transform.InverseTransformPoint(hit.point);
        Vector3 blockCenter = CalculateBlockCenter(localPoint, hit.normal);
        Vector3 blockPosition = blockCenter - Vector3.one * 0.5f;

        var blockToRemove = blockList.FirstOrDefault(b => b.blockPosition == blockPosition);
        if (blockToRemove != null)
        {
            blockList.Remove(blockToRemove);
        }

        UpdateNeighborBlocks(blockList, blockCenter);

        return UpdateChunkMesh(chunk);
    }

    private Vector3 CalculateBlockCenter(Vector3 localPoint, Vector3 normal)
    {
        return ((Vector3)(Vector3Int.FloorToInt(localPoint) + Vector3Int.CeilToInt(localPoint)) / 2f) + (-0.5f * normal);
    }

    private void UpdateNeighborBlocks(System.Collections.Generic.List<Block> blockList, Vector3 blockCenter)
    {
        Vector3[] directions = {
            Vector3.right, Vector3.left,
            Vector3.forward, Vector3.back,
            Vector3.up, Vector3.down
        };

        foreach (var dir in directions)
        {
            Vector3 neighborCenter = blockCenter + dir;
            var neighbor = blockList.FirstOrDefault(b => b.blockCenter == neighborCenter);
            if (neighbor == null) continue;

            Vector3 neighborPos = neighborCenter - Vector3.one * 0.5f;
            var newBlock = blockGenerator.GenerateOneBlock(
                Mathf.FloorToInt(neighborPos.x),
                Mathf.FloorToInt(neighborPos.y),
                Mathf.FloorToInt(neighborPos.z)
            );

            newBlock.blockCenter = neighborCenter;
            newBlock.blockPosition = neighborPos;

            blockList.Remove(neighbor);
            blockList.Add(newBlock);
        }
    }

    private Mesh UpdateChunkMesh(Chunk chunk)
    {
        var meshFilter = chunk.GetComponent<MeshFilter>();
        meshFilter.mesh.Clear();
        return ChunkGenerator.AcceptMesh(chunk).GetComponent<MeshFilter>().mesh;
    }
}