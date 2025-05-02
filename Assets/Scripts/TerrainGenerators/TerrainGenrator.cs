using UnityEngine;

public class TerrainGenerator
{
    private FastNoiseLite heightNoise;
    private FastNoiseLite mountNoise;

    public float mountFrequency = 0.001f;
    public float mountAmplitude = 100f;
    public int mountOctave = 5;
    public float mountLacunarity = 2f;
    public float mountGain = 0.38f;

    public float heightFrequency = 0.01f;
    public float heightAmplitude = 10f;
    public int seaLevel = 32;

    public TerrainGenerator()
    {
        mountNoise = new FastNoiseLite();
        mountNoise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);
        mountNoise.SetFrequency(mountFrequency);
        mountNoise.SetFractalType(FastNoiseLite.FractalType.FBm);
        mountNoise.SetFractalOctaves(mountOctave);
        mountNoise.SetFractalLacunarity(mountLacunarity);
        mountNoise.SetFractalGain(mountGain);

        heightNoise = new FastNoiseLite();
        heightNoise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);
        heightNoise.SetSeed(1337); // Заменить на случайный для мира
        heightNoise.SetFrequency(heightFrequency);
    }

    public BlockType[,,] GenerateTerrain(int chunkX, int chunkZ)
    {
        const int chunkSize = 16;
        const int chunkHeight = 128;

        BlockType[,,] blocks = new BlockType[chunkSize, chunkHeight, chunkSize];

        for (int x = 0; x < chunkSize; x++)
        {
            for (int z = 0; z < chunkSize; z++)
            {
                int worldX = chunkX * chunkSize + x;
                int worldZ = chunkZ * chunkSize + z;

                float mountValue = mountNoise.GetNoise(worldX, worldZ);
                float noiseValue = heightNoise.GetNoise(worldX, worldZ);
                int height = Mathf.FloorToInt(((noiseValue + 1f) * 0.5f * heightAmplitude) + ((mountValue + 1f) * 0.5f * mountAmplitude) + seaLevel);

                for (int y = 0; y < chunkHeight; y++)
                {
                    if (y >= height -4 && y < height)
                        blocks[x, y, z] = BlockType.Dirt;
                    if (y == height)
                        blocks[x, y, z] = BlockType.Grass;
                    if (y < height - 4)
                        blocks[x, y, z] = BlockType.Stone;
                    if (y > height)
                        blocks[x, y, z] = BlockType.Air;
                }
            }
        }

        return blocks;
    }
}