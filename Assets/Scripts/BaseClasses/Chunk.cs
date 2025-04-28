using UnityEngine;

public class Chunk : IChunk
{
    public static int ChunkWidth = 16;
    public static int ChunkDepth = 128;
    public static int ChunkHeight = 16;

    public int ChunkId { get; private set; }

    public Vector3Int ChunkPos { get; private set; }

    public BlockType[,,] Blocks { get; private set; }

    public Chunk(int ChunkId, Vector3Int ChunkPos, BlockType[,,] Blocks)
    {
        this.ChunkId = ChunkId;
        this.ChunkPos = ChunkPos;
        this.Blocks = Blocks;
    }
}