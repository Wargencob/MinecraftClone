using static Unity.Collections.AllocatorManager;
using UnityEngine;

public class ChunkPlacer : MonoBehaviour
{
    [SerializeField] private GameObject chunkPrefab;
    private void Start()
    {
        TerrainGenerator terrainGenerator = new ();

        int id = 0;

        for (int x = 0; x < 2; x++)
        {
            for (int z = 0; z < 1; z++)
            {
                ++id;

                BlockType[,,] blocks = terrainGenerator.GenerateTerrain(x, z);

                var chunkObject = Instantiate(chunkPrefab, new Vector3(x * 16 * Block.BlockScale, 0, z * 16 * Block.BlockScale), Quaternion.identity, transform);

                var chunkMesh = chunkObject.GetComponent<MeshFilter>().mesh;
                var chunkCollider = chunkObject.GetComponent<MeshCollider>();

                Chunk chunk = new Chunk(id, new Vector3Int(x * 16, 0, z * 16), blocks);

                chunkObject.GetComponent<ChunkInfoContainer>().ChunkInfo = chunk;

                var chunkMeshGenerator = new ChunkMeshGenerator(blocks, chunkMesh);

                chunkMeshGenerator.GenerateMesh();
                chunkMeshGenerator.GenerateMeshCollider(chunkCollider);

                chunkMesh = chunkMeshGenerator.GetMesh();

                chunkMesh.RecalculateBounds();
                chunkMesh.RecalculateNormals();
            }
        }
    }
}