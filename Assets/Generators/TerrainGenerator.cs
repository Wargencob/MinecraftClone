using UnityEngine;

namespace TerrainGen
{
    public class TerrainGenerator
    {
        private const int ChunkWidth = 16;
        private const int ChunkHeight = 16;
        private const int ChunkDepth = 128;
        public BlockType[,,] GenerateTerrain()
        {
            var result = new BlockType[ChunkWidth, ChunkDepth, ChunkHeight];

            for (int x = 0; x < ChunkWidth; x++)
            {
                for (int z = 0; z < ChunkHeight; z++)
                {
                    float height = Mathf.PerlinNoise(x * 0.1f, z * 0.1f) * 11 + 10 * 3;

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


