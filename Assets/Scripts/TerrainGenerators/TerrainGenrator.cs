using UnityEngine;
public class TerrainGenerator : ITerrainGenerator
{
    readonly int ChunkWidth = Chunk.ChunkWidth;
    readonly int ChunkHeight = Chunk.ChunkHeight;
    readonly int ChunkDepth = Chunk.ChunkDepth;
    public BlockType[,,] GenerateTerrain(int chunkX, int chunkZ)
    {
        var result = new BlockType[ChunkWidth, ChunkDepth, ChunkHeight];

        for (int x = 0; x < Chunk.ChunkWidth; x++)
        {
            for (int z = 0; z < Chunk.ChunkHeight; z++)
            {
                int worldX = chunkX * Chunk.ChunkWidth + x;
                int worldZ = chunkZ * Chunk.ChunkDepth + z;

                float baseHeight = Mathf.PerlinNoise(worldX * 0.05f, worldZ * 0.05f) * 20f;

                float detailNoise = Mathf.PerlinNoise(worldX * 0.2f, worldZ * 0.2f) * 5f;

                float turbulence = Mathf.PerlinNoise((worldX + 100) * 0.1f, (worldZ + 100) * 0.1f) * 3f;

                int height = Mathf.FloorToInt(baseHeight + detailNoise + turbulence + 20);

                for (int y = 0; y < (int)height; y++)
                {
                    result[x, y, z] = BlockType.Dirt;
                }
            }
        }

        for (int x = 0; x < Chunk.ChunkWidth; x++)
        {
            for (int z = 0; z < Chunk.ChunkHeight; z++)
            {
                for (int y = 0; y < Chunk.ChunkDepth; y++)
                {
                    if (result[x, y + 1, z] == BlockType.Air)
                    {
                        result[x, y, z] = BlockType.Grass;
                        break;
                    }
                }
            }
        }

        return result;
    }
}