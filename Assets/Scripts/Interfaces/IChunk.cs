using UnityEngine;

public interface IChunk
{
    public int ChunkId { get; }
    public Vector3Int ChunkPos { get; }

    public BlockType[,,] Blocks { get; }
}