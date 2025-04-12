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
        ChunkGenerator chunkGenerator = new();

        for (int x = 0; x < 10; x++)
        {
            for (int z = 0; z < 10; z++)
            {
                TerrainGenerator terrainGenerator = new();

                BlockType[,,] blocks = terrainGenerator.GenerateTerrain(x, z);

                var prefab = Instantiate(chunkPrefab, new Vector3(x*16*Block.blockScale, 0, z*16*Block.blockScale), Quaternion.identity, transform);
                prefab.name = "chunk";

                var chunk = prefab.GetComponent<Chunk>();
                chunk.id = ++a;

                chunk = chunkGenerator.GenerateChunk(blocks, chunk);

                prefab.GetComponent<MeshFilter>().mesh = chunk.GetComponent<MeshFilter>().mesh;
            }
        }
    }
}

