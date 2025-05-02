using UnityEngine;

public interface IChunk
{
    public Vector3Int ChunkPos { get; }

    public BlockType[,,] Blocks { get; }
}