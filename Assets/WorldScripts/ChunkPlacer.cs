using ChunkGen;
using System.Collections.Generic;
using TerrainGen;
using UnityEngine;

internal class ChunkPlacer : MonoBehaviour
{
    public GameObject chunkPrefab;

    int a = 0;

    private void Start()
    {
        TerrainGenerator terrainGenerator = new();

        ChunkGenerator chunkGenerator = new();

        BlockType[,,] blocks = terrainGenerator.GenerateTerrain();

        var data = new Dictionary<int, Vector2Int>();

        for (int x = 0; x < 10; x++)
        {
            for (int z = 0; z < 10; z++)
            {
                var prefab = Instantiate(chunkPrefab, new Vector3(x*16, 0, z*16), Quaternion.identity, transform);
                prefab.name = "chunk";

                var chunk = prefab.GetComponent<Chunk>();
                chunk.id = ++a;
                chunk.x = x * 16;
                chunk.z = z * 16;
                chunk.transform = prefab.transform;
                chunk.chunkMesh = prefab.GetComponent<MeshFilter>().mesh;
                
                chunkGenerator.GenerateChunk(blocks);

                chunkGenerator.AcceptMesh(chunk);

                data.Add(a, new Vector2Int(x, z));
            }
        }
    }
}

