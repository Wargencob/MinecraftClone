using UnityEngine;

namespace TerrainGen
{
    public class TerrainGenerator
    {
        private const int ChunkWidth = 16;
        private const int ChunkHeight = 16;
        private const int ChunkDepth = 128;
        public BlockType[,,] GenerateTerrain(int chunkX, int chunkZ)
        {
            var result = new BlockType[ChunkWidth, ChunkDepth, ChunkHeight];

            for (int x = 0; x < ChunkWidth; x++)
            {
                for (int z = 0; z < ChunkHeight; z++)
                {
                    int worldX = chunkX * ChunkWidth + x;
                    int worldZ = chunkZ * ChunkDepth + z;

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
            return result;
        }
    }
}


