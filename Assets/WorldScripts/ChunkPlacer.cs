using ChunkGen;
using System.Collections.Generic;
using TerrainGen;
using UnityEngine;

internal class ChunkPlacer : MonoBehaviour
{
    public GameObject prefab;

    private GameObject chunk;

    int a = 1;

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
                chunk = Instantiate(prefab, new Vector3(x*16, 0, z*16), Quaternion.identity, transform);
                chunk.name = "chunk";

                var chunkInfo = chunk.GetComponent<Chunk>();
                chunkInfo.id = a++;
                chunkInfo.x = x * 16;
                chunkInfo.z = z * 16;
                
                chunkGenerator.GenerateChunk(blocks);

                chunkGenerator.AcceptMesh(chunk);

                data.Add(a, new Vector2Int(x, z));
            }
        }
    }
}

