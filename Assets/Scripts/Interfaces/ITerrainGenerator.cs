public interface ITerrainGenerator
{
    public BlockType[,,] GenerateTerrain(int chunkX, int chunkZ);
}